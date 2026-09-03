using EmployeeApi.Model.Employee;
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
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetById(id);

            return employee is null
                ? NotFound()
                : Ok(employee);
        }

        [HttpPost("create/edit")]
        public async Task<IActionResult> Create(
            [FromBody] CreateEmployeeViewModel model)
        {
            if (model.Id != 0 && await _service.GetById(model.Id) is null)
            {
                return NotFound($"Employee with ID {model.Id} was not found.");
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

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdEmployee.Id },
                    createdEmployee
                );
            }

            return Ok(await _service.Update(employee));
        }

        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _service.Delete(id)
                ? NoContent()
                : NotFound();
        }
    }
}
