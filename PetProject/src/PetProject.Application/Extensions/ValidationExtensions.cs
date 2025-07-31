using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;
using PetProject.Contracts;

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
