using Microsoft.AspNetCore.Http;

namespace CFS.Application.Common.Result
{
    public enum ErrorCode
    {
        ValidationFailed = StatusCodes.Status400BadRequest,
        NotFound = StatusCodes.Status404NotFound,
        Conflict = StatusCodes.Status409Conflict
    }
}
