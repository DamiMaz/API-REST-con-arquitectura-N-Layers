using Microsoft.AspNetCore.Mvc;

namespace NLayers.Presentation.Base;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult Success(object? data = null, string message = "OperaciÃ³n exitosa")
    {
        return Ok(new
        {
            code = 200,
            data,
            message
        });
    }

    protected IActionResult CreatedResult(object? data = null, string message = "Recurso creado exitosamente")
    {
        return StatusCode(201, new
        {
            code = 201,
            data,
            message
        });
    }

    protected IActionResult NotFoundResult(string message = "Recurso no encontrado")
    {
        return NotFound(new
        {
            code = 404,
            message
        });
    }

    protected IActionResult BadRequestResult(string message)
    {
        return BadRequest(new
        {
            code = 400,
            message
        });
    }
}
