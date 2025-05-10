using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PetProject.API.Response;
using PetProject.Domain.Shared;

namespace PetProject.API.Exctensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Error error)
    {
         
        var details = new ProblemDetails();
        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError,
        };

        var envelope = Envelope.Error(error);


        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }




}

