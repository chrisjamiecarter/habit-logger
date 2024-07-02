// --------------------------------------------------------------------------------------------------
// HabitTracker.ConsoleApp.Program
// --------------------------------------------------------------------------------------------------
// Main insertion point of the application.
// --------------------------------------------------------------------------------------------------
using HabitTracker.ConsoleApp.Views;
using Microsoft.Extensions.Configuration;

namespace HabitTracker.ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        // Configure appsettings. Required for database connection string.
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        // Get the database connection string.
        string? databaseConnectionString = config.GetConnectionString("SqliteConnection");
        if(string.IsNullOrWhiteSpace(databaseConnectionString))
        {
            MessagePage.Show("Error", "Missing DatabaseConnectionString value in appsetttings.json");
            Environment.Exit(0);
        }

        // Create the required service.
        var habitTrackerService = new HabitTrackerService(databaseConnectionString!);

        // Show the main menu.
        var mainMenu = new MainMenuPage(habitTrackerService);
        mainMenu.Show();
    }
}
