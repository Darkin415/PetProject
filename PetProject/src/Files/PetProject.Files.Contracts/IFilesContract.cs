using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using FileInfo = System.IO.FileInfo;

namespace PetProject.Files.Contracts;

public interface IFilesContract
{
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(IEnumerable<FileData> fileData, CancellationToken cancellationtoken = default);
    Task<Result<IReadOnlyList<string>, Error>> RemoveFiles(IEnumerable<string> filesNames, CancellationToken cancellationToken = default);
    Task<Result<string, Error>> GetUrlFile(FileMetaData fileMetaData, CancellationToken cancellationToken);
    Task<UnitResult<Error>> RemoveFile(FileInfo fileInfo, CancellationToken cancellationToken = default);
}