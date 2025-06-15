using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetProject.Application.FileProvider;
using PetProject.Application.Providers;
using PetProject.Domain.Shared.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetProject.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;
    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }
    public async Task<Result<string, Error>> UploadFile(
        FileData fileData,  
        CancellationToken cancellationToken)
    {

        var bucketExistArgs = new BucketExistsArgs()
        .WithBucket(fileData.BucketName);

        var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
        try
        {
            if (!bucketExist)
            {
                var makeBucketArgs = new MakeBucketArgs()
                .WithBucket(fileData.BucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
            var path = Guid.NewGuid();

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithStreamData(fileData.Stream)
                .WithObjectSize(fileData.Stream.Length)
                .WithObject(path.ToString());

            var result = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return result.ObjectName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to upload file in minio");
            return Error.Failure("file.upload", "Fail to upload file in minio");
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

    public async Task<Result<string,Error>> GetUrlFile(FileMetaData fileData, CancellationToken cancellationToken)
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
