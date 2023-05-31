using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apollo.Migrations
{
    public partial class refactoredAttendanceEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DefaultClockOut",
                table: "Attendance",
                newName: "ClockOut");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "TimeOffWork",
                table: "Attendance",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ActiveWorkTime",
                table: "Attendance",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Attendance",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "newsequentialid()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ClockOut",
                table: "Attendance",
                type: "datetimeoffset",
                nullable: true, //todo change to false
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "BreakTimeEnd",
                table: "Attendance",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "BreakTimeStart",
                table: "Attendance",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreakTimeEnd",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "BreakTimeStart",
                table: "Attendance");

            migrationBuilder.RenameColumn(
                name: "ClockOut",
                table: "Attendance",
                newName: "DefaultClockOut");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "TimeOffWork",
                table: "Attendance",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ActiveWorkTime",
                table: "Attendance",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Attendance",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "newsequentialid()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DefaultClockOut",
                table: "Attendance",
                type: "datetimeoffset",
                nullable: true, //todo: change to false
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            
        }
    }
}
