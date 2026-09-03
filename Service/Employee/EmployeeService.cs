using EmployeeApi.Data;
using Microsoft.EntityFrameworkCore;
using EmployeeEntity = EmployeeApi.Model.Employee.Employee;
namespace EmployeeApi.Service.Employee

{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeEntity>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<EmployeeEntity?> GetById(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<EmployeeEntity> Create(EmployeeEntity employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<EmployeeEntity> Update(EmployeeEntity employee)
        {
            var existingEmployee = await _context.Employees.FindAsync(employee.Id)
                ?? throw new KeyNotFoundException($"Employee {employee.Id} was not found.");
            _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
            await _context.SaveChangesAsync();
            return existingEmployee;
        }

        public async Task<bool> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee is null)
            {
                return false;
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}