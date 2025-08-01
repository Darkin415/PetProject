using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetProject.Core.Messaging;
using PetProject.Files.Application;
using PetProject.Files.Infrastructure.MessageQueues;
using PetProject.Files.Infrastructure.Options;
// using PetProject.Files.Infrastructure.Providers;

namespace PetProject.Files.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddFilesInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        
        // services.AddMinio(configuration);
        
        
        // services.AddScoped<IFilesProvider, MinioProvider>();
        
        services.AddScoped<MinioOptions>();
        
        services.AddSingleton<IMessageQueue<IEnumerable<FileInfo>>,
            InMemoryMessageQueue<IEnumerable<FileInfo>>>();
        
        
        // services.AddScoped<IFIlesCleanerService, FilesCleanerBackgroundService>();
        
        return services;
    }

    // private static IServiceCollection AddMinio(
    //     this IServiceCollection services, IConfiguration configuration)
    // {
    //     // services.Configure<MinioOptions>(
    //     //     configuration.GetSection(MinioOptions.MINIO));
    //
    //     services.AddMinio(options =>
    //     {
    //         MinioOptions minioOption = configuration.GetSection(MinioOptions.MINIO)
    //             .Get<MinioOptions>() ?? throw new ApplicationException("Minio confuguration");
    //
    //         options.WithEndpoint(minioOption.Endpoint);
    //
    //         options.WithCredentials(minioOption.AccessKey, minioOption.SecretKey);
    //         options.WithSSL(minioOption.WithSsl);
    //     });
    //
    //     return services;
    // }
}