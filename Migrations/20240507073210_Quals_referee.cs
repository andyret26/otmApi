using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class Quals_referee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Num",
                table: "QualsSchedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RefereeId",
                table: "QualsSchedules",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QualsSchedules_RefereeId",
                table: "QualsSchedules",
                column: "RefereeId");

            migrationBuilder.AddForeignKey(
                name: "FK_QualsSchedules_Staff_RefereeId",
                table: "QualsSchedules",
                column: "RefereeId",
                principalTable: "Staff",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QualsSchedules_Staff_RefereeId",
                table: "QualsSchedules");

            migrationBuilder.DropIndex(
                name: "IX_QualsSchedules_RefereeId",
                table: "QualsSchedules");

            migrationBuilder.DropColumn(
                name: "Num",
                table: "QualsSchedules");

            migrationBuilder.DropColumn(
                name: "RefereeId",
                table: "QualsSchedules");
        }
    }
}
