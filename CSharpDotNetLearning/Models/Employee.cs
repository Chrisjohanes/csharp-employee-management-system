
using System.ComponentModel.DataAnnotations;

namespace CSharpDotNetLearning.Models;

public class Employee
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Employee name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Employee position is required.")]
    [StringLength(100, ErrorMessage = "Position cannot exceed 100 characters.")]
    public string Position { get; set; } = string.Empty;

    [Range(18, 65, ErrorMessage = "Age must be between 18 and 65.")]
    public int Age { get; set; }

    [Range(
        typeof(decimal),
        "0.01",
        "999999999999999.99",
        ErrorMessage = "Salary must be greater than zero."
    )]
    public decimal Salary { get; set; }
}
