using HabitLogger.Data.Entities;

namespace HabitLogger.Models;

/// <summary>
/// Represents a HabitLogReport data transfer object (DTO), 
/// providing detailed information about a logged Habit, including the name, 
/// measurement, date, and quantity, with initialization from an entity.
/// </summary>
public class HabitLogReport
{
    #region Constructors

    public HabitLogReport(HabitLogReportEntity entity)
    {
        Id = entity.Id;
        HabitId = entity.HabitId;
        Name = entity.Name;
        Measure = entity.Measure;
        Date = entity.Date;
        Quantity = entity.Quantity;
    }

    #endregion
    #region Properties

    public int Id { get; set; }

    public int HabitId { get; set; }

    public DateTime Date { get; set; }

    public string? Name { get; set; }

    public string? Measure { get; set; }

    public int Quantity { get; set; }

    #endregion
}
