using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class Map_multiole_rounds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TMap_Rounds_RoundId",
                table: "TMap");

            migrationBuilder.DropForeignKey(
                name: "FK_TMapSuggestion_Rounds_RoundId",
                table: "TMapSuggestion");

            migrationBuilder.DropIndex(
                name: "IX_TMapSuggestion_RoundId",
                table: "TMapSuggestion");

            migrationBuilder.DropIndex(
                name: "IX_TMap_RoundId",
                table: "TMap");

            migrationBuilder.DropColumn(
                name: "RoundId",
                table: "TMapSuggestion");

            migrationBuilder.DropColumn(
                name: "RoundId",
                table: "TMap");

            migrationBuilder.CreateTable(
                name: "RoundMap",
                columns: table => new
                {
                    MappoolId = table.Column<int>(type: "integer", nullable: false),
                    RoundsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundMap", x => new { x.MappoolId, x.RoundsId });
                    table.ForeignKey(
                        name: "FK_RoundMap_Rounds_RoundsId",
                        column: x => x.RoundsId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoundMap_TMap_MappoolId",
                        column: x => x.MappoolId,
                        principalTable: "TMap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoundMapSuggestion",
                columns: table => new
                {
                    MapSuggestionsId = table.Column<int>(type: "integer", nullable: false),
                    RoundsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundMapSuggestion", x => new { x.MapSuggestionsId, x.RoundsId });
                    table.ForeignKey(
                        name: "FK_RoundMapSuggestion_Rounds_RoundsId",
                        column: x => x.RoundsId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoundMapSuggestion_TMapSuggestion_MapSuggestionsId",
                        column: x => x.MapSuggestionsId,
                        principalTable: "TMapSuggestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoundMap_RoundsId",
                table: "RoundMap",
                column: "RoundsId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundMapSuggestion_RoundsId",
                table: "RoundMapSuggestion",
                column: "RoundsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoundMap");

            migrationBuilder.DropTable(
                name: "RoundMapSuggestion");

            migrationBuilder.AddColumn<int>(
                name: "RoundId",
                table: "TMapSuggestion",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoundId",
                table: "TMap",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TMapSuggestion_RoundId",
                table: "TMapSuggestion",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_TMap_RoundId",
                table: "TMap",
                column: "RoundId");

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
    }
}
