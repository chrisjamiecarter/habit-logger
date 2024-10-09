using System.Data;
using HabitLogger.Data.Extensions;

namespace HabitLogger.Data.Entities;

/// <summary>
/// Represents a database entity in the vw_habit_report view.
/// </summary>
public class HabitReportEntity
{
    #region Constructors

    public HabitReportEntity(IDataReader reader)
    {
        Id = reader.GetInt32("habit_id");
        Name = reader.GetString("name");
        Measure = reader.GetString("measure");
        IsActive = reader.GetBoolean("is_active");
    }

    #endregion
    #region Properties

    public int Id { get; }

    public string? Name { get; }

    public string? Measure { get; }

    public bool IsActive { get; }

    #endregion
}
