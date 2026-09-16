using EmployeeApi.Helpers;
using EmployeeApi.Service.Employee;
using EmployeeApi.ViewModel.Employee;
using Microsoft.AspNetCore.Mvc;
using EmployeeEntity = EmployeeApi.Model.Employee.Employee;

namespace EmployeeApi.Api.v1
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeApiController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _service.GetAll();

            return ApiResponseHelper.Ok(employees, "Employees fetched successfully.");
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetById(id);

            if (employee is null)
            {
                return ApiResponseHelper.NotFound(
                    "Employee not found.",
                    [$"Employee with ID {id} was not found."]);
            }

            return ApiResponseHelper.Ok(employee, "Employee fetched successfully.");
        }

        [HttpPost("create/edit")]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeViewModel model)
        {
            if (model.Id != 0 && await _service.GetById(model.Id) is null)
            {
                return ApiResponseHelper.NotFound(
                    "Employee not found.",
                    [$"Employee with ID {model.Id} was not found."]);
            }

            var employee = new EmployeeEntity
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Salary = model.Salary
            };

            if (model.Id == 0)
            {
                var createdEmployee = await _service.Create(employee);

                return ApiResponseHelper.Ok(createdEmployee, "Employee created successfully.");
            }

            var updatedEmployee = await _service.Update(employee);

            return ApiResponseHelper.Ok(updatedEmployee, "Employee updated successfully.");
        }

        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);

            if (!deleted)
            {
                return ApiResponseHelper.NotFound(
                    "Employee not found.",
                    [$"Employee with ID {id} was not found."]);
            }

            return ApiResponseHelper.Ok(Array.Empty<object>(), "Employee deleted successfully.");
        }
    }
}
