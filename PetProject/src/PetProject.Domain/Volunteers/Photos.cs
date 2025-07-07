using CSharpFunctionalExtensions;
namespace PetProject.Domain.Volunteers;
public record Photos
{
    public Photos(FilePath pathToStorage)
    {
        PathToStorage = pathToStorage;
    }

    public FilePath PathToStorage { get; }
   
}
