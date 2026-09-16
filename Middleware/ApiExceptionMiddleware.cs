using System.Net;
using EmployeeApi.Response;

namespace EmployeeApi.Middleware
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = ApiResponse.Failure(
                    "Something went wrong.",
                    [ex.Message],
                    StatusCodes.Status500InternalServerError);

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
