namespace CSharpDotNetLearning.UI;

public static class ConsoleHelper
{
    public const int BoxWidth = 50;

    public static void ShowHeader(string title)
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║              EMPLOYEE MANAGEMENT SYSTEM              ║");
        Console.WriteLine("║                    C# / .NET                         ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        Console.ResetColor();

        Console.WriteLine();

        Console.WriteLine($"  {title}");

        Console.WriteLine();
    }

    public static void ShowSuccess(string message)
    {
        WriteColoredMessage("✓", message, ConsoleColor.Green);
    }

    public static void ShowError(string message)
    {
        WriteColoredMessage("✗", message, ConsoleColor.Red);
    }

    public static void ShowWarning(string message)
    {
        WriteColoredMessage("⚠", message, ConsoleColor.Yellow);
    }

    public static void Pause()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("  Press ENTER to continue...");
        Console.ResetColor();

        Console.ReadLine();
    }

    public static void WriteBoxTop()
    {
        WriteColoredLine($"  ┌{new string('─', BoxWidth)}┐");
    }

    public static void WriteBoxTitle(string title)
    {
        string content = CenterText(title, BoxWidth);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"  │{content}│");
        Console.WriteLine($"  ├{new string('─', BoxWidth)}┤");
        Console.ResetColor();
    }

    public static void WriteBoxLine(string content)
    {
        content = TruncateText(content, BoxWidth);

        Console.WriteLine(
            $"  │{content.PadRight(BoxWidth)}│"
        );
    }

    public static void WriteBoxBottom()
    {
        WriteColoredLine(
            $"  └{new string('─', BoxWidth)}┘"
        );
    }

    public static void WriteMenuItem(
        int number,
        string label)
    {
        WriteBoxLine($"  [{number}]  {label}");
    }

    public static string TruncateText(
        string value,
        int maxLength)
    {
        return value.Length > maxLength
            ? value[..maxLength]
            : value;
    }

    private static string CenterText(
        string text,
        int width)
    {
        if (text.Length >= width)
        {
            return text[..width];
        }

        int leftPadding =
            (width - text.Length) / 2;

        int rightPadding =
            width - text.Length - leftPadding;

        return new string(' ', leftPadding)
            + text
            + new string(' ', rightPadding);
    }

    private static void WriteColoredLine(
        string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private static void WriteColoredMessage(
        string symbol,
        string message,
        ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"  {symbol} {message}");
        Console.ResetColor();
    }
}