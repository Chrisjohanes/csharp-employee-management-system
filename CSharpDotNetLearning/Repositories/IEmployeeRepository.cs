using CSharpDotNetLearning.Models;

namespace CSharpDotNetLearning.Repositories;

public interface IEmployeeRepository
{
    List<Employee> Load();

    void Save(List<Employee> employees);
}