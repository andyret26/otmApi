using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class Round : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Round_Tournaments_TournamentId",
                table: "Round");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Round_RoundId",
                table: "Stats");

            migrationBuilder.DropForeignKey(
                name: "FK_TMap_Round_RoundId",
                table: "TMap");

            migrationBuilder.DropForeignKey(
                name: "FK_TMapSuggestion_Round_RoundId",
                table: "TMapSuggestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Round",
                table: "Round");

            migrationBuilder.RenameTable(
                name: "Round",
                newName: "Rounds");

            migrationBuilder.RenameIndex(
                name: "IX_Round_TournamentId",
                table: "Rounds",
                newName: "IX_Rounds_TournamentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rounds",
                table: "Rounds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Tournaments_TournamentId",
                table: "Rounds",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Rounds_RoundId",
                table: "Stats",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TMap_Rounds_RoundId",
                table: "TMap",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TMapSuggestion_Rounds_RoundId",
                table: "TMapSuggestion",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Tournaments_TournamentId",
                table: "Rounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Rounds_RoundId",
                table: "Stats");

            migrationBuilder.DropForeignKey(
                name: "FK_TMap_Rounds_RoundId",
                table: "TMap");

            migrationBuilder.DropForeignKey(
                name: "FK_TMapSuggestion_Rounds_RoundId",
                table: "TMapSuggestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rounds",
                table: "Rounds");

            migrationBuilder.RenameTable(
                name: "Rounds",
                newName: "Round");

            migrationBuilder.RenameIndex(
                name: "IX_Rounds_TournamentId",
                table: "Round",
                newName: "IX_Round_TournamentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Round",
                table: "Round",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Round_Tournaments_TournamentId",
                table: "Round",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Round_RoundId",
                table: "Stats",
                column: "RoundId",
                principalTable: "Round",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TMap_Round_RoundId",
                table: "TMap",
                column: "RoundId",
                principalTable: "Round",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TMapSuggestion_Round_RoundId",
                table: "TMapSuggestion",
                column: "RoundId",
                principalTable: "Round",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
