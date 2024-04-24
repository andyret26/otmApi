using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class Context_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoundMap_TMap_MappoolId",
                table: "RoundMap");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundMapSuggestion_TMapSuggestion_MapSuggestionsId",
                table: "RoundMapSuggestion");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_TMap_MapId",
                table: "Stats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TMapSuggestion",
                table: "TMapSuggestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TMap",
                table: "TMap");

            migrationBuilder.RenameTable(
                name: "TMapSuggestion",
                newName: "MapSuggestions");

            migrationBuilder.RenameTable(
                name: "TMap",
                newName: "Maps");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapSuggestions",
                table: "MapSuggestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Maps",
                table: "Maps",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoundMap_Maps_MappoolId",
                table: "RoundMap",
                column: "MappoolId",
                principalTable: "Maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundMapSuggestion_MapSuggestions_MapSuggestionsId",
                table: "RoundMapSuggestion",
                column: "MapSuggestionsId",
                principalTable: "MapSuggestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Maps_MapId",
                table: "Stats",
                column: "MapId",
                principalTable: "Maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoundMap_Maps_MappoolId",
                table: "RoundMap");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundMapSuggestion_MapSuggestions_MapSuggestionsId",
                table: "RoundMapSuggestion");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Maps_MapId",
                table: "Stats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Maps",
                table: "Maps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MapSuggestions",
                table: "MapSuggestions");

            migrationBuilder.RenameTable(
                name: "Maps",
                newName: "TMap");

            migrationBuilder.RenameTable(
                name: "MapSuggestions",
                newName: "TMapSuggestion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TMap",
                table: "TMap",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TMapSuggestion",
                table: "TMapSuggestion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoundMap_TMap_MappoolId",
                table: "RoundMap",
                column: "MappoolId",
                principalTable: "TMap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundMapSuggestion_TMapSuggestion_MapSuggestionsId",
                table: "RoundMapSuggestion",
                column: "MapSuggestionsId",
                principalTable: "TMapSuggestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_TMap_MapId",
                table: "Stats",
                column: "MapId",
                principalTable: "TMap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
