using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class is_knocked_out : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPlayer_Players_PlayersId",
                table: "TournamentPlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPlayer_Tournaments_TournamentsId",
                table: "TournamentPlayer");

            migrationBuilder.RenameColumn(
                name: "TournamentsId",
                table: "TournamentPlayer",
                newName: "TournamentId");

            migrationBuilder.RenameColumn(
                name: "PlayersId",
                table: "TournamentPlayer",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentPlayer_TournamentsId",
                table: "TournamentPlayer",
                newName: "IX_TournamentPlayer_TournamentId");

            migrationBuilder.AddColumn<bool>(
                name: "Isknockout",
                table: "TournamentPlayer",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isknockout",
                table: "Team",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentPlayer_Players_PlayerId",
                table: "TournamentPlayer",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentPlayer_Tournaments_TournamentId",
                table: "TournamentPlayer",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPlayer_Players_PlayerId",
                table: "TournamentPlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPlayer_Tournaments_TournamentId",
                table: "TournamentPlayer");

            migrationBuilder.DropColumn(
                name: "Isknockout",
                table: "TournamentPlayer");

            migrationBuilder.DropColumn(
                name: "Isknockout",
                table: "Team");

            migrationBuilder.RenameColumn(
                name: "TournamentId",
                table: "TournamentPlayer",
                newName: "TournamentsId");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "TournamentPlayer",
                newName: "PlayersId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentPlayer_TournamentId",
                table: "TournamentPlayer",
                newName: "IX_TournamentPlayer_TournamentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentPlayer_Players_PlayersId",
                table: "TournamentPlayer",
                column: "PlayersId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentPlayer_Tournaments_TournamentsId",
                table: "TournamentPlayer",
                column: "TournamentsId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
