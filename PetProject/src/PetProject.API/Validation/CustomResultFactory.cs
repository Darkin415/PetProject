using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetProject.API.Response;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using static PetProject.API.Response.Envelope;

namespace PetProject.API.Validation;

public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(
        ActionExecutingContext context,
        ValidationProblemDetails? validationProblemDetails)
    {
        if (validationProblemDetails is null)
        {
            throw new InvalidOperationException("ValidationProblemDetails is null");
        }

        List<ResponseError> errors = [];

        foreach (var (invalidField, validationErrors) in validationProblemDetails.Errors)
        {
            var responseErrors = from errorMessage in validationErrors
                                 let error = Domain.Shared.ValueObject.Error.Deserialize(errorMessage)
                                 select new ResponseError(error.Code, error.Message, invalidField);

            errors.AddRange(responseErrors);
        }
        var envelope = Envelope.Error(errors);

        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}