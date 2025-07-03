using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetProject.Application.FileProvider;
using PetProject.Application.Providers;
using PetProject.Infrastructure.MessageQueues;
using FileInfo = PetProject.Application.FileProvider.FileInfo;

namespace PetProject.Infrastructure.BackgroundServices;

public class FilesCleanerBackgroundService : BackgroundService
{
    private readonly ILogger<FilesCleanerBackgroundService> _logger;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly IServiceScopeFactory _scopedFactory;
    public FilesCleanerBackgroundService(
        ILogger<FilesCleanerBackgroundService> logger, 
        IMessageQueue<IEnumerable<FileInfo>> messageQueue,
        IServiceScopeFactory scopedFactory
        )
    {
        _scopedFactory = scopedFactory;
        _messageQueue = messageQueue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("FilesCleanerBackgroundService is starting");

        await using var scope = _scopedFactory.CreateAsyncScope();

        var fileProvdier = scope.ServiceProvider.GetRequiredService<IFilesProvider>();

        while (!stoppingToken.IsCancellationRequested)
        {
           
            var filesInfos = await _messageQueue.ReadAsync(stoppingToken);

            foreach(var fileInfo in filesInfos) 
            {
                await fileProvdier.RemoveFile(fileInfo, stoppingToken);
            }                  
        }        
        await Task.CompletedTask;
    }
}
