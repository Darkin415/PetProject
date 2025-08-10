using CSharpFunctionalExtensions;
using PetProject.Files.Contracts;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using FileInfo = System.IO.FileInfo;

namespace PetProject.Files.Infrastructure;

public class FilesContract : IFilesContract
{
    public Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(IEnumerable<FileData> fileData, CancellationToken cancellationtoken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IReadOnlyList<string>, Error>> RemoveFiles(IEnumerable<string> filesNames, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<string, Error>> GetUrlFile(FileMetaData fileMetaData, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UnitResult<Error>> RemoveFile(FileInfo fileInfo, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}