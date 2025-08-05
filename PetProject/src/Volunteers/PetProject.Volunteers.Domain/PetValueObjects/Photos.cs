using PetProject.SharedKernel.ValueObjects;

namespace PetProject.Volunteers.Domain.PetValueObjects;
public record Photos
{
    public Photos(FilePath pathToStorage)
    {
        PathToStorage = pathToStorage;
    }

    public FilePath PathToStorage { get; }
   
}
