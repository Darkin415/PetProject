using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Minio.AspNetCore;
using PetProject.Application;
using PetProject.Application.Providers;
using PetProject.Application.Volunteers;
using PetProject.Contracts;
using PetProject.Infrastructure.BackgroundServices;
using PetProject.Infrastructure.DbContexts;
using PetProject.Infrastructure.MessageQueues;
using PetProject.Infrastructure.Options;
using PetProject.Infrastructure.Providers;
using MinioOptions = PetProject.Infrastructure.Options.MinioOptions;
namespace PetProject.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContexts()
            .AddMinio(configuration)
            .AddRepositories()
            .AddDatabase()
            .AddHostedServices()
            .AddMessageQueues();     
       
        return services;
    }

    //private static IServiceCollection AddServices(
    //    this IServiceCollection services)
    //{
    //    services.AddScoped<IFIlesCleanerService, FilesCleanerBackgroundService>();

    //    return services;
    //}

    private static IServiceCollection AddMessageQueues(
        this IServiceCollection services)
    {
        services.AddSingleton<IMessageQueue<IEnumerable<PetProject.Application.FileProvider.FileInfo>>,
            InMemoryMessageQueue<IEnumerable<PetProject.Application.FileProvider.FileInfo>>>();

        return services;
    }

    private static IServiceCollection AddHostedServices(
        this IServiceCollection services)
    {
        services.AddHostedService<FilesCleanerBackgroundService>();

        return services;
    }


    private static IServiceCollection AddDatabase(
        this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>(); 

        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();

        services.AddScoped<ISpeciesRepository, SpeciesRepository>();

        return services;
    }

    private static IServiceCollection AddDbContexts(
        this IServiceCollection services)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();

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
