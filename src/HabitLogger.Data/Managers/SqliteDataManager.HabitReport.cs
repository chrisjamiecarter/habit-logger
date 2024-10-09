// --------------------------------------------------------------------------------------------------
// HabitLogger.Data.Managers.SqliteDataManager.HabitReport
// --------------------------------------------------------------------------------------------------
// Partial class for data manager methods specific to the HabitReport entity.
// --------------------------------------------------------------------------------------------------
using HabitLogger.Data.Entities;
using Microsoft.Data.Sqlite;

namespace HabitLogger.Data.Managers;

public partial class SqliteDataManager
{
    #region Constants

    internal static readonly string GetHabitReportQuery =
        @"
        SELECT
            *
        FROM
            vw_habit_report
        ;";

    #endregion
    #region Methods: Public - Read

    public IReadOnlyList<HabitReportEntity> GetHabitReport()
    {
        var output = new List<HabitReportEntity>();

        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = GetHabitReportQuery;

        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            output.Add(new HabitReportEntity(reader));
        }

        return output;
    }

    #endregion
}
