using Microsoft.AspNetCore.Mvc;
using ResultSample.Abstractions.Models;
using Error = ResultSample.Abstractions.Models.Error;

namespace ResultSample.Abstractions.Controllers;

[ApiController]
public class CustomController : ControllerBase
{
    protected IActionResult CreatedAtCustomResponse(Result result)
    {
        return ResultResponse(result, StatusCode(201, result.Response));
    }

    protected IActionResult CustomResponse(Result result)
    {
        return ResultResponse(result, Ok(result.Response));
    }

    private IActionResult ResultResponse(Result result, IActionResult actionResult)
    {
        if (result.IsFailure)
            return ErrorResponse([.. result.Errors!]);

        return actionResult;
    }

    protected IActionResult ErrorResponse(List<Error> errors)
    {
        return BadRequest(new Dictionary<string, Error[]>
                {
                    { $"errors", errors!.ToArray() }
                });
    }
}