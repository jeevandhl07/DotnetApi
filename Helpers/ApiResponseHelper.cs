using EmployeeApi.Response;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Helpers
{
    public static class ApiResponseHelper
    {
        public static IActionResult Ok(object data, string message = "Success")
        {
            return new OkObjectResult(ApiResponse.Success(data, message));
        }

        public static IActionResult BadRequest(string message, IEnumerable<string>? errors = null)
        {
            return new BadRequestObjectResult(ApiResponse.Failure(
                message,
                errors,
                StatusCodes.Status400BadRequest));
        }

        public static IActionResult NotFound(string message, IEnumerable<string>? errors = null)
        {
            return new NotFoundObjectResult(ApiResponse.Failure(
                message,
                errors,
                StatusCodes.Status404NotFound));
        }

        public static IActionResult InternalServerError(string message, IEnumerable<string>? errors = null)
        {
            return new ObjectResult(ApiResponse.Failure(
                message,
                errors,
                StatusCodes.Status500InternalServerError))
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
