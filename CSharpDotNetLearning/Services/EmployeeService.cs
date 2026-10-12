using CSharpDotNetLearning.Models;
using CSharpDotNetLearning.Repositories;

namespace CSharpDotNetLearning.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public List<Employee> Employees { get; private set; } = new();

    public EmployeeService(
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;

        LoadData();
    }


    // ========================================
    // LOAD DATA
    // ========================================

    private void LoadData()
    {
        Employees = _employeeRepository.Load();

        if (Employees.Count == 0)
        {
            CreateInitialData();

            SaveData();
        }
    }


    // ========================================
    // SAVE DATA
    // ========================================

    private void SaveData()
    {
        _employeeRepository.Save(Employees);
    }


    // ========================================
    // CREATE INITIAL DATA
    // ========================================

    private void CreateInitialData()
    {
        Employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "Christian",
                Position = "IT Developer",
                Age = 24,
                Salary = 7500000
            },

            new Employee
            {
                Id = 2,
                Name = "Budi",
                Position = "Software Developer",
                Age = 25,
                Salary = 8000000
            },

            new Employee
            {
                Id = 3,
                Name = "Andi",
                Position = "Software Engineer",
                Age = 23,
                Salary = 8500000
            },

            new Employee
            {
                Id = 4,
                Name = "Doni",
                Position = "QA Engineer",
                Age = 26,
                Salary = 7000000
            }
        };
    }


    // ========================================
    // ADD EMPLOYEE
    // ========================================

    public Employee AddEmployee(
        string name,
        string position,
        int age,
        decimal salary)
    {
        int newId = Employees.Count == 0
            ? 1
            : Employees.Max(
                employee => employee.Id
            ) + 1;

        Employee employee = new()
        {
            Id = newId,
            Name = name,
            Position = position,
            Age = age,
            Salary = salary
        };

        Employees.Add(employee);

        SaveData();

        return employee;
    }


    // ========================================
    // SEARCH EMPLOYEE BY NAME
    // ========================================

    public List<Employee> SearchByName(
        string name)
    {
        return Employees
            .Where(employee =>
                employee.Name.Contains(
                    name,
                    StringComparison.OrdinalIgnoreCase
                )
            )
            .ToList();
    }


    // ========================================
    // FILTER EMPLOYEE BY POSITION
    // ========================================

    public List<Employee> FilterByPosition(
        string position)
    {
        return Employees
            .Where(employee =>
                employee.Position.Contains(
                    position,
                    StringComparison.OrdinalIgnoreCase
                )
            )
            .ToList();
    }


    // ========================================
    // GROUP EMPLOYEE BY POSITION
    // ========================================

    public Dictionary<string, int> GroupByPosition()
    {
        return Employees
            .GroupBy(
                employee => employee.Position
            )
            .ToDictionary(
                group => group.Key,
                group => group.Count()
            );
    }


    // ========================================
    // FIND EMPLOYEE BY ID
    // ========================================

    public Employee? FindById(int id)
    {
        return Employees.FirstOrDefault(
            employee => employee.Id == id
        );
    }


    // ========================================
    // UPDATE EMPLOYEE
    // ========================================

    public bool UpdateEmployee(
        int id,
        string name,
        string position,
        int age,
        decimal salary)
    {
        Employee? employee = FindById(id);

        if (employee == null)
        {
            return false;
        }

        employee.Name = name;
        employee.Position = position;
        employee.Age = age;
        employee.Salary = salary;

        SaveData();

        return true;
    }


    // ========================================
    // DELETE EMPLOYEE
    // ========================================

    public bool DeleteEmployee(int id)
    {
        Employee? employee = FindById(id);

        if (employee == null)
        {
            return false;
        }

        Employees.Remove(employee);

        SaveData();

        return true;
    }
}