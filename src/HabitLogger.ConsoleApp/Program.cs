using HabitLogger.ConsoleApp.Views;
using Microsoft.Extensions.Configuration;

namespace HabitLogger.ConsoleApp;

/// <summary>
/// Main insertion point of the application.
/// Sets up the database, seeds any data and then displays the main menu.
/// </summary>
internal class Program
{
    private static void Main(string[] args)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        string? databaseConnectionString = config.GetConnectionString("SqliteConnection");
        if (string.IsNullOrWhiteSpace(databaseConnectionString))
        {
            MessagePage.Show("Error", "Missing DatabaseConnectionString value in appsetttings.json");
            Environment.Exit(-1);
        }

        var habitLoggerService = new HabitLoggerService(databaseConnectionString!);

        bool generateSeedData = config.GetValue<bool>("Development:GenerateSeedData");
        if (generateSeedData)
        {
            Console.WriteLine("Generating seed data. Please wait...");
            habitLoggerService.SeedDatabase();
            Console.WriteLine("Seed data generated.");
        }

        var mainMenu = new MainMenuPage(habitLoggerService);
        mainMenu.Show();
        Environment.Exit(0);
    }
}
