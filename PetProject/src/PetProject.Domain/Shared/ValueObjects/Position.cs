﻿using CSharpFunctionalExtensions;

namespace PetProject.Domain.Shared.ValueObjects;

public class Position : ValueObject
{
    public static Position First => new(1);
    private Position(int value)
    {
        Value = value;
    }
    public int Value { get; }

    public Result<Position, Error> Forward() 
        => Create(Value+1);

    public Result<Position, Error> Back()
        => Create(Value - 1);

    public static Result<Position, Error> Create(int number)
    {
        if (number < 1)
            return Errors.General.ValueIsInvalid("position");

        return new Position(number);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(Position position) =>
        position.Value;

}