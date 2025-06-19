using CSharpFunctionalExtensions;
using PetProject.Application.FileProvider;
using PetProject.Domain.Shared.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Application.Providers;

public interface IFilesProvider
{
    Task<Result<string, Error>> UploadFile(FileData fileData, CancellationToken cancellationtoken = default);
    Task<Result<string, Error>> DeleteFile(FileMetaData fileMetaData, CancellationToken cancellationtoken = default);
    Task<Result<string, Error>> GetUrlFile(FileMetaData fileMetaData, CancellationToken cancellationToken);
}
