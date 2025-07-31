using CSharpFunctionalExtensions;
using PetProject.Contracts;

namespace PetProject.Application.Providers;

public record FilePath
{
    public FilePath(string path)
    {
        Path = path;     
    }

    public string Path { get;}

    public static Result<FilePath, Error> Create(Guid path, string extension)
    {
        var fullPath = path + "." + extension;
        return new FilePath(fullPath);
    }

    public static Result<FilePath, Error> Create(string fullPath)
    {       
        return new FilePath(fullPath);
    }
}
