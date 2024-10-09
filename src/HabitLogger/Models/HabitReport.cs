using HabitLogger.Data.Entities;

namespace HabitLogger.Models;

/// <summary>
/// Represents a HabitReport data transfer object (DTO), providing details about a Habit, 
/// including its ID, name, measurement, and active status, initialized from an entity.
/// </summary>
public class HabitReport
{
    #region Constructors

    public HabitReport(HabitReportEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Measure = entity.Measure;
        IsActive = entity.IsActive;
    }

    #endregion
    #region Properties

    public int Id { get; }

    public string? Name { get; }

    public string? Measure { get; }

    public bool IsActive { get; }

    #endregion
}
