namespace PetProject.Contracts;

public interface ISoftDeletable
{
    void Delete();
    void Restore();
}

