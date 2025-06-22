using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetProject.Application.FileProvider;
using PetProject.Application.Providers;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetProject.Infrastructure.Providers;

public class MinioProvider : IFilesProvider
{
    private const int MAX_DEGREE_OF_PARALLELISM = 10;
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;
    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }
    public async Task<Result <IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> filesData,
        CancellationToken cancellationToken)
    {
        var semaphorSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);

        var filesList = filesData.ToList();
        try
        {
            await IfBucketsNotExistCreateBucket(filesList, cancellationToken);

            var tasks = filesList.Select(async file => await PutObject(file, semaphorSlim, cancellationToken));

            var pathResult = await Task.WhenAll(tasks);

            if (pathResult.Any(p => p.IsFailure))
                return pathResult.First().Error;

            var result = pathResult.Select(p => p.Value).ToList();

            return result;  
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to upload file in minio");
            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphorSlim.Release();
        }    
    }

    private async Task<Result<FilePath, Error>> PutObject(
        FileData fileData,
        SemaphoreSlim semaphoreSlim, 
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(fileData.BucketName)
            .WithStreamData(fileData.Stream)
            .WithObjectSize(fileData.Stream.Length)
            .WithObject(fileData.FilePath.Path);

        try
        {
            await _minioClient
                .PutObjectAsync(putObjectArgs, cancellationToken);

            return fileData.FilePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to upload file in minio with path {path} in bucket {bucket}",
                fileData.FilePath.Path,
                fileData.BucketName);

            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    public async Task IfBucketsNotExistCreateBucket(IEnumerable<FileData> filesData, CancellationToken cancellationToken)
    {
        HashSet<string> bucketNames = [.. filesData.Select(file => file.BucketName)];

        foreach(var bucketName in bucketNames)
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(bucketName);

            var bucketExist = await _minioClient
                .BucketExistsAsync(bucketExistArgs, cancellationToken);

            if(bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        }
    }

    public async Task<Result<string, Error>> DeleteFile(FileMetaData fileMetaData, CancellationToken cancellationToken)
    {
        try
        {
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(fileMetaData.BucketName)
                .WithObject(fileMetaData.ObjectName.ToString());

            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);

            return fileMetaData.ObjectName.ToString();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to delete fail in Minio");
            return Error.Failure("file.delete", "Fail to delete fail in Minio");
        }
    }

    public async Task<Result<string, Error>> GetUrlFile(FileMetaData fileData, CancellationToken cancellationToken)
    {
        var objectExistArgs = new StatObjectArgs()
            .WithBucket(fileData.BucketName)
            .WithObject(fileData.ObjectName.ToString());
        try
        {
            if (objectExistArgs is null)
                return Error.NotFound("object.not.found", "Url not found");

            var presignedArgs = new PresignedGetObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.ObjectName.ToString())
                .WithExpiry(60 * 40 * 60);

            var url = await _minioClient.PresignedGetObjectAsync(presignedArgs);
            return url;
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to get file in minio");
            return Error.Failure("file.get", "Fail to get fail in Minio");
        }
    }

  
}
