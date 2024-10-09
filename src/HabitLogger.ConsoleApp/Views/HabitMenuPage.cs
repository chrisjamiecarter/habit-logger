// --------------------------------------------------------------------------------------------------
// HabitLogger.ConsoleApp.Views.HabitMenuPage
// --------------------------------------------------------------------------------------------------
// Displays a list of Habits to a user and returns the users selection.
// --------------------------------------------------------------------------------------------------
using System.Text;
using HabitLogger.ConsoleApp.Enums;
using HabitLogger.ConsoleApp.Utilities;
using HabitLogger.Models;

namespace HabitLogger.ConsoleApp.Views;

/// <summary>
/// Represents the page for displaying a list of <see cref="Habit"/> for user selection.
/// </summary>
internal class HabitMenuPage : BasePage
{
    #region Constants

    private const string PageTitle = "Habit Menu";

    #endregion
    #region Methods: Internal

    internal static Habit? Show(string action, List<Habit> habits)
    {
        var status = PageStatus.Opened;

        Habit? output = null;

        while (status != PageStatus.Closed)
        {
            Console.Clear();

            WriteHeader($"{PageTitle} ({action})");

            Console.Write(MenuText(habits));

            var option = ConsoleHelper.GetInt("Enter your selection: ");

            switch (option)
            {
                case 0:

                    // Go back to main menu.
                    status = PageStatus.Closed;
                    break;

                default:

                    if (option < 1 || option > habits.Count)
                    {
                        MessagePage.Show("Error", "Invalid option selected.");
                    }
                    else
                    {
                        // NOTE: option is 1-based (list is 0-based)
                        output = habits[option - 1];
                        status = PageStatus.Closed;
                    }
                    break;
            }
        }

        return output;
    }

    #endregion
    #region Methods: Private

    private static string MenuText(List<Habit> habits)
    {
        var builder = new StringBuilder();
        builder.AppendLine("Select an option...");
        builder.AppendLine();
        builder.AppendLine("0 - Back to main menu");

        for (int i = 0; i < habits.Count; i++)
        {
            builder.AppendLine($"{i + 1} - {habits[i].Name}");
        }

        builder.AppendLine();

        return builder.ToString();
    }

    #endregion
}
