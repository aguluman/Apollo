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

    [Column("DefaultClockOut")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset? ClockOut
    {
        get => _clockOut ?? CalculateClockOut;
        set => _clockOut = value; //you can change here to set/init, if you want mutability down the line
    }
    
    [Column("UserClockOut")]
    private  DateTimeOffset? _clockOut; // and you can remove/add the readonly for mutability.
    
    [NotMapped]
    private DateTimeOffset CalculateClockOut
    {
        get
        {
            // If the custom ClockOut value is set, return it
            if (_clockOut.HasValue)
                return _clockOut.Value;

            // Check if the current time is past 5 PM
            var currentTime = DateTimeOffset.Now;
            var clockOutTime = new DateTimeOffset(currentTime.Year, currentTime.Month, currentTime.Day, 17, 0, 0, currentTime.Offset);

            // If it's past 5 PM, set the default clock out time to 5 PM
            return currentTime >= clockOutTime ? clockOutTime : currentTime;
        }
    }

    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public TimeSpan? TimeOffWork
    {
        get => CalculateTimeOffWork;
        set { }
    }
    
    [NotMapped]
    private TimeSpan? CalculateTimeOffWork
    {
        get
        {
            if (ClockIn == default || ClockOut == default)
                return null;

            return ClockOut.Value - ClockIn;
        }
    }
    
    
    [ForeignKey(nameof(Employee))] 
    public Guid EmployeeId { get; set; }
    
    public Employee Employee { get; set; }
    
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
    [Range(typeof(TimeSpan), "0:00", "1:30")]
    public TimeSpan BreakTime { get; set; }   

    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan? ActiveWorkTime
    {
        get => CalculateActiveWorkTime;
        set { }
    }

    [NotMapped]
    private TimeSpan? CalculateActiveWorkTime
    {
        get
        {
            if (!ClockOut.HasValue)
                return null;

            var totalBreakTime = BreakTime;
            var timeWorked = ClockOut.Value - ClockIn;
            var actualWorkTime = timeWorked - totalBreakTime;

            return actualWorkTime;
        }
    }
}