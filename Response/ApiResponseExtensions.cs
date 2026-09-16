using EmployeeApi.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Response
{
    public static class ApiResponseExtensions
    {
        public static IMvcBuilder AddApiResponseFormat(this IMvcBuilder builder)
        {
            return builder.ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values
                        .SelectMany(value => value.Errors)
                        .Select(error => error.ErrorMessage)
                        .ToList();

                    return new BadRequestObjectResult(ApiResponse.Failure(
                        "Validation failed.",
                        errors,
                        StatusCodes.Status400BadRequest));
                };
            });
        }

        public static IApplicationBuilder UseApiExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ApiExceptionMiddleware>();
        }
    }
}
