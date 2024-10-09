using HabitLogger.Data.Entities;

namespace HabitLogger.Models;

/// <summary>
/// Represents a HabitLogSumReport data transfer object (DTO), 
/// summarizing the total quantity of a Habit along with its name and measurement, 
/// initialized from an entity.
/// </summary>
public class HabitLogSumReport
{
    #region Constructors

    public HabitLogSumReport(HabitLogSumReportEntity entity)
    {
        Name = entity.Name;
        Measure = entity.Measure;
        Quantity = entity.Quantity;
    }

    #endregion
    #region Properties

    public string? Name { get; set; }

    public string? Measure { get; set; }

    public int Quantity { get; set; }

    #endregion
}
