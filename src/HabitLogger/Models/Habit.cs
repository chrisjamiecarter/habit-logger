using HabitLogger.Data.Entities;

namespace HabitLogger.Models;

/// <summary>
/// Represents a Habit data transfer object (DTO), encapsulating the Habit's 
/// ID, name, measurement, and active status, and providing constructors 
/// for initialization from both entities and raw data.
/// </summary>
public class Habit
{
    #region Constructors

    public Habit(HabitEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Measure = entity.Measure;
        IsActive = entity.IsActive;
    }

    public Habit(string name, string measure)
    {
        Name = name;
        Measure = measure;
        IsActive = true;
    }

    #endregion
    #region Properties

    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Measure { get; set; }

    public bool IsActive { get; set; }

    #endregion
}
