using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.ClockIn)
            .IsRequired();

        builder.Property(a => a.ClockOut)
            .IsRequired(false);
        
        builder.Property(a => a.TimeOffWork)
            .IsRequired(false);
        
        builder.Property(a => a.ActiveWorkTime)
            .IsRequired(false);
        
        

        builder.HasOne(a => a.Employee)
            .WithMany(e => e.Attendance)
            .HasForeignKey(a => a.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasData(
            new Attendance
            {
                Id = new Guid("1c15d6a9-6e63-4a2e-9b28-af2c6f18b6a5"),
                ClockIn = DateTimeOffset.Now.AddHours(-11),
                ClockOut = DateTimeOffset.Now.AddHours(-0.45f),
                TimeOffWork = TimeSpan.FromHours(16.15f),
                BreakTime = TimeSpan.FromMinutes(45),
                ActiveWorkTime = TimeSpan.FromHours(11.45f),
                EmployeeId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
            },
            new Attendance
            {
                Id = new Guid("3a55d1d3-97f8-497a-8bf7-878c5910e378"),
                ClockIn = DateTimeOffset.Now.AddHours(-11),
                ClockOut = DateTimeOffset.Now.AddHours(-1),
                TimeOffWork = TimeSpan.FromHours(14.0f),
                BreakTime = TimeSpan.FromMinutes(60),
                ActiveWorkTime = TimeSpan.FromHours(10.0f),
                EmployeeId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
            }
        );
        
        
    }
}