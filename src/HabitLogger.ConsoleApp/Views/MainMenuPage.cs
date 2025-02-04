﻿using System.Data;
using System.Text;
using ConsoleTableExt;
using HabitLogger.ConsoleApp.Enums;
using HabitLogger.ConsoleApp.Utilities;
using HabitLogger.Models;

namespace HabitLogger.ConsoleApp.Views;

/// <summary>
/// Represents the main menu page for the application. It displays a list of options for user selection.
/// </summary>
internal class MainMenuPage : BasePage
{
    #region Constants

    private const string PageTitle = "Main Menu";

    #endregion
    #region Fields

    private readonly HabitLoggerService _habitLoggerService;

    #endregion
    #region Constructors

    public MainMenuPage(HabitLoggerService habitLoggerService)
    {
        _habitLoggerService = habitLoggerService;
    }

    #endregion
    #region Properties

    internal static string MenuText
    {
        get
        {
            var builder = new StringBuilder();
            builder.AppendLine("Select an option...");
            builder.AppendLine();
            builder.AppendLine("0 - Close application");
            builder.AppendLine();
            builder.AppendLine("- Recording -");
            builder.AppendLine("1 - Record habit");
            builder.AppendLine();
            builder.AppendLine("- Reporting -");
            builder.AppendLine("2 - View habit report");
            builder.AppendLine("3 - View habit log report");
            builder.AppendLine();
            builder.AppendLine("- Management -");
            builder.AppendLine("4 - Add new habit");
            builder.AppendLine("5 - Activate habit");
            builder.AppendLine("6 - Deactivate habit");
            builder.AppendLine("7 - Update habit log entry");
            builder.AppendLine("8 - Delete habit log entry");
            builder.AppendLine();

            return builder.ToString();
        }
    }

    #endregion
    #region Methods: Internal

    internal void Show()
    {
        var status = PageStatus.Opened;

        while (status != PageStatus.Closed)
        {
            Console.Clear();

            WriteHeader(PageTitle);

            Console.Write(MenuText);

            var option = ConsoleHelper.GetInt("Enter your selection: ");
            status = PerformOption(option);
        }
    }

    #endregion
    #region Methods: Private

    private PageStatus PerformOption(int option)
    {
        var output = PageStatus.Opened;

        switch (option)
        {
            case 0:

                // Close application.
                output = PageStatus.Closed;
                break;

            case 1:

                // Record habit.
                RecordHabit();
                break;

            case 2:

                // View Habit report.
                ViewHabitReportPage();
                break;

            case 3:

                // View Habit Log report.
                ViewHabitLogReportPage();
                break;

            case 4:

                // Add new habit.
                NewHabit();
                break;

            case 5:

                // Activate habit.
                SetHabitIsActive(true);
                break;

            case 6:

                // Deactivate habit.
                SetHabitIsActive(false);
                break;

            case 7:

                // Update habit log entry.
                UpdateHabitLog();
                break;

            case 8:

                // Delete habit log entry.
                DeleteHabitLog();
                break;

            default:

                MessagePage.Show("Error", "Invalid option selected.");
                break;
        }

        return output;
    }

    private void NewHabit()
    {
        var habit = AddHabitPage.Show();
        if (habit == null)
        {
            return;
        }

        _habitLoggerService.AddHabit(habit.Name!, habit.Measure!);

        MessagePage.Show("New Habit", $"Habit added successfully.");
    }

    private void RecordHabit()
    {
        var habits = _habitLoggerService.GetHabitsByIsActive(true);
        if (habits.Count < 1)
        {
            MessagePage.Show("Error", "No active habits.");
            return;
        }

        var habit = HabitMenuPage.Show("Record", habits);
        if (habit == null)
        {
            return;
        }

        var habitLog = AddHabitLogPage.Show(habit);
        if (habitLog == null)
        {
            return;
        }

        _habitLoggerService.AddHabitLog(habit.Id, habitLog.Date, habitLog.Quantity);

        MessagePage.Show("Record Habit", $"Habit recorded successfully.");
    }

    private void SetHabitIsActive(bool setToStatus)
    {
        var habits = _habitLoggerService.GetHabitsByIsActive(!setToStatus);
        if (habits.Count < 1)
        {
            MessagePage.Show("Error", $"No {(setToStatus ? "inactive" : "active")} habits.");
            return;
        }

        var action = setToStatus ? "Activate" : "Deactivate";

        var habit = HabitMenuPage.Show(action, habits);
        if (habit == null)
        {
            return;
        }

        _habitLoggerService.SetHabitIsActive(habit.Id, setToStatus);

        MessagePage.Show($"{action} Habit", $"Habit {(setToStatus ? "activated" : "deactivated")} successfully.");
    }

    private void UpdateHabitLog()
    {
        var habitLogs = _habitLoggerService.GetHabitLogReport();
        if (habitLogs.Count < 1)
        {
            MessagePage.Show("Error", $"No habit logs found.");
            return;
        }

        HabitLog? oldHabitLog = HabitLogMenuPage.Show("Update", habitLogs);
        if (oldHabitLog == null)
        {
            return;
        }

        Habit? habit = _habitLoggerService.GetHabit(oldHabitLog.HabitId);
        if (habit == null)
        {
            return;
        }

        HabitLog? newHabitLog = SetHabitLogPage.Show(habit, oldHabitLog);
        if (newHabitLog == null)
        {
            return;
        }

        _habitLoggerService.SetHabitLog(oldHabitLog.Id, habit.Id, newHabitLog.Date, newHabitLog.Quantity);

        MessagePage.Show("Update Habit Log Entry", $"Habit log entry updated successfully.");
    }

    private void DeleteHabitLog()
    {
        var habitLogs = _habitLoggerService.GetHabitLogReport();
        if (habitLogs.Count < 1)
        {
            MessagePage.Show("Error", $"No habit logs found.");
            return;
        }

        HabitLog? habitLog = HabitLogMenuPage.Show("Delete", habitLogs);
        if (habitLog == null)
        {
            return;
        }

        _habitLoggerService.DeleteHabitLog(habitLog.Id);

        MessagePage.Show("Delete Habit Log Entry", $"Habit log entry deleted successfully.");
    }

    private void ViewHabitLogReportPage()
    {
        // Additional specific report support:
        // Allow user to choose between a report for all habits or a specific one.

        var habits = _habitLoggerService.GetHabits();

        var reportConfig = ConfigureHabitLogReportPage.Show(habits);
        if (reportConfig == null)
        {
            // Go back to main menu.
            return;
        }

        var dataTable = new DataTable();
        if (reportConfig.DateFrom.HasValue && reportConfig.DateTo.HasValue)
        {
            // View quantity within date range.
            dataTable.Columns.Add("Habit");
            dataTable.Columns.Add("Quantity");
            dataTable.Columns.Add("Measure");

            if (reportConfig.HabitId.HasValue)
            {
                // Specific habit.
                var report = _habitLoggerService.GetHabitLogSumReportByHabitId(reportConfig.DateFrom.Value, reportConfig.DateTo.Value, reportConfig.HabitId.Value);
                foreach (var x in report)
                {
                    dataTable.Rows.Add([x.Name, x.Quantity, x.Measure]);
                }
            }
            else
            {
                // All habits.
                var report = _habitLoggerService.GetHabitLogSumReport(reportConfig.DateFrom.Value, reportConfig.DateTo.Value);
                foreach (var x in report)
                {
                    dataTable.Rows.Add([x.Name, x.Quantity, x.Measure]);
                }
            }
        }
        else
        {
            // View all dates.
            dataTable.Columns.Add("Date");
            dataTable.Columns.Add("Habit");
            dataTable.Columns.Add("Quantity");
            dataTable.Columns.Add("Measure");

            if (reportConfig.HabitId.HasValue)
            {
                // Specific habit.
                var report = _habitLoggerService.GetHabitLogReportByHabitId(reportConfig.HabitId.Value);
                foreach (var x in report)
                {
                    dataTable.Rows.Add([x.Date.ToShortDateString(), x.Name, x.Quantity, x.Measure]);
                }
            }
            else
            {
                // All habits.
                var report = _habitLoggerService.GetHabitLogReport();
                foreach (var x in report)
                {
                    dataTable.Rows.Add([x.Date.ToShortDateString(), x.Name, x.Quantity, x.Measure]);
                }
            }
        }

        var consoleTable = ConsoleTableBuilder.From(dataTable);

        MessagePage.Show("Habit Log Report", consoleTable.Export().ToString());
    }

    private void ViewHabitReportPage()
    {
        var habitReport = _habitLoggerService.GetHabitReport();

        var dataTable = new DataTable();
        dataTable.Columns.Add("Name");
        dataTable.Columns.Add("Measure");
        dataTable.Columns.Add("IsActive");
        foreach (var x in habitReport)
        {
            dataTable.Rows.Add([x.Name, x.Measure, x.IsActive]);
        }

        var consoleTable = ConsoleTableBuilder.From(dataTable);

        MessagePage.Show("Habit Report", consoleTable.Export().ToString());
    }

    #endregion
}
