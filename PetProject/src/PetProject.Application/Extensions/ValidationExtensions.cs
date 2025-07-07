using FluentValidation.Results;
using PetProject.Domain.Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace PetProject.Application.Extensions;
public static class ValidationExtensions
{
    public static ErrorList ToErrorList(this FluentValidation.Results.ValidationResult validationResult)
    {
        var validationErrors = validationResult.Errors;

        var errors = from validationError in validationErrors
                     let errorMessage = validationError.ErrorMessage
                     let error = Error.Deserialize(errorMessage)
                     select Error.Validation(error.Code, error.Message, validationError.PropertyName);

        return errors.ToList();
    }
}
