using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apollo.Migrations
{
    public partial class addedAttendanceAndDateTimeOffsetEVERYWHERE : Migration
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClockIn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DefaultClockOut = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    TimeOffWork = table.Column<TimeSpan>(type: "time", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BreakTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ActiveWorkTime = table.Column<TimeSpan>(type: "time", nullable: true)
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
                columns: new[] { "Id", "ActiveWorkTime", "BreakTime", "ClockIn", "DefaultClockOut", "EmployeeId", "TimeOffWork" },
                values: new object[,]
                {
                    { new Guid("1c15d6a9-6e63-4a2e-9b28-af2c6f18b6a5"), new TimeSpan(0, 0, 45, 0, 0), new TimeSpan(0, 0, 45, 0, 0), new DateTimeOffset(new DateTime(2023, 5, 25, 0, 35, 37, 976, DateTimeKind.Unspecified).AddTicks(637), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 5, 25, 8, 8, 37, 976, DateTimeKind.Unspecified).AddTicks(646), new TimeSpan(0, 1, 0, 0, 0)), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), new TimeSpan(271800000009) },
                    { new Guid("3a55d1d3-97f8-497a-8bf7-878c5910e378"), new TimeSpan(0, 1, 0, 0, 0), new TimeSpan(0, 1, 0, 0, 0), new DateTimeOffset(new DateTime(2023, 5, 24, 23, 35, 37, 976, DateTimeKind.Unspecified).AddTicks(656), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 5, 25, 7, 35, 37, 976, DateTimeKind.Unspecified).AddTicks(657), new TimeSpan(0, 1, 0, 0, 0)), new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), new TimeSpan(288000000001) }
                });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("69d59c4d-4c77-4d77-b52e-51b69118dbcc"),
                columns: new[] { "CreatedAt", "DueAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 5, 25, 8, 35, 37, 975, DateTimeKind.Unspecified).AddTicks(9219), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 8, 8, 35, 37, 975, DateTimeKind.Unspecified).AddTicks(9243), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("e7e86390-dcf5-4d63-af31-f6187fc7646e"),
                columns: new[] { "CreatedAt", "DueAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 5, 25, 8, 35, 37, 975, DateTimeKind.Unspecified).AddTicks(9259), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 24, 8, 35, 37, 975, DateTimeKind.Unspecified).AddTicks(9262), new TimeSpan(0, 1, 0, 0, 0)) });

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

           migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("69d59c4d-4c77-4d77-b52e-51b69118dbcc"),
                columns: new[] { "CreatedAt", "DueAt" },
                values: new object[] { new DateTime(2023, 5, 15, 12, 56, 47, 310, DateTimeKind.Local).AddTicks(1044), new DateTime(2023, 5, 29, 12, 56, 47, 310, DateTimeKind.Local).AddTicks(1061) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("e7e86390-dcf5-4d63-af31-f6187fc7646e"),
                columns: new[] { "CreatedAt", "DueAt" },
                values: new object[] { new DateTime(2023, 5, 15, 12, 56, 47, 310, DateTimeKind.Local).AddTicks(1068), new DateTime(2023, 6, 14, 12, 56, 47, 310, DateTimeKind.Local).AddTicks(1070) });
        }
    }
}
