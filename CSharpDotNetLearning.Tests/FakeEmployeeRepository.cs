using CSharpDotNetLearning.Models;
using CSharpDotNetLearning.Repositories;

namespace CSharpDotNetLearning.Tests;

public class FakeEmployeeRepository : IEmployeeRepository
{
    private List<Employee> _employees = new();

    public List<Employee> Load()
    {
        return _employees;
    }

    public void Save(List<Employee> employees)
    {
        _employees = employees;
    }
}