using PetProject.Application.Providers;

namespace PetProject.Volunteers.Domain.Pets;
public record Photos
{
    public Photos(FilePath pathToStorage)
    {
        PathToStorage = pathToStorage;
    }

    public FilePath PathToStorage { get; }
   
}
