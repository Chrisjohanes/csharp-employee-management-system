using CSharpDotNetLearning.Repositories;
using CSharpDotNetLearning.Services;
using CSharpDotNetLearning.UI;

EmployeeRepository employeeRepository = new();

EmployeeService employeeService = new(
    employeeRepository
);

EmployeeMenu employeeMenu = new(
    employeeService
);

employeeMenu.Run();