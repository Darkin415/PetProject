using CSharpFunctionalExtensions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PetProject.API.Response;
using PetProject.Domain.Shared.ValueObject;
using static PetProject.API.Response.Envelope;
using System.ComponentModel.DataAnnotations;

namespace PetProject.API.Exctensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this UnitResult<Error> result)
    {
        if (result.IsSuccess)
            return new OkResult();

        var statusCode = result.Error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError,
        };
        var responseError = Envelope.FromError(result.Error);
        var envelope = Envelope.Error(new[] { responseError });


        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }
    public static ActionResult ToResponse(this Error error)
    {

        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError,
        };

        var responseError = Envelope.FromError(error);
        var envelope = Envelope.Error(new[] { responseError });

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }

    public static ActionResult ToValidationErrorResponse(this FluentValidation.Results.ValidationResult result)
    {
        if (result.IsValid)
            throw new InvalidOperationException("Result can not be succed");

        var validationErrors = result.Errors;

        var responseErrors = from validationError in validationErrors
                             let errorMessage = validationError.ErrorMessage
                             let error = Domain.Shared.ValueObject.Error.Deserialize(errorMessage)
                             select new ResponseError(error.Code, error.Message, validationError.PropertyName);

        var envelope = Envelope.Error(responseErrors);

        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }      
}