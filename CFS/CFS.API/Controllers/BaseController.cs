using CFS.Application.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace CFS.API.Controllers;

public class BaseController : ControllerBase
{
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess) return Ok(result.Value);

        return result.Error!.Code switch
        {
            ErrorCode.BadRequest => BadRequest(result.Error),
            ErrorCode.NotFound => NotFound(result.Error),
            ErrorCode.Conflict => Conflict(result.Error),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown error.")
        };
    }

    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess) return Ok();

        return result.Error!.Code switch
        {
            ErrorCode.BadRequest => BadRequest(result.Error),
            ErrorCode.NotFound => NotFound(result.Error),
            ErrorCode.Conflict => Conflict(result.Error),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "Unknown error.")
        };
    }
}
