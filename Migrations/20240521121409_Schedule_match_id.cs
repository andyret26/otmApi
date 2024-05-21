using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class Schedule_match_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "Schedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "QualsSchedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MpLinkIsVisable",
                table: "QualsSchedules",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "QualsSchedules");

            migrationBuilder.DropColumn(
                name: "MpLinkIsVisable",
                table: "QualsSchedules");
        }
    }
}
