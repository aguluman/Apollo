using Entities.Models;
using Entities.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class TasksConfiguration : IEntityTypeConfiguration<Tasks>
{
    public void Configure(EntityTypeBuilder<Tasks> builder)
    {
        builder.Property(t => t.State)
            .HasConversion(
                s => s.ToString(), // convert enum to string
                s => (State)Enum.Parse(typeof(State), s) // convert string back to enum
            );

        builder.Property(t => t.Priority)
            .HasConversion(
                p => p.ToString(),
                p => (Priority)Enum.Parse(typeof(Priority), p)
                );
       
        builder.HasData(
            new Tasks
            {
                Id = new Guid("69d59c4d-4c77-4d77-b52e-51b69118dbcc"),
                Title = "Finish project A",
                Description = "Complete all the remaining tasks for project A",
                DueAt = DateTime.Now.AddDays(14),
                EmployeeId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                State = State.InProgress,
                Priority = Priority.High
            },
            new Tasks
            {
                Id = new Guid("e7e86390-dcf5-4d63-af31-f6187fc7646e"),
                Title = "Create new website",
                Description = "Design and develop a new company website",
                DueAt = DateTime.Now.AddDays(30),
                EmployeeId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                State = State.NotStarted,
                Priority = Priority.Normal
            }
        );
    }
}