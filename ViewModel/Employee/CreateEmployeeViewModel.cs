using System.ComponentModel.DataAnnotations;
namespace EmployeeApi.ViewModel.Employee;

public class CreateEmployeeViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;
    [Range(0, double.MaxValue)]
    public decimal Salary { get; set; }
}
