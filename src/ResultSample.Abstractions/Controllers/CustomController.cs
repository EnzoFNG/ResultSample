using Microsoft.AspNetCore.Mvc;
using ResultSample.Abstractions.Models;
using Error = ResultSample.Abstractions.Models.Error;

namespace ResultSample.Abstractions.Controllers;

[ApiController]
public class CustomController : ControllerBase
{
    protected IActionResult CreatedAtCustomResponse<T>(Result result)
    {
        if (result is Result<T> response)
        {
            return response.Match(response => StatusCode(201, response), ErrorResponse);
        }

        return result.Match(NoContent, ErrorResponse);
    }

    protected IActionResult CustomResponse(Result result)
        => result.Match(NoContent, ErrorResponse);

    protected IActionResult CustomResponse<T>(Result result)
    {
        if (result is Result<T> response)
        {
            return response.Match(response => Ok(response), ErrorResponse);
        }

        return result.Match(NoContent, ErrorResponse);
    }

    protected IActionResult CustomResponse<T>(Result<T> result)
        => result.Match(response => Ok(response), ErrorResponse);

    protected IActionResult ErrorResponse(List<Error> errors)
    {
        return BadRequest(new Dictionary<string, Error[]>
                {
                    { $"errors", errors!.ToArray() }
                });
    }
}