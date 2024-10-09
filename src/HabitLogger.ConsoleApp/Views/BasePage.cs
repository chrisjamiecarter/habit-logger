using System.Text;
using HabitLogger.Constants;

namespace HabitLogger.ConsoleApp.Views;

/// <summary>
/// Provides a base class for console application pages, facilitating header and footer display.
/// </summary>
internal abstract class BasePage
{
    #region Methods: Protected

    protected static void WriteFooter()
    {
        Console.Write($"{Environment.NewLine}Press any key to continue...");
    }

    protected static void WriteHeader(string title)
    {
        Console.Clear();
        Console.Write(GetHeaderText(title));
    }

    #endregion
    #region Methods: Private

    private static string GetHeaderText(string pageTitle)
    {
        var builder = new StringBuilder();
        builder.AppendLine("----------------------------------------");
        builder.AppendLine($"{Application.Title}: {pageTitle}");
        builder.AppendLine("----------------------------------------");
        builder.AppendLine();
        return builder.ToString();
    }

    #endregion
}
