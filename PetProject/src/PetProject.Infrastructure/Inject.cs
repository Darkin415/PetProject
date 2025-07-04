﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Minio.AspNetCore;
using PetProject.Application.Database;
using PetProject.Application.Providers;
using PetProject.Application.Volunteers;
using PetProject.Infrastructure.BackgroundServices;
using PetProject.Infrastructure.MessageQueues;
using PetProject.Infrastructure.Options;
using PetProject.Infrastructure.Providers;
using PetProject.Infrastructure.Repositories;
using MinioOptions = PetProject.Infrastructure.Options.MinioOptions;
namespace PetProject.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>();

        services.AddScoped<IVolunteersRepository, VolunteersRepository>();

        services.AddScoped<ISpeciesRepository, SpeciesRepository>();

        services.AddHostedService<FilesCleanerBackgroundService>();

        services.AddSingleton<IMessageQueue<IEnumerable<PetProject.Application.FileProvider.FileInfo>>,
            InMemoryMessageQueue<IEnumerable<PetProject.Application.FileProvider.FileInfo>>
>();

        services.AddMinio(configuration);

        return services;
    }
    private static IServiceCollection AddMinio(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioOptions>(
            configuration.GetSection(MinioOptions.MINIO));


        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
            ?? throw new ApplicationException("Missing minio configuration");

            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.WithSsl);
        });

        services.AddScoped<IFilesProvider, MinioProvider>();

        return services;
    }
}
