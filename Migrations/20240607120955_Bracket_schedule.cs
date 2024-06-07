using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class Bracket_schedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MatchId",
                table: "Schedules",
                newName: "Score2");

            migrationBuilder.AddColumn<bool>(
                name: "IsKnockedDown",
                table: "TournamentPlayer",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsKnockedDown",
                table: "Team",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Loser",
                table: "Schedules",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MpLinkId",
                table: "Schedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Num",
                table: "Schedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoundNumber",
                table: "Schedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Score1",
                table: "Schedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Winner",
                table: "Schedules",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsKnockedDown",
                table: "TournamentPlayer");

            migrationBuilder.DropColumn(
                name: "IsKnockedDown",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Loser",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "MpLinkId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Num",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "RoundNumber",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Score1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Winner",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "Score2",
                table: "Schedules",
                newName: "MatchId");
        }
    }
}
