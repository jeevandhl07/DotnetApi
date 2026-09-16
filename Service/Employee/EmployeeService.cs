using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using EmployeeEntity = EmployeeApi.Model.Employee.Employee;

namespace EmployeeApi.Service.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly string _connectionString;

        public EmployeeService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");
        }

        private SqlConnection CreateConnection() => new(_connectionString);

        public async Task<List<EmployeeEntity>> GetAll()
        {
            await using var connection = CreateConnection();

            var employees = await connection.QueryAsync<EmployeeEntity>(
                "dbo.Employee_GetAll",
                commandType: CommandType.StoredProcedure);

            return employees.ToList();
        }

        public async Task<EmployeeEntity?> GetById(int id)
        {
            await using var connection = CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<EmployeeEntity>(
                "dbo.Employee_GetById",
                new { Id = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<EmployeeEntity> Create(EmployeeEntity employee)
        {
            await using var connection = CreateConnection();

            return await connection.QuerySingleAsync<EmployeeEntity>(
                "dbo.Employee_Create",
                new
                {
                    employee.Name,
                    employee.Email,
                    employee.Salary
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<EmployeeEntity> Update(EmployeeEntity employee)
        {
            await using var connection = CreateConnection();

            return await connection.QuerySingleAsync<EmployeeEntity>(
                "dbo.Employee_Update",
                new
                {
                    employee.Id,
                    employee.Name,
                    employee.Email,
                    employee.Salary
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Delete(int id)
        {
            await using var connection = CreateConnection();

            var deletedRows = await connection.ExecuteScalarAsync<int>(
                "dbo.Employee_Delete",
                new { Id = id },
                commandType: CommandType.StoredProcedure);

            return deletedRows > 0;
        }
    }
}
