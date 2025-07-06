using PetProject.Domain.Shared.ValueObject;

namespace PetProject.Contracts.Response;

public record Envelope
{
    public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);
    private Envelope(object? result, ErrorList errors)
    {
        Result = result;
        Errors = errors;
        TimeGenerated = DateTime.Now;
    }
    public object? Result { get; }

    public ErrorList? Errors { get; }

    public string? ErrorMessage { get; }

    public DateTime TimeGenerated { get; }

    public static Envelope Ok(object? result = null) => new(result, null);
    public static Envelope Error(ErrorList errors) => new(null, errors);

    public static ResponseError FromError(Error error) =>
       new ResponseError(error.Code, error.Message, null);
}
