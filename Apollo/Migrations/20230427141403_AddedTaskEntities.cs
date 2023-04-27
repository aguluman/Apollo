using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apollo.Migrations
{
    public partial class AddedTaskEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "CreatedAt", "Description", "DueAt", "EmployeeId", "Priority", "State", "Title" },
                values: new object[,]
                {
                    { new Guid("69d59c4d-4c77-4d77-b52e-51b69118dbcc"), new DateTime(2023, 4, 27, 15, 14, 3, 118, DateTimeKind.Local).AddTicks(4634), "Complete all the remaining tasks for project A", new DateTime(2023, 5, 11, 15, 14, 3, 118, DateTimeKind.Local).AddTicks(4660), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 2, 1, "Finish project A" },
                    { new Guid("e7e86390-dcf5-4d63-af31-f6187fc7646e"), new DateTime(2023, 4, 27, 15, 14, 3, 118, DateTimeKind.Local).AddTicks(4671), "Design and develop a new company website", new DateTime(2023, 5, 27, 15, 14, 3, 118, DateTimeKind.Local).AddTicks(4673), new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), 1, 0, "Create new website" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EmployeeId",
                table: "Tasks",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
