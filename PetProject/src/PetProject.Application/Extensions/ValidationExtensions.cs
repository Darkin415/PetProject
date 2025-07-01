using FluentValidation.Results;
using PetProject.Domain.Shared.ValueObject;
using System.ComponentModel.DataAnnotations;

namespace PetProject.Application.Extensions;
public static class ValidationExtensions
{
    public static ErrorList ToErrorList(this FluentValidation.Results.ValidationResult validationResult)
    {
        var validationErrors = validationResult.Errors;

        var errors = from validationError in validationErrors
                     let errorMessage = validationError.ErrorMessage
                     let error = Domain.Shared.ValueObject.Error.Deserialize(errorMessage)
                     select Domain.Shared.ValueObject
                     .Error.Validation(error.Code, error.Message, validationError.PropertyName);

        return errors.ToList();
    }
}
