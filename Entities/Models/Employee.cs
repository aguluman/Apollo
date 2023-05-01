using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Employee
{
    [Column("EmployeeId")]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Employee first name is required")]
    [MaxLength(30, ErrorMessage = "Maximum length for the First Name is 30 characters")]
    public string? Name { get; set; }
    
    [Required(ErrorMessage = "Age is required")]
    public int Age { get; set; }
    
    [Required(ErrorMessage = "Position is a required field")]
    [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters")]
    public string? Position { get; set; }
    
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
    [Range(typeof(TimeSpan), "0:00", "1:30")]
    public TimeSpan BreakTime { get; set; } // Todo apply this change in EmployeeConfiguration
    
    [ForeignKey(nameof(Company))]
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; }
    public ICollection<Attendance> Attendance { get; set; }
    
    public Employee()
    {
        Attendance = new List<Attendance>();
    }
}