using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apollo.Migrations
{
    public partial class AttendanceAndEmployeeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "BreakTime",
                table: "Employees",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClockIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClockOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeOffWork = table.Column<TimeSpan>(type: "time", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });
            
            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "Id", "ClockIn", "ClockOut", "CompanyId", "EmployeeId", "TimeOffWork" },
                values: new object[,]
                {
                    { new Guid("1c15d6a9-6e63-4a2e-9b28-af2c6f18b6a5"), new DateTime(2023, 5, 1, 8, 38, 38, 857, DateTimeKind.Local).AddTicks(8348), new DateTime(2023, 5, 1, 16, 11, 38, 857, DateTimeKind.Local).AddTicks(8353), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), new TimeSpan(0, 1, 0, 0, 0) },
                    { new Guid("3a55d1d3-97f8-497a-8bf7-878c5910e378"), new DateTime(2023, 5, 1, 7, 38, 38, 857, DateTimeKind.Local).AddTicks(8363), new DateTime(2023, 5, 1, 15, 38, 38, 857, DateTimeKind.Local).AddTicks(8363), new Guid("aee67334-35ee-428a-b826-08db2ca52a22"), new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), new TimeSpan(43200001716) }
                });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                column: "BreakTime",
                value: new TimeSpan(0, 1, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                column: "BreakTime",
                value: new TimeSpan(0, 0, 45, 0, 0));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                column: "BreakTime",
                value: new TimeSpan(0, 0, 40, 0, 0));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("69d59c4d-4c77-4d77-b52e-51b69118dbcc"),
                columns: new[] { "CreatedAt", "DueAt" },
                values: new object[] { new DateTime(2023, 5, 1, 16, 38, 38, 857, DateTimeKind.Local).AddTicks(5978), new DateTime(2023, 5, 15, 16, 38, 38, 857, DateTimeKind.Local).AddTicks(5997) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("e7e86390-dcf5-4d63-af31-f6187fc7646e"),
                columns: new[] { "CreatedAt", "DueAt" },
                values: new object[] { new DateTime(2023, 5, 1, 16, 38, 38, 857, DateTimeKind.Local).AddTicks(6004), new DateTime(2023, 5, 31, 16, 38, 38, 857, DateTimeKind.Local).AddTicks(6006) });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_CompanyId",
                table: "Attendances",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmployeeId",
                table: "Attendances",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropColumn(
                name: "BreakTime",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("69d59c4d-4c77-4d77-b52e-51b69118dbcc"),
                columns: new[] { "CreatedAt", "DueAt" },
                values: new object[] { new DateTime(2023, 4, 27, 15, 43, 45, 28, DateTimeKind.Local).AddTicks(1198), new DateTime(2023, 5, 11, 15, 43, 45, 28, DateTimeKind.Local).AddTicks(1214) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("e7e86390-dcf5-4d63-af31-f6187fc7646e"),
                columns: new[] { "CreatedAt", "DueAt" },
                values: new object[] { new DateTime(2023, 4, 27, 15, 43, 45, 28, DateTimeKind.Local).AddTicks(1225), new DateTime(2023, 5, 27, 15, 43, 45, 28, DateTimeKind.Local).AddTicks(1228) });
        }
    }
}
