// --------------------------------------------------------------------------------------------------
// HabitLogger.ConsoleApp.Views.NewHabitPage
// --------------------------------------------------------------------------------------------------
// Gets the required input from a user to add a new habit.
// --------------------------------------------------------------------------------------------------
using HabitLogger.ConsoleApp.Utilities;
using HabitLogger.Models;

namespace HabitLogger.ConsoleApp.Views;

internal class AddHabitPage : BasePage
{
    #region Constants

    private const string PageTitle = "New Habit";

    #endregion
    #region Methods: Internal

    internal static Habit? Show()
    {
        Habit? nullHabit = null;

        Console.Clear();

        WriteHeader(PageTitle);

        string name = ConsoleHelper.GetString("Enter the name or 0 to return to main menu: ");
        if (name == "0")
        {
            return nullHabit;
        }

        string measure = ConsoleHelper.GetString("Enter the measure or 0 to return to main menu: ");
        if (measure == "0")
        {
            return nullHabit;
        }

        return new Habit(name, measure);
    }

    #endregion
}
