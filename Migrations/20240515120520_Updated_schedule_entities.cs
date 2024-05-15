using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class Updated_schedule_entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_QualsSchedules_QualsScheduleId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_QualsSchedules_Staff_RefereeId_RefereeTournamentId",
                table: "QualsSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Players_Player1Id",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Players_Player2Id",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Team_Team1Id",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Team_Team2Id",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_QualsSchedules_QualsScheduleId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_QualsScheduleId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_Player1Id",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_Player2Id",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_Team1Id",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_Team2Id",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_QualsSchedules_RefereeId_RefereeTournamentId",
                table: "QualsSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Players_QualsScheduleId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "QualsScheduleId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Player1Id",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Player2Id",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Team1Id",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Team2Id",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "RefereeId",
                table: "QualsSchedules");

            migrationBuilder.DropColumn(
                name: "RefereeTournamentId",
                table: "QualsSchedules");

            migrationBuilder.DropColumn(
                name: "QualsScheduleId",
                table: "Players");

            migrationBuilder.AddColumn<List<string>>(
                name: "Commentators",
                table: "Schedules",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name1",
                table: "Schedules",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name2",
                table: "Schedules",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Referee",
                table: "Schedules",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Streamer",
                table: "Schedules",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Names",
                table: "QualsSchedules",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Referee",
                table: "QualsSchedules",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commentators",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Name1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Name2",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Referee",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Streamer",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Names",
                table: "QualsSchedules");

            migrationBuilder.DropColumn(
                name: "Referee",
                table: "QualsSchedules");

            migrationBuilder.AddColumn<int>(
                name: "QualsScheduleId",
                table: "Team",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player1Id",
                table: "Schedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player2Id",
                table: "Schedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Team1Id",
                table: "Schedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Team2Id",
                table: "Schedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RefereeId",
                table: "QualsSchedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RefereeTournamentId",
                table: "QualsSchedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QualsScheduleId",
                table: "Players",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_QualsScheduleId",
                table: "Team",
                column: "QualsScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Player1Id",
                table: "Schedules",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Player2Id",
                table: "Schedules",
                column: "Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Team1Id",
                table: "Schedules",
                column: "Team1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Team2Id",
                table: "Schedules",
                column: "Team2Id");

            migrationBuilder.CreateIndex(
                name: "IX_QualsSchedules_RefereeId_RefereeTournamentId",
                table: "QualsSchedules",
                columns: new[] { "RefereeId", "RefereeTournamentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Players_QualsScheduleId",
                table: "Players",
                column: "QualsScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_QualsSchedules_QualsScheduleId",
                table: "Players",
                column: "QualsScheduleId",
                principalTable: "QualsSchedules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QualsSchedules_Staff_RefereeId_RefereeTournamentId",
                table: "QualsSchedules",
                columns: new[] { "RefereeId", "RefereeTournamentId" },
                principalTable: "Staff",
                principalColumns: new[] { "Id", "TournamentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Players_Player1Id",
                table: "Schedules",
                column: "Player1Id",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Players_Player2Id",
                table: "Schedules",
                column: "Player2Id",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Team_Team1Id",
                table: "Schedules",
                column: "Team1Id",
                principalTable: "Team",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Team_Team2Id",
                table: "Schedules",
                column: "Team2Id",
                principalTable: "Team",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_QualsSchedules_QualsScheduleId",
                table: "Team",
                column: "QualsScheduleId",
                principalTable: "QualsSchedules",
                principalColumn: "Id");
        }
    }
}
