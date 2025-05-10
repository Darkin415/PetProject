using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain;

public record Email
{
    private Email(string link)
    {
        Link = link;
    }
    public string Link { get;}
    public static Result<Email, Error> Create(string link)
    {
        if (string.IsNullOrWhiteSpace(link))
            return Errors.General.ValueIsInvalid("Email");
        return new Email(link);
    }
}