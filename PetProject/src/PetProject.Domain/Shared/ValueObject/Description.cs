﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace PetProject.Domain.Shared.ValueObject;

public record Description
{
    public Description(string information)
    {
        Information = information;
    }
    public string Information { get; }
    public static Result<Description, Error> Create(string information)
    {
        if (string.IsNullOrWhiteSpace(information))
            return Errors.General.ValueIsInvalid("Information");
        return new Description(information);
    }
}
