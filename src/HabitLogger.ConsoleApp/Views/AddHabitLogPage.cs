using HabitLogger.ConsoleApp.Utilities;
using HabitLogger.Models;

namespace HabitLogger.ConsoleApp.Views;

/// <summary>
/// Represents the page for recording a new <see cref="HabitLog"/> entry.
/// </summary>
internal class AddHabitLogPage : BasePage
{
    #region Constants

    private const string PageTitle = "Record Habit";

    #endregion
    #region Methods: Internal

    internal static HabitLog? Show(Habit habit)
    {
        HabitLog? nullHabitLog = null;

        Console.Clear();

        WriteHeader($"{PageTitle} ({habit.Name})");

        DateTime? date = ConsoleHelper.GetDate("Enter the date (format yyyy-MM-dd) or 0 to return to main menu: ", "yyyy-MM-dd");
        if (!date.HasValue)
        {
            return nullHabitLog;
        }

        int quantity = ConsoleHelper.GetInt("Enter the quantity (format integer > 0) or 0 to return to main menu: ", 0);
        if (quantity == 0)
        {
            return nullHabitLog;
        }

        return new HabitLog(habit.Id, date.Value, quantity);
    }

    #endregion
}
