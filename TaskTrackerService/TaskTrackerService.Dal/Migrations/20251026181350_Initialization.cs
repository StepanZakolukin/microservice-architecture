using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTrackerService.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Initialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "task_tracker_service");

            migrationBuilder.CreateTable(
                name: "Board",
                schema: "task_tracker_service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Board", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priority",
                schema: "task_tracker_service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardEditor",
                schema: "task_tracker_service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    BoardId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardEditor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardEditor_Board_BoardId",
                        column: x => x.BoardId,
                        principalSchema: "task_tracker_service",
                        principalTable: "Board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Column",
                schema: "task_tracker_service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    BoardId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Column", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Column_Board_BoardId",
                        column: x => x.BoardId,
                        principalSchema: "task_tracker_service",
                        principalTable: "Board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                schema: "task_tracker_service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Completed = table.Column<bool>(type: "boolean", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PriorityId = table.Column<Guid>(type: "uuid", nullable: false),
                    ColumnId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Column_ColumnId",
                        column: x => x.ColumnId,
                        principalSchema: "task_tracker_service",
                        principalTable: "Column",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalSchema: "task_tracker_service",
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardEditor_BoardId",
                schema: "task_tracker_service",
                table: "BoardEditor",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Column_BoardId",
                schema: "task_tracker_service",
                table: "Column",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_ColumnId",
                schema: "task_tracker_service",
                table: "Task",
                column: "ColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_PriorityId",
                schema: "task_tracker_service",
                table: "Task",
                column: "PriorityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardEditor",
                schema: "task_tracker_service");

            migrationBuilder.DropTable(
                name: "Task",
                schema: "task_tracker_service");

            migrationBuilder.DropTable(
                name: "Column",
                schema: "task_tracker_service");

            migrationBuilder.DropTable(
                name: "Priority",
                schema: "task_tracker_service");

            migrationBuilder.DropTable(
                name: "Board",
                schema: "task_tracker_service");
        }
    }
}
