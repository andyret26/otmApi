using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class Stats_matchId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Staff");

            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "Stats",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Stats");

            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "Staff",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
