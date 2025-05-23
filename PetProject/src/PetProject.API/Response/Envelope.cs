using PetProject.Domain.Shared.ValueObject;
namespace PetProject.API.Response;
public record Envelope
{
    public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);
    private Envelope(object? result, IEnumerable<ResponseError> errors)
    {
        Result = result;
        Errors = errors.ToList();
        TimeGenerated = DateTime.Now;
    }
    public object? Result { get; }

    public List<ResponseError> Errors { get; }

    public string? ErrorMessage { get; }

    public DateTime TimeGenerated { get; }

    public static Envelope Ok(object? result = null) => new(result, []);
    public static Envelope Error(IEnumerable<ResponseError> errors) => new(null, errors);

    public static ResponseError FromError(Error error) =>
       new ResponseError(error.Code, error.Message, null);
}