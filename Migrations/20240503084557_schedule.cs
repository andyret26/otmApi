using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class schedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QualsScheduleId",
                table: "Team",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QualsScheduleId",
                table: "Players",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QualsSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoundId = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualsSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualsSchedules_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoundId = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Team1Id = table.Column<int>(type: "integer", nullable: true),
                    Team2Id = table.Column<int>(type: "integer", nullable: true),
                    Player1Id = table.Column<int>(type: "integer", nullable: true),
                    Player2Id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Players_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedules_Players_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedules_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Team_Team1Id",
                        column: x => x.Team1Id,
                        principalTable: "Team",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedules_Team_Team2Id",
                        column: x => x.Team2Id,
                        principalTable: "Team",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Team_QualsScheduleId",
                table: "Team",
                column: "QualsScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_QualsScheduleId",
                table: "Players",
                column: "QualsScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_QualsSchedules_RoundId",
                table: "QualsSchedules",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Player1Id",
                table: "Schedules",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Player2Id",
                table: "Schedules",
                column: "Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RoundId",
                table: "Schedules",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Team1Id",
                table: "Schedules",
                column: "Team1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Team2Id",
                table: "Schedules",
                column: "Team2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_QualsSchedules_QualsScheduleId",
                table: "Players",
                column: "QualsScheduleId",
                principalTable: "QualsSchedules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_QualsSchedules_QualsScheduleId",
                table: "Team",
                column: "QualsScheduleId",
                principalTable: "QualsSchedules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_QualsSchedules_QualsScheduleId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_QualsSchedules_QualsScheduleId",
                table: "Team");

            migrationBuilder.DropTable(
                name: "QualsSchedules");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Team_QualsScheduleId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Players_QualsScheduleId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "QualsScheduleId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "QualsScheduleId",
                table: "Players");
        }
    }
}
