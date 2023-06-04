using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apollo.Migrations
{
    public partial class AttendanceDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DueAt",
                table: "Tasks",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Tasks",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClockIn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ClockOut = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    WorkHours = table.Column<TimeSpan>(type: "time", nullable: false),
                    BreakTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    BreakTimeStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    BreakTimeEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ActiveWorkTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendance_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Attendance",
                columns: new[] { "Id", "ActiveWorkTime", "BreakTime", "BreakTimeEnd", "BreakTimeStart", "ClockIn", "ClockOut", "EmployeeId", "WorkHours" },
                values: new object[,]
                {
                    { new Guid("1c15d6a9-6e63-4a2e-9b28-af2c6f18b6a5"), new TimeSpan(0, 7, 15, 0, 0), new TimeSpan(0, 0, 45, 0, 0), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 4, 6, 28, 40, 910, DateTimeKind.Unspecified).AddTicks(8963), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 5, 1, 28, 40, 910, DateTimeKind.Unspecified).AddTicks(9107), new TimeSpan(0, 1, 0, 0, 0)), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("3a55d1d3-97f8-497a-8bf7-878c5910e378"), new TimeSpan(0, 7, 15, 0, 0), new TimeSpan(0, 1, 0, 0, 0), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 4, 7, 10, 40, 909, DateTimeKind.Unspecified).AddTicks(9115), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 5, 1, 28, 40, 910, DateTimeKind.Unspecified).AddTicks(9117), new TimeSpan(0, 1, 0, 0, 0)), new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), new TimeSpan(0, 8, 0, 0, 0) }
                });

            
            migrationBuilder.CreateIndex(
                name: "IX_Attendance_EmployeeId",
                table: "Attendance",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");
            
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueAt",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");
        }
    }
}
