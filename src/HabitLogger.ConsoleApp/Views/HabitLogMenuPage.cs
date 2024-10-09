using System.Text;
using ConsoleTableExt;
using HabitLogger.ConsoleApp.Enums;
using HabitLogger.ConsoleApp.Utilities;
using HabitLogger.Models;

namespace HabitLogger.ConsoleApp.Views;

/// <summary>
/// Represents the page for displaying a list of <see cref="HabitLog"/> for user selection.
/// </summary>
internal class HabitLogMenuPage : BasePage
{
    #region Constants

    private const string PageTitle = "Habit Log Menu";

    #endregion
    #region Properties

    private static string MenuText
    {
        get
        {
            var builder = new StringBuilder();
            builder.AppendLine("Select an option...");
            builder.AppendLine();
            builder.AppendLine("0 - Back to main menu");
            return builder.ToString();
        }
    }

    #endregion
    #region Methods: Internal

    internal static HabitLog? Show(string action, List<HabitLogReport> habitLogs)
    {
        var status = PageStatus.Opened;

        HabitLog? output = null;

        while (status != PageStatus.Closed)
        {
            Console.Clear();

            WriteHeader($"{PageTitle} ({action})");

            WriteMenuText(habitLogs);

            var option = ConsoleHelper.GetInt("Enter your selection: ");

            switch (option)
            {
                case 0:

                    // Go back to main menu.
                    status = PageStatus.Closed;
                    break;

                default:

                    if (option < 1 || option > habitLogs.Count)
                    {
                        MessagePage.Show("Error", "Invalid option selected.");
                    }
                    else
                    {
                        // NOTE: option is 1-based (list is 0-based)
                        output = new HabitLog(habitLogs[option - 1]);
                        status = PageStatus.Closed;
                    }
                    break;
            }
        }

        return output;
    }

    #endregion
    #region Methods: Private

    private static void WriteMenuText(List<HabitLogReport> habitLogs)
    {
        Console.Write(MenuText);

        WriteHabitLogSelections(habitLogs);

        Console.WriteLine();
    }

    private static void WriteHabitLogSelections(List<HabitLogReport> habitLogs)
    {
        // Configure table data.
        // NOTE: list<object> will create a headerless table which will append to the menu.
        var data = new List<List<object>>();
        for (int i = 0; i < habitLogs.Count; i++)
        {
            data.Add([$"{i + 1} -", habitLogs[i].Date.ToShortDateString(), habitLogs[i].Name, habitLogs[i].Quantity, habitLogs[i].Measure]);
        }

        // Configure & write console table.
        ConsoleTableBuilder.
            From(data).
            WithFormat(ConsoleTableBuilderFormat.Minimal).
            ExportAndWriteLine();
    }

    #endregion
}
