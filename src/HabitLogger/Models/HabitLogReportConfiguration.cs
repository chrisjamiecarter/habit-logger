namespace HabitLogger.Models;

/// <summary>
/// Represents the configuration parameters for generating a HabitLogReport, 
/// allowing optional filtering by ID, start date, and end date.
/// </summary>
public class HabitLogReportConfiguration
{
    #region Properties

    public int? HabitId { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    #endregion
}
