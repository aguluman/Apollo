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
                ClockIn = DateTimeOffset.Now.AddHours(-8),
                ClockOut = DateTimeOffset.Now.AddHours(-0.45f),
                TimeOffWork = TimeSpan.FromHours(1.05f),
                ActiveWorkTime = TimeSpan.FromHours(8.15f),
                EmployeeId = new Guid("80ABBCA8-664D-4B20-B5DE-024705497D4A"),
            },
            new Attendance
            {
                Id = new Guid("3a55d1d3-97f8-497a-8bf7-878c5910e378"),
                ClockIn = DateTimeOffset.Now.AddHours(-9),
                ClockOut = DateTimeOffset.Now.AddHours(-1),
                TimeOffWork = TimeSpan.FromHours(1.2f),
                ActiveWorkTime = TimeSpan.FromHours(7.8f),
                EmployeeId = new Guid("86DBA8C0-D178-41E7-938C-ED49778FB52A"),
            }
        );

    }
}