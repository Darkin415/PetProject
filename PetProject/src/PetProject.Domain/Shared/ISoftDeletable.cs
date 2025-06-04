namespace PetProject.Domain.Volunteers;

public interface ISoftDeletable
{
    void Delete();
    void Restore();
}

