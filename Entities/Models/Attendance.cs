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
    public DateTimeOffset ClockIn { get; set; } /*= DateTimeOffset.Now; *///Todo: check if this is correct


    [Column("ClockOut")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset ClockOut { get; set; } /*= DateTimeOffset.Now;*/ //Todo: check if this is correct
    
    
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public TimeSpan? TimeOffWork { get; set; }
    
    
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
    [Range(typeof(TimeSpan), "0:00", "1:30")]
    public TimeSpan BreakTime { get; set; }   

    public DateTimeOffset BreakTimeStart { get; set; } /*= DateTimeOffset.Now;*/

    public DateTimeOffset BreakTimeEnd { get; set; } /*= DateTimeOffset.Now;*/
    
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan? ActiveWorkTime { get; set; }
}