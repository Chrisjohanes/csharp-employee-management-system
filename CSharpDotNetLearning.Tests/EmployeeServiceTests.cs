using CSharpDotNetLearning.Models;
using CSharpDotNetLearning.Repositories;
using CSharpDotNetLearning.Services;

namespace CSharpDotNetLearning.Tests;

public class EmployeeServiceTests
{
    [Fact]
    public void AddEmployee_ShouldAddEmployee()
    {
        // Arrange
        IEmployeeRepository repository =
            new FakeEmployeeRepository();

        EmployeeService service =
            new EmployeeService(repository);

        // Act
        Employee employee =
            service.AddEmployee(
                "Budi",
                "Software Developer",
                25,
                8000000
            );

        // Assert
        Assert.Equal("Budi", employee.Name);
        Assert.Equal("Software Developer", employee.Position);
        Assert.Equal(25, employee.Age);
        Assert.Equal(8000000, employee.Salary);
    }

    [Fact]
    public void FindById_ShouldReturnEmployee_WhenIdExists()
    {
        // Arrange
        IEmployeeRepository repository =
            new FakeEmployeeRepository();

        EmployeeService service =
            new EmployeeService(repository);

        Employee addedEmployee =
            service.AddEmployee(
                "Budi",
                "Software Developer",
                25,
                8000000
            );

        // Act
        Employee? result =
            service.FindById(addedEmployee.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(addedEmployee.Id, result.Id);
        Assert.Equal("Budi", result.Name);
    }

    [Fact]
    public void FindById_ShouldReturnNull_WhenIdDoesNotExist()
    {
        // Arrange
        IEmployeeRepository repository =
            new FakeEmployeeRepository();

        EmployeeService service =
            new EmployeeService(repository);

        // Act
        Employee? result =
            service.FindById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateEmployee_ShouldUpdateEmployee()
    {
        // Arrange
        IEmployeeRepository repository =
            new FakeEmployeeRepository();

        EmployeeService service =
            new EmployeeService(repository);

        Employee employee =
            service.AddEmployee(
                "Budi",
                "Software Developer",
                25,
                8000000
            );

        // Act
        bool result =
            service.UpdateEmployee(
                employee.Id,
                "Budi Santoso",
                "Senior Software Developer",
                26,
                10000000
            );

        // Assert
        Assert.True(result);
        Assert.Equal("Budi Santoso", employee.Name);
        Assert.Equal(
            "Senior Software Developer",
            employee.Position
        );
        Assert.Equal(26, employee.Age);
        Assert.Equal(10000000, employee.Salary);
    }

    [Fact]
    public void UpdateEmployee_ShouldReturnFalse_WhenIdDoesNotExist()
    {
        // Arrange
        IEmployeeRepository repository =
            new FakeEmployeeRepository();

        EmployeeService service =
            new EmployeeService(repository);

        // Act
        bool result =
            service.UpdateEmployee(
                999,
                "Budi Santoso",
                "Senior Software Developer",
                26,
                10000000
            );

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void DeleteEmployee_ShouldDeleteEmployee()
    {
        // Arrange
        IEmployeeRepository repository =
            new FakeEmployeeRepository();

        EmployeeService service =
            new EmployeeService(repository);

        Employee employee =
            service.AddEmployee(
                "Budi",
                "Software Developer",
                25,
                8000000
            );

        // Act
        bool result =
            service.DeleteEmployee(employee.Id);

        // Assert
        Assert.True(result);

        Assert.DoesNotContain(
            service.Employees,
            item => item.Id == employee.Id
        );
    }

    [Fact]
    public void DeleteEmployee_ShouldReturnFalse_WhenIdDoesNotExist()
    {
        // Arrange
        IEmployeeRepository repository =
            new FakeEmployeeRepository();

        EmployeeService service =
            new EmployeeService(repository);

        // Act
        bool result =
            service.DeleteEmployee(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void SearchByName_ShouldReturnMatchingEmployees()
    {
        // Arrange
        IEmployeeRepository repository =
            new FakeEmployeeRepository();

        EmployeeService service =
            new EmployeeService(repository);

        service.AddEmployee(
            "Rudi",
            "Software Developer",
            25,
            8000000
        );

        service.AddEmployee(
            "Rudi Santoso",
            "Senior Developer",
            28,
            10000000
        );

        service.AddEmployee(
            "Andi",
            "QA Engineer",
            26,
            7000000
        );

        // Act
        List<Employee> results =
            service.SearchByName("Rudi");

        // Assert
        Assert.Equal(2, results.Count);

        Assert.All(
            results,
            employee => Assert.Contains(
                "Rudi",
                employee.Name,
                StringComparison.OrdinalIgnoreCase
            )
        );
    }

    [Fact]
    public void FilterByPosition_ShouldReturnMatchingEmployees()
    {
        // Arrange
        IEmployeeRepository repository =
            new FakeEmployeeRepository();

        EmployeeService service =
            new EmployeeService(repository);

        service.AddEmployee(
            "Rudi",
            "Software Developer",
            25,
            8000000
        );

        service.AddEmployee(
            "Andi",
            "QA Engineer",
            26,
            7000000
        );

        service.AddEmployee(
            "Sinta",
            "Senior Developer",
            28,
            10000000
        );

        // Act
        List<Employee> results =
            service.FilterByPosition("Developer");

        // Assert
        Assert.Contains(
            results,
            employee => employee.Name == "Rudi"
        );

        Assert.Contains(
            results,
            employee => employee.Name == "Sinta"
        );

        Assert.DoesNotContain(
            results,
            employee => employee.Name == "Andi"
        );
    }

    [Fact]
    public void GroupByPosition_ShouldReturnCorrectCounts()
    {
        // Arrange
        IEmployeeRepository repository =
            new FakeEmployeeRepository();

        EmployeeService service =
            new EmployeeService(repository);

        service.AddEmployee(
            "Rudi",
            "Software Developer",
            25,
            8000000
        );

        service.AddEmployee(
            "Sinta",
            "Software Developer",
            27,
            9000000
        );

        service.AddEmployee(
            "Andi",
            "QA Engineer",
            26,
            7000000
        );

        // Act
        Dictionary<string, int> results =
            service.GroupByPosition();

        // Assert
        Assert.True(
            results.ContainsKey("Software Developer")
        );

        Assert.True(
            results.ContainsKey("QA Engineer")
        );

        Assert.True(
            results["Software Developer"] >= 2
        );

        Assert.True(
            results["QA Engineer"] >= 1
        );
    }

    [Fact]
    public void SearchByName_ShouldIgnoreCase()
    {
        // Arrange
        IEmployeeRepository repository =
            new FakeEmployeeRepository();

        EmployeeService service =
            new EmployeeService(repository);

        service.AddEmployee(
            "Rudi",
            "Software Developer",
            25,
            8000000
        );

        // Act
        List<Employee> results =
            service.SearchByName("rudi");

        // Assert
        Assert.Contains(
            results,
            employee => employee.Name == "Rudi"
        );
    }

    [Fact]
    public void FilterByPosition_ShouldIgnoreCase()
    {
        // Arrange
        IEmployeeRepository repository =
            new FakeEmployeeRepository();

        EmployeeService service =
            new EmployeeService(repository);

        service.AddEmployee(
            "Rudi",
            "Software Developer",
            25,
            8000000
        );

        // Act
        List<Employee> results =
            service.FilterByPosition("developer");

        // Assert
        Assert.Contains(
            results,
            employee => employee.Name == "Rudi"
        );
    }

    [Fact]
    public void AddEmployee_ShouldGenerateUniqueIds()
    {
        // Arrange
        IEmployeeRepository repository =
            new FakeEmployeeRepository();

        EmployeeService service =
            new EmployeeService(repository);

        // Act
        Employee firstEmployee =
            service.AddEmployee(
                "Rudi",
                "Software Developer",
                25,
                8000000
            );

        Employee secondEmployee =
            service.AddEmployee(
                "Sinta",
                "QA Engineer",
                27,
                9000000
            );

        // Assert
        Assert.NotEqual(
            firstEmployee.Id,
            secondEmployee.Id
        );
    }
}