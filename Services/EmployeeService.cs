using System.Text.Json;
using CSharpDotNetLearning.Models;

namespace CSharpDotNetLearning.Services;

public class EmployeeService
{
    private readonly string _filePath = "Data/employees.json";

    public List<Employee> Employees { get; private set; } = new();

    public EmployeeService()
    {
        LoadData();
    }


    // ========================================
    // LOAD DATA
    // ========================================

    public void LoadData()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                CreateInitialData();
                SaveData();
                return;
            }

            string json = File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                CreateInitialData();
                SaveData();
                return;
            }

            Employees = JsonSerializer.Deserialize<List<Employee>>(json)
                        ?? new List<Employee>();
        }
        catch (JsonException)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Error: Employee data file contains invalid JSON."
            );

            Console.WriteLine(
                "The application will use empty employee data."
            );

            Console.WriteLine();

            Employees = new List<Employee>();
        }
        catch (IOException)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Error: Unable to read employee data file."
            );

            Console.WriteLine(
                "Please check the file or folder permissions."
            );

            Console.WriteLine();

            Employees = new List<Employee>();
        }
        catch (Exception)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Error: Failed to load employee data."
            );

            Console.WriteLine();

            Employees = new List<Employee>();
        }
    }


    // ========================================
    // SAVE DATA
    // ========================================

    public void SaveData()
    {
        try
        {
            string? directory = Path.GetDirectoryName(_filePath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(
                Employees,
                options
            );

            File.WriteAllText(_filePath, json);
        }
        catch (IOException)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Error: Unable to save employee data."
            );

            Console.WriteLine(
                "Please check the file or folder permissions."
            );

            Console.WriteLine();
        }
        catch (Exception)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Error: Failed to save employee data."
            );

            Console.WriteLine();
        }
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
            : Employees.Max(employee => employee.Id) + 1;

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

    public List<Employee> SearchByName(string name)
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

    public List<Employee> FilterByPosition(string position)
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

    public Dictionary<string, int> GroupByPosition()
    {
        return Employees
            .GroupBy(employee => employee.Position)
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