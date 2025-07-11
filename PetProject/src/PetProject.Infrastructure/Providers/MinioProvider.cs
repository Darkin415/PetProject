﻿using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetProject.Application.FileProvider;
using PetProject.Application.Providers;
using PetProject.Domain.Shared.ValueObjects;
using PetProject.Domain.Volunteers;
using FileInfo = PetProject.Application.FileProvider.FileInfo;

namespace PetProject.Infrastructure.Providers;

public class MinioProvider : IFilesProvider
{
    private const int MAX_DEGREE_OF_PARALLELISM = 10;
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;
    private const string BUCKET_NAME = "photos";
    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }
    public async Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> filesData,
        CancellationToken cancellationToken)
    {
        var semaphorSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);

        var filesList = filesData.ToList();
        try
        {
            await IfBucketsNotExistCreateBucket(filesList.Select(file => file.Info.BucketName), cancellationToken);

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
            .WithBucket(fileData.Info.BucketName)
            .WithStreamData(fileData.Stream)
            .WithObjectSize(fileData.Stream.Length)
            .WithObject(fileData.Info.FilePath.Path);

        try
        {
            await _minioClient
                .PutObjectAsync(putObjectArgs, cancellationToken);

            return fileData.Info.FilePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to upload file in minio with path {path} in bucket {bucket}",
                fileData.Info.FilePath.Path,
                fileData.Info.BucketName);

            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    public async Task<Result<string, Error>> RemoveObject(
        string fileName,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var removeObjectArgs = new RemoveObjectArgs()
            .WithBucket(BUCKET_NAME)
            .WithObject(fileName);

        try
        {
            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);

            return fileName;
        }
        catch (Exception ex)
        {

            _logger.LogError(ex,
                "Fail to remove file from minio with name {fileName} in bucket {BUCKET_NAME}",
                fileName,
                BUCKET_NAME);

            return Error.Failure("file.remove", "Fail to remove file from minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    public async Task<UnitResult<Error>> RemoveFile(
        FileInfo fileInfo,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await IfBucketsNotExistCreateBucket([fileInfo.BucketName], cancellationToken);

            var statArgs = new StatObjectArgs()
                .WithBucket(fileInfo.BucketName)
                .WithObject(fileInfo.FilePath.Path);


          var objectStat = await _minioClient.StatObjectAsync(statArgs, cancellationToken);
            if (objectStat is null)
                return Result.Success<Error>();

            var removeArgs = new RemoveObjectArgs()
            .WithBucket(fileInfo.BucketName)
            .WithObject(fileInfo.FilePath.Path);

            await _minioClient.RemoveObjectAsync(removeArgs, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to remove file in minio with path {path} in bucket {bucket}",
                fileInfo.FilePath.Path,
                fileInfo.BucketName);

            return Error.Failure("file.remove", "Fail to remove file in minio");
        }

        return Result.Success<Error>();
    }


    public async Task IfBucketsNotExistCreateBucket(IEnumerable<string> buckets, CancellationToken cancellationToken)
    {
        HashSet<string> bucketNames = [..buckets];

        foreach (var bucketName in bucketNames)
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(bucketName);

            var bucketExist = await _minioClient
                .BucketExistsAsync(bucketExistArgs, cancellationToken);

            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        }
    }
    public async Task<Result<IReadOnlyList<string>, Error>> RemoveFiles(
        IEnumerable<string> filesNames,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);
        var filesNamesList = filesNames.ToList();

        try
        {
            var tasks = filesNamesList.Select(async fileName =>
                await RemoveObject(fileName, semaphoreSlim, cancellationToken));

            var fileNamesResult = await Task.WhenAll(tasks);

            if (fileNamesResult.Any(p => p.IsFailure))
            {
                _logger.LogError("Ошибка удаления FileName: Error");
                return fileNamesResult.First().Error;
            }

            var results = fileNamesResult.Select(p => p.Value).ToList();

            return results;
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
