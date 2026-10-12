using CSharpDotNetLearning.Repositories;
using CSharpDotNetLearning.Services;
using CSharpDotNetLearning.UI;

IEmployeeRepository employeeRepository =
    new EmployeeRepository();

EmployeeService employeeService =
    new EmployeeService(
        employeeRepository
    );

EmployeeMenu employeeMenu =
    new EmployeeMenu(
        employeeService
    );

employeeMenu.Run();