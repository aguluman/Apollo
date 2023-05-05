using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Models;

public class Attendance
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Clock In is required")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime ClockIn { get; set; }
    
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime? ClockOut { get; set; }
    
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public TimeSpan? TimeOffWork { get; set; }

    [ForeignKey(nameof(Employee))]
    public Guid EmployeeId { get; set; }
    
    public Employee Employee { get; set; }
    
    [ForeignKey(nameof(Company))]
    public Guid CompanyId { get; set; }
    
    public Company Company { get; set; }

    /*[DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan? ActiveWorkTime
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
    }*/
}