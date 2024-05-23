using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class Round_mp_stats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MpLinkIsVisable",
                table: "QualsSchedules");

            migrationBuilder.AddColumn<bool>(
                name: "IsMpLinksPublic",
                table: "Rounds",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStatsPublic",
                table: "Rounds",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMpLinksPublic",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "IsStatsPublic",
                table: "Rounds");

            migrationBuilder.AddColumn<bool>(
                name: "MpLinkIsVisable",
                table: "QualsSchedules",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
