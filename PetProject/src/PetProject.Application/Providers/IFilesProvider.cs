using CSharpFunctionalExtensions;
using PetProject.Application.FileProvider;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Application.Providers;

public interface IFilesProvider
{
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(IEnumerable<FileData> fileData, CancellationToken cancellationtoken = default);
    Task<Result<IReadOnlyList<string>, Error>> RemoveFiles(IEnumerable<string> filesNames, CancellationToken cancellationToken = default);
    Task<Result<string, Error>> GetUrlFile(FileMetaData fileMetaData, CancellationToken cancellationToken);
}
