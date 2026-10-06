using CSharpDotNetLearning.Models;
using CSharpDotNetLearning.Services;

namespace CSharpDotNetLearning.UI;

public class EmployeeMenu
{
    private readonly EmployeeService _employeeService;

    public EmployeeMenu(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    // ========================================
    // RUN APPLICATION
    // ========================================

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
                    ShowDashboard();
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
                    ShowError("Invalid menu. Please choose 1-9.");
                    break;
            }

            if (menu != 9)
            {
                Pause();
            }
        }
    }

    // ========================================
    // UI HELPERS
    // ========================================

    private const int BoxWidth = 50;
    private const int NameColumnWidth = 15;
    private const int PositionColumnWidth = 23;
    private const int PositionDashboardWidth = 25;

    private void ShowHeader(string title)
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║              EMPLOYEE MANAGEMENT SYSTEM              ║");
        Console.WriteLine("║                    C# / .NET                         ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        Console.ResetColor();

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"  {title}");
        Console.ResetColor();

        Console.WriteLine();
    }

    private void ShowSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  ✓ {message}");
        Console.ResetColor();
    }

    private void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  ✗ {message}");
        Console.ResetColor();
    }

    private void ShowWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  ⚠ {message}");
        Console.ResetColor();
    }

    private void Pause()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("  Press ENTER to continue...");
        Console.ResetColor();
        Console.ReadLine();
    }

    private void WriteBoxTop()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"  ┌{new string('─', BoxWidth)}┐");
        Console.ResetColor();
    }

    private void WriteBoxTitle(string title)
    {
        string content = CenterText(title, BoxWidth);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"  │{content}│");
        Console.WriteLine($"  ├{new string('─', BoxWidth)}┤");
        Console.ResetColor();
    }

    private void WriteBoxLine(string content)
    {
        if (content.Length > BoxWidth)
        {
            content = content[..BoxWidth];
        }

        Console.WriteLine(
            $"  │{content.PadRight(BoxWidth)}│"
        );
    }

    private void WriteBoxBottom()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"  └{new string('─', BoxWidth)}┘");
        Console.ResetColor();
    }

    private string CenterText(string text, int width)
    {
        if (text.Length >= width)
        {
            return text[..width];
        }

        int leftPadding = (width - text.Length) / 2;
        int rightPadding = width - text.Length - leftPadding;

        return new string(' ', leftPadding)
            + text
            + new string(' ', rightPadding);
    }

    // ========================================
    // MENU HELPERS
    // ========================================

    private void WriteMenuItem(int number, string label)
    {
        WriteBoxLine($"  [{number}]  {label}");
    }

    // ========================================
    // SHOW MAIN MENU
    // ========================================

    private void ShowMenu()
    {
        ShowHeader("MAIN MENU");

        WriteBoxTop();
        WriteBoxTitle("MAIN MENU");

        WriteMenuItem(1, "Dashboard");
        WriteMenuItem(2, "View Employees");
        WriteMenuItem(3, "Add Employee");
        WriteMenuItem(4, "Search Employee");
        WriteMenuItem(5, "Filter Employees");
        WriteMenuItem(6, "Update Employee");
        WriteMenuItem(7, "Delete Employee");
        WriteMenuItem(8, "Sort Employees");
        WriteMenuItem(9, "Exit");

        WriteBoxBottom();

        Console.WriteLine();
    }

    // ========================================
    // DASHBOARD
    // ========================================

    private void ShowDashboard()
    {
        ShowHeader("EMPLOYEE DASHBOARD");

        List<Employee> employees =
            _employeeService.Employees;

        if (employees.Count == 0)
        {
            ShowWarning("No employee data available.");
            return;
        }

        // ========================================
        // BASIC STATISTICS
        // ========================================

        int totalEmployees = employees.Count;

        decimal averageAge = employees
            .Average(employee => (decimal)employee.Age);

        decimal averageSalary = employees
            .Average(employee => employee.Salary);

        decimal highestSalary = employees
            .Max(employee => employee.Salary);

        decimal lowestSalary = employees
            .Min(employee => employee.Salary);

        // ========================================
        // EMPLOYEE HIGHLIGHTS
        // ========================================

        Employee highestPaidEmployee = employees
            .MaxBy(employee => employee.Salary)!;

        Employee youngestEmployee = employees
            .MinBy(employee => employee.Age)!;

        Employee oldestEmployee = employees
            .MaxBy(employee => employee.Age)!;

        // ========================================
        // SUMMARY
        // ========================================

        WriteBoxTop();
        WriteBoxTitle("SUMMARY");

        WriteBoxLine(
            $"  Total Employees     : {totalEmployees}"
        );

        WriteBoxLine(
            $"  Average Age         : {averageAge:N2} years"
        );

        WriteBoxLine(
            $"  Average Salary      : Rp {averageSalary:N0}"
        );

        WriteBoxLine(
            $"  Highest Salary      : Rp {highestSalary:N0}"
        );

        WriteBoxLine(
            $"  Lowest Salary       : Rp {lowestSalary:N0}"
        );

        WriteBoxBottom();

        Console.WriteLine();

        // ========================================
        // TOP EMPLOYEES
        // ========================================

        WriteBoxTop();
        WriteBoxTitle("TOP EMPLOYEES");

        WriteBoxLine(
            $"  Highest Paid        : {highestPaidEmployee.Name,-21}"
        );

        WriteBoxLine(
            $"  Youngest Employee   : {youngestEmployee.Name,-21}"
        );

        WriteBoxLine(
            $"  Oldest Employee     : {oldestEmployee.Name,-21}"
        );

        WriteBoxBottom();

        Console.WriteLine();

        // ========================================
        // EMPLOYEE BY POSITION
        // ========================================

        Dictionary<string, int> employeesByPosition =
            _employeeService.GroupByPosition();

        WriteBoxTop();
        WriteBoxTitle("EMPLOYEE BY POSITION");

        foreach (KeyValuePair<string, int> item in employeesByPosition)
        {
            string position = TruncateText(
                item.Key,
                PositionDashboardWidth
            );

            WriteBoxLine(
                $"  {position,-25} : {item.Value}"
            );
        }

        WriteBoxBottom();
    }

    // ========================================
    // VIEW EMPLOYEES
    // ========================================

    private void ViewEmployees()
    {
        ShowHeader("VIEW EMPLOYEES");

        DisplayEmployeeList(
            _employeeService.Employees
        );
    }

    // ========================================
    // ADD EMPLOYEE
    // ========================================

    private void AddEmployee()
    {
        ShowHeader("ADD EMPLOYEE");

        string name = InputHelper.ReadRequiredString(
            "  Enter employee name: "
        );

        string position = InputHelper.ReadRequiredString(
            "  Enter employee position: "
        );

        int age = InputHelper.ReadAge();

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

        ShowSuccess("Employee added successfully.");

        Console.WriteLine(
            $"  Employee ID : {employee.Id}"
        );
    }

    // ========================================
    // SEARCH EMPLOYEE
    // ========================================

    private void SearchEmployee()
    {
        ShowHeader("SEARCH EMPLOYEE");

        while (true)
        {
            Console.WriteLine("1. Search by ID");
            Console.WriteLine("2. Search by Name");
            Console.WriteLine("3. Back");

            Console.WriteLine();

            int menu = InputHelper.ReadPositiveInt(
                "  Choose search: "
            );

            Console.WriteLine();

            switch (menu)
            {
                case 1:
                    SearchEmployeeById();
                    Pause();
                    ShowHeader("SEARCH EMPLOYEE");
                    break;

                case 2:
                    SearchEmployeeByName();
                    Pause();
                    ShowHeader("SEARCH EMPLOYEE");
                    break;

                case 3:
                    return;

                default:
                    ShowError("Invalid menu. Please choose 1-3.");
                    Pause();
                    ShowHeader("SEARCH EMPLOYEE");
                    break;
            }
        }
    }

    // ========================================
    // SEARCH BY ID
    // ========================================

    private void SearchEmployeeById()
    {
        ShowHeader("SEARCH BY ID");

        int id = InputHelper.ReadEmployeeId();

        Employee? employee =
            _employeeService.FindById(id);

        Console.WriteLine();

        if (employee == null)
        {
            ShowError("Employee not found.");

            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("✓ Employee found:");
        Console.ResetColor();

        Console.WriteLine();

        DisplayEmployee(employee);
    }

    // ========================================
    // SEARCH BY NAME
    // ========================================

    private void SearchEmployeeByName()
    {
        ShowHeader("SEARCH BY NAME");

        string name = InputHelper.ReadRequiredString(
            "  Enter employee name: "
        );

        List<Employee> employees =
            _employeeService.SearchByName(name);

        Console.WriteLine();

        if (employees.Count == 0)
        {
            ShowError("Employee not found.");

            return;
        }

        Console.WriteLine(
            $"Found {employees.Count} employee(s):"
        );

        Console.WriteLine();

        DisplayEmployeeList(employees);
    }

    private void FilterEmployees()
    {
        ShowHeader("FILTER EMPLOYEES");

        while (true)
        {
            Console.WriteLine("1. Filter by Position");
            Console.WriteLine("2. Back");

            Console.WriteLine();

            int menu = InputHelper.ReadPositiveInt(
                "  Choose filter: "
            );

            Console.WriteLine();

            switch (menu)
            {
                case 1:
                    FilterByPosition();
                    Pause();
                    ShowHeader("FILTER EMPLOYEES");
                    break;

                case 2:
                    return;

                default:
                    ShowError(
                        "Invalid menu. Please choose 1-2."
                    );

                    Pause();
                    ShowHeader("FILTER EMPLOYEES");
                    break;
            }
        }
    }

    private void FilterByPosition()
    {
        string position = InputHelper.ReadRequiredString(
            "  Enter position: "
        );

        List<Employee> employees =
            _employeeService.FilterByPosition(position);

        Console.WriteLine();

        if (employees.Count == 0)
        {
            ShowWarning(
                $"No employees found for position '{position}'."
            );

            return;
        }

        DisplayEmployeeList(employees);
    }

    // ========================================
    // UPDATE EMPLOYEE
    // ========================================

    private void UpdateEmployee()
    {
        ShowHeader("UPDATE EMPLOYEE");

        int id = InputHelper.ReadEmployeeId();

        Employee? employee =
            _employeeService.FindById(id);

        if (employee == null)
        {
            ShowError("Employee not found.");

            return;
        }

        Console.WriteLine();

        Console.WriteLine(
            "Current employee data:"
        );

        Console.WriteLine();

        DisplayEmployee(employee);

        Console.WriteLine();

        Console.WriteLine(
            "Enter new employee data:"
        );

        Console.WriteLine();

        string name = InputHelper.ReadRequiredString(
            "  Enter employee name: "
        );

        string position = InputHelper.ReadRequiredString(
            "  Enter employee position: "
        );

        int age = InputHelper.ReadAge();

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
            ShowSuccess("Employee updated successfully.");
        }
        else
        {
            ShowError("Failed to update employee.");
        }
    }

    // ========================================
    // DELETE EMPLOYEE
    // ========================================

    private void DeleteEmployee()
    {
        ShowHeader("DELETE EMPLOYEE");

        int id = InputHelper.ReadEmployeeId();

        Employee? employee =
            _employeeService.FindById(id);

        if (employee == null)
        {
            ShowError("Employee not found.");

            return;
        }

        Console.WriteLine();

        Console.WriteLine(
            "Employee to delete:"
        );

        Console.WriteLine();

        DisplayEmployee(employee);

        Console.WriteLine();

        string confirmation =
            InputHelper.ReadRequiredString(
                "  Are you sure? (y/n): "
            );

        if (confirmation.Equals(
                "y",
                StringComparison.OrdinalIgnoreCase))
        {
            bool isDeleted =
                _employeeService.DeleteEmployee(id);

            Console.WriteLine();

            if (isDeleted)
            {
                ShowSuccess("Employee deleted successfully.");
            }
            else
            {
                ShowError("Failed to delete employee.");
            }
        }
        else
        {
            ShowWarning("Delete cancelled.");
        }
    }

    // ========================================
    // SORT EMPLOYEES
    // ========================================

    private void SortEmployees()
    {
        ShowHeader("SORT EMPLOYEES");

        while (true)
        {
            Console.WriteLine("1. Sort by Name");
            Console.WriteLine("2. Sort by Age");
            Console.WriteLine("3. Sort by Salary");
            Console.WriteLine("4. Back");

            Console.WriteLine();

            int menu = InputHelper.ReadPositiveInt(
                "  Choose sort: "
            );

            Console.WriteLine();

            switch (menu)
            {
                case 1:
                    SortByName();
                    Pause();
                    ShowHeader("SORT EMPLOYEES");
                    break;

                case 2:
                    SortByAge();
                    Pause();
                    ShowHeader("SORT EMPLOYEES");
                    break;

                case 3:
                    SortBySalary();
                    Pause();
                    ShowHeader("SORT EMPLOYEES");
                    break;

                case 4:
                    return;

                default:
                    ShowError("Invalid menu. Please choose 1-4.");
                    Pause();
                    ShowHeader("SORT EMPLOYEES");
                    break;
            }
        }
    }

    // ========================================
    // SORT BY NAME
    // ========================================

    private void SortByName()
    {
        ShowHeader("SORT BY NAME");

        Console.WriteLine("1. A-Z");
        Console.WriteLine("2. Z-A");

        Console.WriteLine();

        int menu = InputHelper.ReadPositiveInt(
            "  Choose order: "
        );

        Console.WriteLine();

        List<Employee> employees;

        if (menu == 1)
        {
            employees = _employeeService.Employees
                .OrderBy(employee => employee.Name)
                .ToList();
        }
        else if (menu == 2)
        {
            employees = _employeeService.Employees
                .OrderByDescending(
                    employee => employee.Name
                )
                .ToList();
        }
        else
        {
            ShowError("Invalid order.");

            return;
        }

        DisplayEmployeeList(employees);
    }

    // ========================================
    // SORT BY AGE
    // ========================================

    private void SortByAge()
    {
        ShowHeader("SORT BY AGE");

        Console.WriteLine(
            "1. Youngest to Oldest"
        );

        Console.WriteLine(
            "2. Oldest to Youngest"
        );

        Console.WriteLine();

        int menu = InputHelper.ReadPositiveInt(
            "  Choose order: "
        );

        Console.WriteLine();

        List<Employee> employees;

        if (menu == 1)
        {
            employees = _employeeService.Employees
                .OrderBy(employee => employee.Age)
                .ToList();
        }
        else if (menu == 2)
        {
            employees = _employeeService.Employees
                .OrderByDescending(
                    employee => employee.Age
                )
                .ToList();
        }
        else
        {
            ShowError("Invalid order.");

            return;
        }

        DisplayEmployeeList(employees);
    }

    // ========================================
    // SORT BY SALARY
    // ========================================

    private void SortBySalary()
    {
        ShowHeader("SORT BY SALARY");

        Console.WriteLine(
            "1. Lowest to Highest"
        );

        Console.WriteLine(
            "2. Highest to Lowest"
        );

        Console.WriteLine();

        int menu = InputHelper.ReadPositiveInt(
            "  Choose order: "
        );

        Console.WriteLine();

        List<Employee> employees;

        if (menu == 1)
        {
            employees = _employeeService.Employees
                .OrderBy(employee => employee.Salary)
                .ToList();
        }
        else if (menu == 2)
        {
            employees = _employeeService.Employees
                .OrderByDescending(
                    employee => employee.Salary
                )
                .ToList();
        }
        else
        {
            ShowError("Invalid order.");

            return;
        }

        DisplayEmployeeList(employees);
    }

    // ========================================
    // DISPLAY HELPERS
    // ========================================

    private string TruncateText(string value, int maxLength)
    {
        return value.Length > maxLength
            ? value[..maxLength]
            : value;
    }

    // ========================================
    // DISPLAY EMPLOYEE LIST
    // ========================================

    private void DisplayEmployeeList(
        List<Employee> employees)
    {
        if (employees.Count == 0)
        {
            ShowWarning("No employees found.");
            return;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine(
            "  ┌─────┬─────────────────┬─────────────────────────┬──────┬────────────────┐"
        );

        Console.WriteLine(
            "  │ ID  │ Name            │ Position                │ Age  │ Salary         │"
        );

        Console.WriteLine(
            "  ├─────┼─────────────────┼─────────────────────────┼──────┼────────────────┤"
        );

        Console.ResetColor();

        foreach (Employee employee in employees)
        {
            string name = TruncateText(
                employee.Name,
                NameColumnWidth
            );

            string position = TruncateText(
                employee.Position,
                PositionColumnWidth
            );

            Console.WriteLine(
                $"  │ {employee.Id,-3} │ {name,-15} │ {position,-23} │ {employee.Age,-4} │ Rp {employee.Salary,11:N0} │"
            );
        }

        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine(
            "  └─────┴─────────────────┴─────────────────────────┴──────┴────────────────┘"
        );

        Console.ResetColor();

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(
            $"  Total Employees: {employees.Count}"
        );
        Console.ResetColor();
    }

    // ========================================
    // DISPLAY SINGLE EMPLOYEE
    // ========================================

    private void DisplayEmployee(Employee employee)
    {
        WriteBoxTop();
        WriteBoxLine($"  ID       : {employee.Id}");
        WriteBoxLine($"  Name     : {employee.Name}");
        WriteBoxLine($"  Position : {employee.Position}");
        WriteBoxLine($"  Age      : {employee.Age}");
        WriteBoxLine($"  Salary   : Rp {employee.Salary:N0}");
        WriteBoxBottom();
    }

}