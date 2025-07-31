using CSharpFunctionalExtensions;
using PetProject.Application.FileProvider;
using PetProject.Contracts;
using FileInfo = PetProject.Application.FileProvider.FileInfo;

namespace PetProject.Application.Providers;

public interface IFilesProvider
{
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(IEnumerable<FileData> fileData, CancellationToken cancellationtoken = default);
    Task<Result<IReadOnlyList<string>, Error>> RemoveFiles(IEnumerable<string> filesNames, CancellationToken cancellationToken = default);
    Task<Result<string, Error>> GetUrlFile(FileMetaData fileMetaData, CancellationToken cancellationToken);
    Task<UnitResult<Error>> RemoveFile(FileInfo fileInfo, CancellationToken cancellationToken = default);
}
