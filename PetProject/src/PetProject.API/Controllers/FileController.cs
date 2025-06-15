using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Minio;
using Minio.DataModel.Args;
using PetProject.API.Exctensions;
using PetProject.Application.FileProvider;
using PetProject.Application.Providers;
using PetProject.Domain.Shared.ValueObject;
using IFileProvider = PetProject.Application.Providers.IFileProvider;

namespace PetProject.API.Controllers;

public class FileController : ApplicationController
{

    private readonly string BUCKET_NAME = "photos";
    private readonly IFileProvider _fileProvider;

    public FileController(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }
    [HttpPost]
    public async Task<IActionResult> CreateFile(IFormFile file, CancellationToken cancellationToken)
    {
        await using var stream = file.OpenReadStream();

        var fileData = new FileData(stream, BUCKET_NAME, Guid.NewGuid());

        var result = await _fileProvider.UploadFile(fileData, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{objectName}")]
    public async Task<IActionResult> DeleteFile(
    [FromRoute] Guid objectName,
    CancellationToken cancellationToken)
    {
        var fileMetaData = new FileMetaData(BUCKET_NAME, objectName);
        var result = await _fileProvider.DeleteFile(fileMetaData, cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet]

    public async Task<IActionResult> GetUrl([FromRoute] Guid ObjectName, CancellationToken cancellationToken)
    {
        var fileMetaData = new FileMetaData(BUCKET_NAME, ObjectName);

        var result = await _fileProvider.GetUrlFile(fileMetaData, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

}
