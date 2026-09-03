using EmployeeApi.Service.Employee;
namespace EmployeeApi;

public static class EmployeeApiServiceRegistrar
{
    public static IServiceCollection AddEmployeeApiServices(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        return services;
    }
}