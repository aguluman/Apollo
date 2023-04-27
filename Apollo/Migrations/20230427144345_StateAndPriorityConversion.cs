using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apollo.Migrations
{
    public partial class StateAndPriorityConversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Priority",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("69d59c4d-4c77-4d77-b52e-51b69118dbcc"),
                columns: new[] { "CreatedAt", "DueAt", "Priority", "State" },
                values: new object[] { new DateTime(2023, 4, 27, 15, 43, 45, 28, DateTimeKind.Local).AddTicks(1198), new DateTime(2023, 5, 11, 15, 43, 45, 28, DateTimeKind.Local).AddTicks(1214), "High", "InProgress" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("e7e86390-dcf5-4d63-af31-f6187fc7646e"),
                columns: new[] { "CreatedAt", "DueAt", "Priority", "State" },
                values: new object[] { new DateTime(2023, 4, 27, 15, 43, 45, 28, DateTimeKind.Local).AddTicks(1225), new DateTime(2023, 5, 27, 15, 43, 45, 28, DateTimeKind.Local).AddTicks(1228), "Normal", "NotStarted" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "Tasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "Tasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("69d59c4d-4c77-4d77-b52e-51b69118dbcc"),
                columns: new[] { "CreatedAt", "DueAt", "Priority", "State" },
                values: new object[] { new DateTime(2023, 4, 27, 15, 14, 3, 118, DateTimeKind.Local).AddTicks(4634), new DateTime(2023, 5, 11, 15, 14, 3, 118, DateTimeKind.Local).AddTicks(4660), 2, 1 });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("e7e86390-dcf5-4d63-af31-f6187fc7646e"),
                columns: new[] { "CreatedAt", "DueAt", "Priority", "State" },
                values: new object[] { new DateTime(2023, 4, 27, 15, 14, 3, 118, DateTimeKind.Local).AddTicks(4671), new DateTime(2023, 5, 27, 15, 14, 3, 118, DateTimeKind.Local).AddTicks(4673), 1, 0 });
        }
    }
}
