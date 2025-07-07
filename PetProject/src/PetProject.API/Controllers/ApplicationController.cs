using Microsoft.AspNetCore.Mvc;
using PetProject.Contracts.Response;
namespace PetProject.API.Controllers;

[Route("[controller]")]
[ApiController]
public abstract class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);

        return base.Ok(envelope);
    }
}