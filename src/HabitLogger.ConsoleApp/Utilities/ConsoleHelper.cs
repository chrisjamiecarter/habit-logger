using System.Globalization;

namespace HabitLogger.ConsoleApp.Utilities;

/// <summary>
/// Provides utility methods for console input validation and retrieval, such as getting characters, integers, dates, and strings.
/// </summary>
internal static class ConsoleHelper
{
    #region Methods: Internal

    /// <summary>
    /// Gets a valid character from the user.
    /// Cannot be a whitespace character.
    /// </summary>
    /// <param name="message">The message to display to the user in the console.</param>
    /// <returns>The character that the user input via the console.</returns>
    internal static char GetChar(string message)
    {
        string? input = "";
        char output;

        Console.WriteLine(message);
        input = Console.ReadLine();

        // Validation: Input must be something and an only one character.
        while (string.IsNullOrWhiteSpace(input) || input.Length != 1)
        {
            Console.WriteLine($"Invalid input. {message}");
            input = Console.ReadLine();
        }

        // Input has been validated..
        output = input.First();

        return output;
    }

    /// <summary>
    /// Gets a valid integer from the user.
    /// Cannot be null.
    /// </summary>
    /// <param name="message">The message to display to the user in the console.</param>
    /// <returns>The int that the user input via the console.</returns>
    internal static int GetInt(string message)
    {
        string? input = "";
        int output;

        Console.WriteLine(message);
        input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out _))
        {
            Console.WriteLine($"Invalid input. {message}");
            input = Console.ReadLine();
        }

        output = int.Parse(input);

        return output;
    }

    /// <summary>
    /// Gets a valid integer from the user.
    /// Cannot be null and must not be less than the minimum value.
    /// </summary>
    /// <param name="message">The message to display to the user in the console.</param>
    /// <param name="min">The minimum allowed value, inclusive.</param>
    /// <returns>The int that the user input via the console.</returns>
    internal static int GetInt(string message, int min)
    {
        string? input = "";
        int output;

        Console.WriteLine(message);
        input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out _) || int.Parse(input) < min)
        {
            Console.WriteLine($"Invalid input. {message}");
            input = Console.ReadLine();
        }

        output = int.Parse(input);

        return output;
    }

    /// <summary>
    /// Gets a valid integer from the user.
    /// Cannot be null and must not be less than the minimum value or greater than the maximum value.
    /// </summary>
    /// <param name="message">The message to display to the user in the console.</param>
    /// <param name="min">The minimum allowed value, inclusive.</param>
    /// <param name="max">The maximum allowed value, inclusive.</param>
    /// <returns>The int that the user input via the console.</returns>
    internal static int GetInt(string message, int min, int max)
    {
        string? input = "";
        int output;

        Console.WriteLine(message);
        input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out _) || int.Parse(input) < min || int.Parse(input) > max)
        {
            Console.WriteLine($"Invalid input. {message}");
            input = Console.ReadLine();
        }

        output = int.Parse(input);

        return output;
    }

    /// <summary>
    /// Gets either a valid DateTime from the user, or null.
    /// Cannot be whitespace and must be parse exactly against the format.
    /// Or if 0 is entered, will return a null datetime.
    /// </summary>
    /// <param name="message">The message to display to the user in the console.</param>
    /// <param name="format">The exact format to parse the input datetime.</param>
    /// <returns>The datetime that the user input via the console.</returns>
    internal static DateTime? GetDate(string message, string format)
    {
        string? input = "";
        DateTime? output = null;

        Console.WriteLine(message);
        input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input) && input == "0")
        {
            return output;
        }

        while (string.IsNullOrWhiteSpace(input) || !DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            if (!string.IsNullOrWhiteSpace(input) && input == "0")
            {
                return output;
            }

            Console.WriteLine($"Invalid input. {message}");
            input = Console.ReadLine();
        }

        output = DateTime.ParseExact(input, format, CultureInfo.InvariantCulture);
        return output;
    }

    /// <summary>
    /// Gets either a valid DateTime from the user, or null.
    /// Cannot be whitespace, must be parse exactly against the format and must be after the afterDateTime param.
    /// Or if 0 is entered, will return a null datetime.
    /// </summary>
    /// <param name="message">The message to display to the user in the console.</param>
    /// <param name="format">The exact format to parse the input datetime.</param>
    /// <param name="afterDateTime">The datetime that the input must be greater than.</param>
    /// <returns>The datetime that the user input via the console.</returns>
    internal static DateTime? GetDateAfter(string message, string format, DateTime afterDateTime)
    {
        string? input = "";
        DateTime? output = null;

        Console.WriteLine(message);
        input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input) && input == "0")
        {
            return output;
        }

        while (string.IsNullOrWhiteSpace(input) || !DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _) || !(DateTime.ParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture) > afterDateTime))
        {
            if (!string.IsNullOrWhiteSpace(input) && input == "0")
            {
                return output;
            }

            Console.WriteLine($"Invalid input. {message}");
            input = Console.ReadLine();
        }

        output = DateTime.ParseExact(input, format, CultureInfo.InvariantCulture);
        return output;
    }

    /// <summary>
    /// Prompts the user for input and ensures the input is not empty or whitespace.
    /// </summary>
    /// <param name="message">The message to display prompting for input.</param>
    /// <returns>The valid string input provided by the user.</returns>
    internal static string GetString(string message)
    {
        string? input = "";
        string output = "";

        Console.WriteLine(message);
        input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine($"Invalid input. {message}");
            input = Console.ReadLine();
        }

        output = input;

        return output;
    }

    #endregion
}
