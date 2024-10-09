using HabitLogger.Data.Entities;

namespace HabitLogger.Models;

/// <summary>
/// Represents a HabitLog data transfer object (DTO), capturing information about a logged Habit, 
/// such as the ID, date, and quantity, and providing constructors for initialization 
/// from both entities and reports.
/// </summary>
public class HabitLog
{
    #region Constructors

    public HabitLog(HabitLogEntity entity)
    {
        Id = entity.Id;
        HabitId = entity.HabitId;
        Date = entity.Date;
        Quantity = entity.Quantity;
    }

    public HabitLog(int habitId, DateTime date, int quantity)
    {
        HabitId = habitId;
        Date = date;
        Quantity = quantity;
    }

    public HabitLog(HabitLogReport report)
    {
        Id = report.Id;
        HabitId = report.HabitId;
        Date = report.Date;
        Quantity = report.Quantity;
    }

    #endregion
    #region Properties

    public int Id { get; set; }

    public int HabitId { get; set; }

    public DateTime Date { get; set; }

    public int Quantity { get; set; }

    #endregion
}
