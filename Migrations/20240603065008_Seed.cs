using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Seed",
                table: "TournamentPlayer",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Seed",
                table: "Team",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seed",
                table: "TournamentPlayer");

            migrationBuilder.DropColumn(
                name: "Seed",
                table: "Team");
        }
    }
}
