// --------------------------------------------------------------------------------------------------
// HabitLogger.Data.Managers.SqliteDataManager.SeedData
// --------------------------------------------------------------------------------------------------
// Partial class for data manager methods specific to seeding the databe with mock data.
// --------------------------------------------------------------------------------------------------
using Microsoft.Data.Sqlite;

namespace HabitLogger.Data.Managers;

public partial class SqliteDataManager
{
    #region Constants
    
    private readonly string SeedTableHabitQuery =
        @"
        insert into habit(name, measure, is_active) values ('drinking water', 'glasses', true);
        insert into habit(name, measure, is_active) values ('morning coffee', 'cups', true);
        insert into habit(name, measure, is_active) values ('afternoon walk', 'kms', true);
        insert into habit(name, measure, is_active) values ('reading before bed', 'pages', true);
        insert into habit(name, measure, is_active) values ('meditation', 'minutes', false);
        insert into habit(name, measure, is_active) values ('yoga', 'minutes', false);
        insert into habit(name, measure, is_active) values ('listening to music', 'minutes', true);
        ";

    #endregion
    #region Methods: Public

    public void SeedDatabase()
    {
        // Only seed once / new database.
        if (GetHabits().Count == 0)
        {
            // Put all seed database methods here, in dependency order.
            SeedTableHabit();
            SeedTableHabitLog();
        }
    }

    #endregion
    #region Methods: Private

    private void SeedTableHabit()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = SeedTableHabitQuery;
        command.ExecuteNonQuery();
    }

    /// <summary>
    /// Inserts a hundred randomly generated habit log entries into the database.
    /// </summary>
    private void SeedTableHabitLog()
    {
        var habits = GetHabits();

        var generateCount = 100;

        for (int i = 0; i < generateCount; i++)
        {
            var habitId = habits[Random.Shared.Next(0, habits.Count)].Id;

            var date = DateTime.Now.AddDays(-Random.Shared.Next(0, 90)).Date;

            var quantity = Random.Shared.Next(1, 11);

            AddHabitLog(habitId, date, quantity);
        }
    }

    #endregion
}
