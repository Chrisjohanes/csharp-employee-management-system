namespace CSharpDotNetLearning.UI;

public static class InputHelper
{
    // ========================================
    // UI HELPERS
    // ========================================

    private static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"✗ {message}");
        Console.ResetColor();
    }


    // ========================================
    // READ REQUIRED STRING
    // ========================================

    public static string ReadRequiredString(string message)
    {
        while (true)
        {
            Console.Write(message);

            string input = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            ShowError("Input cannot be empty.");
        }
    }


    // ========================================
    // READ POSITIVE INTEGER
    // ========================================

    public static int ReadPositiveInt(string message)
    {
        while (true)
        {
            Console.Write(message);

            bool isValid = int.TryParse(
                Console.ReadLine(),
                out int value
            );

            if (isValid && value > 0)
            {
                return value;
            }

            ShowError(
                "Please enter a valid positive number."
            );
        }
    }


    // ========================================
    // READ AGE
    // ========================================

    public static int ReadAge()
    {
        while (true)
        {
            Console.Write(
                "  Enter employee age: "
            );

            bool isValid = int.TryParse(
                Console.ReadLine(),
                out int value
            );

            if (isValid && value >= 18 && value <= 65)
            {
                return value;
            }

            ShowError(
                "Age must be between 18 and 65."
            );
        }
    }


    // ========================================
    // READ POSITIVE DECIMAL
    // ========================================

    public static decimal ReadNonNegativeDecimal(string message)
    {
        while (true)
        {
            Console.Write(message);

            bool isValid = decimal.TryParse(
                Console.ReadLine(),
                out decimal value
            );

            if (isValid && value > 0)
            {
                return value;
            }

            ShowError(
                "Salary must be greater than 0."
            );
        }
    }


    // ========================================
    // READ EMPLOYEE ID
    // ========================================

    public static int ReadEmployeeId()
    {
        return ReadPositiveInt(
            "  Enter employee ID: "
        );
    }
}