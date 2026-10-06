using CSharpDotNetLearning.Services;
using CSharpDotNetLearning.UI;

EmployeeService employeeService = new();

EmployeeMenu employeeMenu = new(employeeService);

employeeMenu.Run();