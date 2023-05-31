using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Attendance
{
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Employee))] 
    public Guid EmployeeId { get; set; }
    
    public Employee Employee { get; set; }

    
    [Required(ErrorMessage = "Clock In is required")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset ClockIn { get; set; } 


    [Column("ClockOut")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset ClockOut { get; set; } 
    
    
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public TimeSpan TimeOffWork { get; set; }
    
    
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
    [Range(typeof(TimeSpan), "0:00", "1:30")]
    public TimeSpan BreakTime { get; set; }   

    public DateTimeOffset BreakTimeStart { get; set; } 

    public DateTimeOffset BreakTimeEnd { get; set; } 
    
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan ActiveWorkTime { get; set; }
}