using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Attendance
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Clock In is required")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset ClockIn { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset? ClockOut
    {
        get => CalculateClockOut;
        set => CalculateClockOut = (DateTimeOffset)value;
        //Todo: if logic breaks, check here!!
    }

    [Column("DefaultClockOut")]
    private DateTimeOffset CalculateClockOut
    {
        get
        {
            // If the clock out time is already set, return the existing value
            if (ClockOut != null)
                return ClockOut.Value;

            // Check if the current time is past 5 PM
            var currentTime = DateTimeOffset.Now;
            var clockOutTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 17, 0, 0);

            // If it's past 5 PM, set the clock out time to 5 PM
            ClockOut = currentTime >= clockOutTime
                ? clockOutTime
                :
                // If it's before 5 PM, set the clock out time to the current time
                currentTime;

            return ClockOut.Value;
        }
        set => ClockOut = value;
    }

    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public TimeSpan? TimeOffWork
    {
        get => CalculateTimeOffWork();
        set => CalculateTimeOffWork();
    }

    private TimeSpan? CalculateTimeOffWork()
    {
        return ClockOut - ClockIn;
    }

    [ForeignKey(nameof(Employee))] 
    public Guid EmployeeId { get; set; }
    
    public Employee Employee { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan? ActiveWorkTime
    {
        get => CalculateActiveWorkTime;
        set => CalculateActiveWorkTime = value;
    }

    private TimeSpan? CalculateActiveWorkTime
    {
        get
        {
            if (!ClockOut.HasValue)
                return null;

            var totalBreakTime = Employee.BreakTime;
            var timeWorked = ClockOut.Value - ClockIn;
            var actualWorkTime = timeWorked - totalBreakTime;

            return timeWorked - actualWorkTime;
        }
        set => ActiveWorkTime = value;
    }
}