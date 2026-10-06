using System.Text.Json;
using CSharpDotNetLearning.Models;

namespace CSharpDotNetLearning.Repositories;

public class EmployeeRepository
{
    private readonly string _filePath;

    public EmployeeRepository(
        string filePath = "Data/employees.json")
    {
        _filePath = filePath;
    }

    public List<Employee> Load()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                return new List<Employee>();
            }

            string json =
                File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Employee>();
            }

            return JsonSerializer.Deserialize<List<Employee>>(
                json
            ) ?? new List<Employee>();
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

            return new List<Employee>();
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

            return new List<Employee>();
        }
        catch (Exception)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Error: Failed to load employee data."
            );
            Console.WriteLine();

            return new List<Employee>();
        }
    }

    public void Save(
        List<Employee> employees)
    {
        try
        {
            string? directory =
                Path.GetDirectoryName(_filePath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(
                    directory
                );
            }

            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            string json =
                JsonSerializer.Serialize(
                    employees,
                    options
                );

            File.WriteAllText(
                _filePath,
                json
            );
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
}