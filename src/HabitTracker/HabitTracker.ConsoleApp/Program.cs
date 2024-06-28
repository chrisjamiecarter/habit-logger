// --------------------------------------------------------------------------------------------------
// HabitTracker.ConsoleApp.Program
// --------------------------------------------------------------------------------------------------
// TODO: Description.
// --------------------------------------------------------------------------------------------------

using System.Globalization;
using Microsoft.Data.Sqlite;

namespace HabitTracker.ConsoleApp;

internal class Program
{
    private static string connectionString = @"Data Source=habit-tracker.db;";

    private static void Main(string[] args)
    {

        using var connection = new SqliteConnection(connectionString);

        connection.Open();

        var command = connection.CreateCommand();
        
        command.CommandText = 
            @"CREATE TABLE IF NOT EXISTS drinking_water 
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Date TEXT,
                Quantity INTEGER
            );";
        
        command.ExecuteNonQuery();

        connection.Close();

        GetUserInput();
    }

    private static void GetUserInput()
    {
        bool closeApp = false;
        while (!closeApp)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Habit Tracker: Main Menu");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Select an option.");
            Console.WriteLine("");
            Console.WriteLine("0 - Close Application.");
            Console.WriteLine("1 - View All Records.");
            Console.WriteLine("2 - Insert Record.");
            Console.WriteLine("3 - Delete Record.");
            Console.WriteLine("4 - Update Record.");
            Console.WriteLine("");

            var userInput = Console.ReadLine();

            switch (userInput)
            {
                case "0":
                    Console.WriteLine();
                    Console.WriteLine("Goodbye!");
                    Console.WriteLine();
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    GetAllRecords();
                    break;
                case "2":
                    Insert();
                    break;
                case "3":
                    Delete();
                    break;
                case "4":
                    Update();
                    break;
                default:
                    Console.WriteLine("Invalid Input.");
                    break;
            }
        }
    }

    private static void GetAllRecords()
    {
        Console.Clear();
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText =
            $@"SELECT * FROM drinking_water;";

        List<DrinkingWater> drinkingWaterItems = [];

        SqliteDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                drinkingWaterItems.Add(
                    new DrinkingWater()
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", new CultureInfo("en-GB")),
                        Quantity = reader.GetInt32(2)
                    });
            }
        }
        else
        {
            Console.WriteLine("No rows found");
        }
        
        connection.Close();

        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("Habit Tracker: View All Records");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("");
        foreach(var item in drinkingWaterItems)
        {
            Console.WriteLine($"{item.Id} - {item.Date:dd-MMM-yyyy} - Quantity: {item.Quantity}");
        }
        Console.WriteLine("");
        
    }


    private static void Insert()
    {
        string date = GetDateInput();

        int quantity = GetNumberInput("Please insert number of glasses or other measure of your choice (No decimals allowed).");

        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = 
            $@"INSERT INTO drinking_water (date, quantity)
                VALUES ('{date}', {quantity});";
        command.ExecuteNonQuery();
        connection.Close();
    }

    private static void Delete()
    {
        GetAllRecords();

        var id = GetNumberInput("Please type the ID of the item you want to delete, ot type 0 to return to the main menu.");

        if (id == 0) GetUserInput();
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText =
            $@"DELETE FROM drinking_water
                WHERE Id={id};";
        int rowCount = command.ExecuteNonQuery();

        if (rowCount == 0)
        {
            Console.WriteLine($"Record with ID {id} doesn't exist.");
            connection.Close();
            Delete();

        }

        Console.WriteLine($"Record with ID {id} was deleted.");
        connection.Close();
    }

    private static void Update()
    {
        GetAllRecords();

        var id = GetNumberInput("Please type the ID of the item you want to update, or type 0 to return to the main menu.");
        

        if (id == 0) GetUserInput();
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var checkCommand = connection.CreateCommand();
        checkCommand.CommandText = $"SELECT EXISTS(SELECT 1 FROM drinking_water WHERE id = {id});";
        int checkQuery = Convert.ToInt32(checkCommand.ExecuteScalar());

        if (checkQuery == 0)
        {
            Console.WriteLine($"Record with ID {id} doesn't exist.");
            connection.Close();
            Update();
        }

        string date = GetDateInput();

        int quantity = GetNumberInput("Please insert number of glasses or other measure of your choice (No decimals allowed).");

        var command = connection.CreateCommand();
        command.CommandText =
            $@"UPDATE drinking_water
                SET Date = '{date}', Quantity = {quantity}
                WHERE Id={id};";
        command.ExecuteNonQuery();
        
        Console.WriteLine($"Record with ID {id} was updated.");
        connection.Close();
    }

    private static string GetDateInput()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Please insert the date: (Format: dd-mm-yy). Type 0 to return to main menu.");

        var dateInput = Console.ReadLine();

        if (dateInput == "0") GetUserInput();

        while (!DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-GB"), DateTimeStyles.None, out _))
        {
            Console.WriteLine("Invalid date. (Format: dd-mm-yy). Type 0 to return to main menu or try again:");
            if (dateInput == "0") GetUserInput();

            dateInput = Console.ReadLine();
        }

        return dateInput;
    }

    private static int GetNumberInput(string message)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(message);

        var numberInput = Console.ReadLine();

        if (numberInput == "0") GetUserInput();

        while (!int.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0)
        {
            Console.WriteLine("Invalid Number. Try again.");
            numberInput = Console.ReadLine();
        }

        int finalInput = Convert.ToInt32(numberInput);

        return finalInput;
    }
}

internal class DrinkingWater
{
    internal int Id { get; set; }

    internal DateTime Date { get; set; }

    internal int Quantity { get; set; }
}