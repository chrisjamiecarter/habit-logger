// --------------------------------------------------------------------------------------------------
// HabitTracker.Data.Managers.SqliteDataManager.HabitLogReport
// --------------------------------------------------------------------------------------------------
// Partial class for data manager methods specific to the HabitLogReport entity.
// --------------------------------------------------------------------------------------------------
using HabitTracker.Data.Entities;
using Microsoft.Data.Sqlite;

namespace HabitTracker.Data.Managers;

public partial class SqliteDataManager
{
    #region Constants

    internal static readonly string GetHabitLogReportQuery =
        @"
        SELECT
            *
        FROM
            vw_habit_log_report
        ;";

    #endregion
    #region Methods: Public - Read

    public IReadOnlyList<HabitLogReportEntity> GetHabitLogReport()
    {
        var output = new List<HabitLogReportEntity>();

        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = GetHabitLogReportQuery;

        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            output.Add(new HabitLogReportEntity(reader));
        }

        connection.Close();
        return output;
    }

    #endregion
}
