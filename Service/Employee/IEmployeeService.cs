using EmployeeEntity = EmployeeApi.Model.Employee.Employee;
namespace EmployeeApi.Service.Employee
{
    public interface IEmployeeService
    {
        Task<List<EmployeeEntity>> GetAll();
        Task<EmployeeEntity?> GetById(int id);
        Task<EmployeeEntity> Create(EmployeeEntity employee);
        Task<EmployeeEntity> Update(EmployeeEntity employee);
        Task<bool> Delete(int id);
    }
}
