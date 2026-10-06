using CSharpDotNetLearning.Models;

namespace CSharpDotNetLearning.UI;

public static class EmployeeDisplay
{
    private const int NameColumnWidth = 15;
    private const int PositionColumnWidth = 23;

    public static void ShowList(
        List<Employee> employees)
    {
        if (employees.Count == 0)
        {
            ConsoleHelper.ShowWarning(
                "No employees found."
            );

            return;
        }

        WriteTableHeader();

        foreach (Employee employee in employees)
        {
            string name =
                ConsoleHelper.TruncateText(
                    employee.Name,
                    NameColumnWidth
                );

            string position =
                ConsoleHelper.TruncateText(
                    employee.Position,
                    PositionColumnWidth
                );

            Console.WriteLine(
                $"  │ {employee.Id,-3} │ {name,-15} │ {position,-23} │ {employee.Age,-4} │ Rp {employee.Salary,11:N0} │"
            );
        }

        WriteTableFooter();

        Console.WriteLine();

        Console.ForegroundColor =
            ConsoleColor.DarkGray;

        Console.WriteLine(
            $"  Total Employees: {employees.Count}"
        );

        Console.ResetColor();
    }

    public static void ShowSingle(
        Employee employee)
    {
        ConsoleHelper.WriteBoxTop();

        ConsoleHelper.WriteBoxLine(
            $"  ID       : {employee.Id}"
        );

        ConsoleHelper.WriteBoxLine(
            $"  Name     : {employee.Name}"
        );

        ConsoleHelper.WriteBoxLine(
            $"  Position : {employee.Position}"
        );

        ConsoleHelper.WriteBoxLine(
            $"  Age      : {employee.Age}"
        );

        ConsoleHelper.WriteBoxLine(
            $"  Salary   : Rp {employee.Salary:N0}"
        );

        ConsoleHelper.WriteBoxBottom();
    }

    private static void WriteTableHeader()
    {
        Console.ForegroundColor =
            ConsoleColor.Cyan;

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
    }

    private static void WriteTableFooter()
    {
        Console.ForegroundColor =
            ConsoleColor.Cyan;

        Console.WriteLine(
            "  └─────┴─────────────────┴─────────────────────────┴──────┴────────────────┘"
        );

        Console.ResetColor();
    }
}