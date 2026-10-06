using CSharpDotNetLearning.Models;
using CSharpDotNetLearning.Services;

namespace CSharpDotNetLearning.UI;

public static class EmployeeDashboard
{
    public static void Show(
        EmployeeService employeeService)
    {
        ConsoleHelper.ShowHeader(
            "EMPLOYEE DASHBOARD"
        );

        List<Employee> employees =
            employeeService.Employees;

        if (employees.Count == 0)
        {
            ConsoleHelper.ShowWarning(
                "No employee data available."
            );

            return;
        }

        int totalEmployees = employees.Count;

        decimal averageAge =
            employees.Average(
                employee => (decimal)employee.Age
            );

        decimal averageSalary =
            employees.Average(
                employee => employee.Salary
            );

        decimal highestSalary =
            employees.Max(
                employee => employee.Salary
            );

        decimal lowestSalary =
            employees.Min(
                employee => employee.Salary
            );

        Employee highestPaidEmployee =
            employees.MaxBy(
                employee => employee.Salary
            )!;

        Employee youngestEmployee =
            employees.MinBy(
                employee => employee.Age
            )!;

        Employee oldestEmployee =
            employees.MaxBy(
                employee => employee.Age
            )!;

        ShowSummary(
            totalEmployees,
            averageAge,
            averageSalary,
            highestSalary,
            lowestSalary
        );

        ShowHighlights(
            highestPaidEmployee,
            youngestEmployee,
            oldestEmployee
        );

        ShowEmployeesByPosition(
            employeeService
        );
    }

    private static void ShowSummary(
        int totalEmployees,
        decimal averageAge,
        decimal averageSalary,
        decimal highestSalary,
        decimal lowestSalary)
    {
        ConsoleHelper.WriteBoxTop();
        ConsoleHelper.WriteBoxTitle("SUMMARY");

        ConsoleHelper.WriteBoxLine(
            $"  Total Employees     : {totalEmployees}"
        );

        ConsoleHelper.WriteBoxLine(
            $"  Average Age         : {averageAge:N2} years"
        );

        ConsoleHelper.WriteBoxLine(
            $"  Average Salary      : Rp {averageSalary:N0}"
        );

        ConsoleHelper.WriteBoxLine(
            $"  Highest Salary      : Rp {highestSalary:N0}"
        );

        ConsoleHelper.WriteBoxLine(
            $"  Lowest Salary       : Rp {lowestSalary:N0}"
        );

        ConsoleHelper.WriteBoxBottom();

        Console.WriteLine();
    }

    private static void ShowHighlights(
        Employee highestPaidEmployee,
        Employee youngestEmployee,
        Employee oldestEmployee)
    {
        ConsoleHelper.WriteBoxTop();
        ConsoleHelper.WriteBoxTitle(
            "TOP EMPLOYEES"
        );

        ConsoleHelper.WriteBoxLine(
            $"  Highest Paid        : {highestPaidEmployee.Name,-21}"
        );

        ConsoleHelper.WriteBoxLine(
            $"  Youngest Employee   : {youngestEmployee.Name,-21}"
        );

        ConsoleHelper.WriteBoxLine(
            $"  Oldest Employee     : {oldestEmployee.Name,-21}"
        );

        ConsoleHelper.WriteBoxBottom();

        Console.WriteLine();
    }

    private static void ShowEmployeesByPosition(
        EmployeeService employeeService)
    {
        const int positionWidth = 25;

        Dictionary<string, int> employeesByPosition =
            employeeService.GroupByPosition();

        ConsoleHelper.WriteBoxTop();

        ConsoleHelper.WriteBoxTitle(
            "EMPLOYEE BY POSITION"
        );

        foreach (
            KeyValuePair<string, int> item
            in employeesByPosition)
        {
            string position =
                ConsoleHelper.TruncateText(
                    item.Key,
                    positionWidth
                );

            ConsoleHelper.WriteBoxLine(
                $"  {position,-25} : {item.Value}"
            );
        }

        ConsoleHelper.WriteBoxBottom();
    }
}