// --------------------------------------------------------------------------------------------------
// HabitLogger.ConsoleApp.Program
// --------------------------------------------------------------------------------------------------
// Main insertion point of the application.
// --------------------------------------------------------------------------------------------------
using HabitLogger.ConsoleApp.Views;
using Microsoft.Extensions.Configuration;

namespace HabitLogger.ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        // Configure appsettings.
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        // Get the database connection string.
        string? databaseConnectionString = config.GetConnectionString("SqliteConnection");
        if (string.IsNullOrWhiteSpace(databaseConnectionString))
        {
            MessagePage.Show("Error", "Missing DatabaseConnectionString value in appsetttings.json");
            Environment.Exit(0);
        }

        // Create the required service.
        var habitLoggerService = new HabitLoggerService(databaseConnectionString!);

        // Get if seed data if required or not.
        bool generateSeedData = config.GetValue<bool>("Development:GenerateSeedData");
        if (generateSeedData)
        {
            Console.WriteLine("Generating seed data. Please wait...");
            habitLoggerService.SeedDatabase();
            Console.WriteLine("Seed data generated.");
        }

        // Show the main menu.
        var mainMenu = new MainMenuPage(habitLoggerService);
        mainMenu.Show();
    }
}
