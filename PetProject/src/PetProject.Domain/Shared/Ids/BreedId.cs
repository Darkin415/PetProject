﻿namespace PetProject.Domain.Shared.Ids;
public record BreedId
{
    public BreedId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    public static BreedId NewGuidId() => new(Guid.NewGuid());

    public static BreedId Empty() => new(Guid.Empty);

    public static BreedId Create(Guid value) => new(value);
}

