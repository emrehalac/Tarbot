using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class entityNamesChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminder_Inek_InekId",
                table: "Reminder");

            migrationBuilder.DropTable(
                name: "Inek");

            migrationBuilder.DropIndex(
                name: "IX_Reminder_InekId",
                table: "Reminder");

            migrationBuilder.AddColumn<int>(
                name: "CowId",
                table: "Reminder",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PregnancyStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cow_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_CowId",
                table: "Reminder",
                column: "CowId");

            migrationBuilder.CreateIndex(
                name: "IX_Cow_UserId",
                table: "Cow",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminder_Cow_CowId",
                table: "Reminder",
                column: "CowId",
                principalTable: "Cow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminder_Cow_CowId",
                table: "Reminder");

            migrationBuilder.DropTable(
                name: "Cow");

            migrationBuilder.DropIndex(
                name: "IX_Reminder_CowId",
                table: "Reminder");

            migrationBuilder.DropColumn(
                name: "CowId",
                table: "Reminder");

            migrationBuilder.CreateTable(
                name: "Inek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    PregnancyStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inek_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_InekId",
                table: "Reminder",
                column: "InekId");

            migrationBuilder.CreateIndex(
                name: "IX_Inek_UserId",
                table: "Inek",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminder_Inek_InekId",
                table: "Reminder",
                column: "InekId",
                principalTable: "Inek",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
