using ECommerce.Application.Common;
using Microsoft.AspNetCore.Mvc;
using static ECommerce.Application.Common.ResultOfT;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected static ActionResult<T> ToActionResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.Value);
            }

            return ToProblem(result.Errors);
        }

        protected static ActionResult<T> ToActionResult<T>(Result result)
        {
            if (result.IsSuccess)
            {
                return new OkResult();
            }

            return ToProblem(result.Errors);
        }

        public static ObjectResult ToProblem(IReadOnlyList<Error> errors)
        {
            var firstError = errors.First();

            var statusCode = firstError.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.BadRequest => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = firstError.Code,
                Detail = string.Join(", ", errors.Select(e => e.Description)),
                Extensions = { { "errors", errors.Select(e => new { e.Code, e.Description }) } }
            };

            return new ObjectResult(problemDetails)
            {
                StatusCode = statusCode
            };
        }
    }
}
