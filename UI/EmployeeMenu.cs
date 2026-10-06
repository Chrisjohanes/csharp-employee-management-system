using CSharpDotNetLearning.Models;
using CSharpDotNetLearning.Services;

namespace CSharpDotNetLearning.UI;

public class EmployeeMenu
{
    private readonly EmployeeService _employeeService;

    public EmployeeMenu(
        EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public void Run()
    {
        while (true)
        {
            ShowMenu();

            int menu = InputHelper.ReadPositiveInt(
                "  Select menu: "
            );

            Console.WriteLine();

            switch (menu)
            {
                case 1:
                    EmployeeDashboard.Show(
                        _employeeService
                    );
                    break;

                case 2:
                    ViewEmployees();
                    break;

                case 3:
                    AddEmployee();
                    break;

                case 4:
                    SearchEmployee();
                    break;

                case 5:
                    FilterEmployees();
                    break;

                case 6:
                    UpdateEmployee();
                    break;

                case 7:
                    DeleteEmployee();
                    break;

                case 8:
                    SortEmployees();
                    break;

                case 9:
                    return;

                default:
                    ConsoleHelper.ShowError(
                        "Invalid menu. Please choose 1-9."
                    );
                    break;
            }

            ConsoleHelper.Pause();
        }
    }

    private void ShowMenu()
    {
        ConsoleHelper.ShowHeader("MAIN MENU");

        ConsoleHelper.WriteBoxTop();
        ConsoleHelper.WriteBoxTitle("MAIN MENU");

        ConsoleHelper.WriteMenuItem(
            1,
            "Dashboard"
        );

        ConsoleHelper.WriteMenuItem(
            2,
            "View Employees"
        );

        ConsoleHelper.WriteMenuItem(
            3,
            "Add Employee"
        );

        ConsoleHelper.WriteMenuItem(
            4,
            "Search Employee"
        );

        ConsoleHelper.WriteMenuItem(
            5,
            "Filter Employees"
        );

        ConsoleHelper.WriteMenuItem(
            6,
            "Update Employee"
        );

        ConsoleHelper.WriteMenuItem(
            7,
            "Delete Employee"
        );

        ConsoleHelper.WriteMenuItem(
            8,
            "Sort Employees"
        );

        ConsoleHelper.WriteMenuItem(
            9,
            "Exit"
        );

        ConsoleHelper.WriteBoxBottom();

        Console.WriteLine();
    }

    private void ViewEmployees()
    {
        ConsoleHelper.ShowHeader(
            "VIEW EMPLOYEES"
        );

        EmployeeDisplay.ShowList(
            _employeeService.Employees
        );
    }

    private void AddEmployee()
    {
        ConsoleHelper.ShowHeader(
            "ADD EMPLOYEE"
        );

        string name =
            InputHelper.ReadRequiredString(
                "  Enter employee name: "
            );

        string position =
            InputHelper.ReadRequiredString(
                "  Enter employee position: "
            );

        int age =
            InputHelper.ReadAge();

        decimal salary =
            InputHelper.ReadNonNegativeDecimal(
                "  Enter employee salary: "
            );

        Employee employee =
            _employeeService.AddEmployee(
                name,
                position,
                age,
                salary
            );

        Console.WriteLine();

        ConsoleHelper.ShowSuccess(
            "Employee added successfully."
        );

        Console.WriteLine(
            $"  Employee ID : {employee.Id}"
        );
    }

    private void SearchEmployee()
    {
        while (true)
        {
            ConsoleHelper.ShowHeader(
                "SEARCH EMPLOYEE"
            );

            Console.WriteLine(
                "1. Search by ID"
            );

            Console.WriteLine(
                "2. Search by Name"
            );

            Console.WriteLine(
                "3. Back"
            );

            Console.WriteLine();

            int menu =
                InputHelper.ReadPositiveInt(
                    "  Choose search: "
                );

            Console.WriteLine();

            switch (menu)
            {
                case 1:
                    SearchEmployeeById();
                    ConsoleHelper.Pause();
                    break;

                case 2:
                    SearchEmployeeByName();
                    ConsoleHelper.Pause();
                    break;

                case 3:
                    return;

                default:
                    ConsoleHelper.ShowError(
                        "Invalid menu. Please choose 1-3."
                    );

                    ConsoleHelper.Pause();
                    break;
            }
        }
    }

    private void SearchEmployeeById()
    {
        ConsoleHelper.ShowHeader(
            "SEARCH BY ID"
        );

        int id =
            InputHelper.ReadEmployeeId();

        Employee? employee =
            _employeeService.FindById(id);

        Console.WriteLine();

        if (employee == null)
        {
            ConsoleHelper.ShowError(
                "Employee not found."
            );

            return;
        }

        ConsoleHelper.ShowSuccess(
            "Employee found:"
        );

        Console.WriteLine();

        EmployeeDisplay.ShowSingle(
            employee
        );
    }

    private void SearchEmployeeByName()
    {
        ConsoleHelper.ShowHeader(
            "SEARCH BY NAME"
        );

        string name =
            InputHelper.ReadRequiredString(
                "  Enter employee name: "
            );

        List<Employee> employees =
            _employeeService.SearchByName(name);

        Console.WriteLine();

        if (employees.Count == 0)
        {
            ConsoleHelper.ShowError(
                "Employee not found."
            );

            return;
        }

        Console.WriteLine(
            $"Found {employees.Count} employee(s):"
        );

        Console.WriteLine();

        EmployeeDisplay.ShowList(
            employees
        );
    }

    private void FilterEmployees()
    {
        while (true)
        {
            ConsoleHelper.ShowHeader(
                "FILTER EMPLOYEES"
            );

            Console.WriteLine(
                "1. Filter by Position"
            );

            Console.WriteLine(
                "2. Back"
            );

            Console.WriteLine();

            int menu =
                InputHelper.ReadPositiveInt(
                    "  Choose filter: "
                );

            Console.WriteLine();

            switch (menu)
            {
                case 1:
                    FilterByPosition();
                    ConsoleHelper.Pause();
                    break;

                case 2:
                    return;

                default:
                    ConsoleHelper.ShowError(
                        "Invalid menu. Please choose 1-2."
                    );

                    ConsoleHelper.Pause();
                    break;
            }
        }
    }

    private void FilterByPosition()
    {
        string position =
            InputHelper.ReadRequiredString(
                "  Enter position: "
            );

        List<Employee> employees =
            _employeeService.FilterByPosition(
                position
            );

        Console.WriteLine();

        if (employees.Count == 0)
        {
            ConsoleHelper.ShowWarning(
                $"No employees found for position '{position}'."
            );

            return;
        }

        EmployeeDisplay.ShowList(
            employees
        );
    }

    private void UpdateEmployee()
    {
        ConsoleHelper.ShowHeader(
            "UPDATE EMPLOYEE"
        );

        int id =
            InputHelper.ReadEmployeeId();

        Employee? employee =
            _employeeService.FindById(id);

        if (employee == null)
        {
            ConsoleHelper.ShowError(
                "Employee not found."
            );

            return;
        }

        Console.WriteLine();
        Console.WriteLine(
            "Current employee data:"
        );

        Console.WriteLine();

        EmployeeDisplay.ShowSingle(
            employee
        );

        Console.WriteLine();
        Console.WriteLine(
            "Enter new employee data:"
        );

        Console.WriteLine();

        string name =
            InputHelper.ReadRequiredString(
                "  Enter employee name: "
            );

        string position =
            InputHelper.ReadRequiredString(
                "  Enter employee position: "
            );

        int age =
            InputHelper.ReadAge();

        decimal salary =
            InputHelper.ReadNonNegativeDecimal(
                "  Enter employee salary: "
            );

        bool isUpdated =
            _employeeService.UpdateEmployee(
                id,
                name,
                position,
                age,
                salary
            );

        Console.WriteLine();

        if (isUpdated)
        {
            ConsoleHelper.ShowSuccess(
                "Employee updated successfully."
            );
        }
        else
        {
            ConsoleHelper.ShowError(
                "Failed to update employee."
            );
        }
    }

    private void DeleteEmployee()
    {
        ConsoleHelper.ShowHeader(
            "DELETE EMPLOYEE"
        );

        int id =
            InputHelper.ReadEmployeeId();

        Employee? employee =
            _employeeService.FindById(id);

        if (employee == null)
        {
            ConsoleHelper.ShowError(
                "Employee not found."
            );

            return;
        }

        Console.WriteLine();
        Console.WriteLine(
            "Employee to delete:"
        );

        Console.WriteLine();

        EmployeeDisplay.ShowSingle(
            employee
        );

        Console.WriteLine();

        string confirmation =
            InputHelper.ReadRequiredString(
                "  Are you sure? (y/n): "
            );

        if (!confirmation.Equals(
                "y",
                StringComparison.OrdinalIgnoreCase))
        {
            ConsoleHelper.ShowWarning(
                "Delete cancelled."
            );

            return;
        }

        bool isDeleted =
            _employeeService.DeleteEmployee(id);

        Console.WriteLine();

        if (isDeleted)
        {
            ConsoleHelper.ShowSuccess(
                "Employee deleted successfully."
            );
        }
        else
        {
            ConsoleHelper.ShowError(
                "Failed to delete employee."
            );
        }
    }

    private void SortEmployees()
    {
        while (true)
        {
            ConsoleHelper.ShowHeader(
                "SORT EMPLOYEES"
            );

            Console.WriteLine(
                "1. Sort by Name"
            );

            Console.WriteLine(
                "2. Sort by Age"
            );

            Console.WriteLine(
                "3. Sort by Salary"
            );

            Console.WriteLine(
                "4. Back"
            );

            Console.WriteLine();

            int menu =
                InputHelper.ReadPositiveInt(
                    "  Choose sort: "
                );

            Console.WriteLine();

            switch (menu)
            {
                case 1:
                    SortByName();
                    ConsoleHelper.Pause();
                    break;

                case 2:
                    SortByAge();
                    ConsoleHelper.Pause();
                    break;

                case 3:
                    SortBySalary();
                    ConsoleHelper.Pause();
                    break;

                case 4:
                    return;

                default:
                    ConsoleHelper.ShowError(
                        "Invalid menu. Please choose 1-4."
                    );

                    ConsoleHelper.Pause();
                    break;
            }
        }
    }

    private void SortByName()
    {
        ConsoleHelper.ShowHeader(
            "SORT BY NAME"
        );

        Console.WriteLine("1. A-Z");
        Console.WriteLine("2. Z-A");
        Console.WriteLine();

        int menu =
            InputHelper.ReadPositiveInt(
                "  Choose order: "
            );

        Console.WriteLine();

        List<Employee> employees;

        if (menu == 1)
        {
            employees =
                _employeeService.Employees
                    .OrderBy(
                        employee => employee.Name
                    )
                    .ToList();
        }
        else if (menu == 2)
        {
            employees =
                _employeeService.Employees
                    .OrderByDescending(
                        employee => employee.Name
                    )
                    .ToList();
        }
        else
        {
            ConsoleHelper.ShowError(
                "Invalid order."
            );

            return;
        }

        EmployeeDisplay.ShowList(
            employees
        );
    }

    private void SortByAge()
    {
        ConsoleHelper.ShowHeader(
            "SORT BY AGE"
        );

        Console.WriteLine(
            "1. Youngest to Oldest"
        );

        Console.WriteLine(
            "2. Oldest to Youngest"
        );

        Console.WriteLine();

        int menu =
            InputHelper.ReadPositiveInt(
                "  Choose order: "
            );

        Console.WriteLine();

        List<Employee> employees;

        if (menu == 1)
        {
            employees =
                _employeeService.Employees
                    .OrderBy(
                        employee => employee.Age
                    )
                    .ToList();
        }
        else if (menu == 2)
        {
            employees =
                _employeeService.Employees
                    .OrderByDescending(
                        employee => employee.Age
                    )
                    .ToList();
        }
        else
        {
            ConsoleHelper.ShowError(
                "Invalid order."
            );

            return;
        }

        EmployeeDisplay.ShowList(
            employees
        );
    }

    private void SortBySalary()
    {
        ConsoleHelper.ShowHeader(
            "SORT BY SALARY"
        );

        Console.WriteLine(
            "1. Lowest to Highest"
        );

        Console.WriteLine(
            "2. Highest to Lowest"
        );

        Console.WriteLine();

        int menu =
            InputHelper.ReadPositiveInt(
                "  Choose order: "
            );

        Console.WriteLine();

        List<Employee> employees;

        if (menu == 1)
        {
            employees =
                _employeeService.Employees
                    .OrderBy(
                        employee => employee.Salary
                    )
                    .ToList();
        }
        else if (menu == 2)
        {
            employees =
                _employeeService.Employees
                    .OrderByDescending(
                        employee => employee.Salary
                    )
                    .ToList();
        }
        else
        {
            ConsoleHelper.ShowError(
                "Invalid order."
            );

            return;
        }

        EmployeeDisplay.ShowList(
            employees
        );
    }
}