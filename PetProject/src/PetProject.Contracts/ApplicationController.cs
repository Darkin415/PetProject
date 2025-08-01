using Microsoft.AspNetCore.Mvc;
using PetProject.SharedKernel;

namespace PetProject.Contracts;

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