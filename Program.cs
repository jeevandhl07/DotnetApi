using EmployeeApi;
using EmployeeApi.Response;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApiResponseFormat();

builder.Services.AddEmployeeApiServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseApiExceptionMiddleware();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("ReactPolicy");

app.MapGet("/", () => "Employee API is running!");

app.MapControllers();

app.Run();
