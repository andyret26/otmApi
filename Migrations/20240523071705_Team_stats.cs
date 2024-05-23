using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class Team_stats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.CreateTable(
                name: "PlayerStats",
                columns: table => new
                {
                    MapId = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    RoundId = table.Column<int>(type: "integer", nullable: false),
                    MapMod = table.Column<string>(type: "text", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    Acc = table.Column<decimal>(type: "numeric", nullable: false),
                    Mods = table.Column<List<string>>(type: "text[]", nullable: true),
                    MatchId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStats", x => new { x.MapId, x.PlayerId, x.RoundId });
                    table.ForeignKey(
                        name: "FK_PlayerStats_Maps_MapId_MapMod",
                        columns: x => new { x.MapId, x.MapMod },
                        principalTable: "Maps",
                        principalColumns: new[] { "Id", "Mod" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerStats_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerStats_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamStats",
                columns: table => new
                {
                    MapId = table.Column<int>(type: "integer", nullable: false),
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    RoundId = table.Column<int>(type: "integer", nullable: false),
                    MapMod = table.Column<string>(type: "text", nullable: false),
                    TotalScore = table.Column<int>(type: "integer", nullable: false),
                    AvgScore = table.Column<int>(type: "integer", nullable: false),
                    Acc = table.Column<decimal>(type: "numeric", nullable: false),
                    MatchId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStats", x => new { x.MapId, x.TeamId, x.RoundId });
                    table.ForeignKey(
                        name: "FK_TeamStats_Maps_MapId_MapMod",
                        columns: x => new { x.MapId, x.MapMod },
                        principalTable: "Maps",
                        principalColumns: new[] { "Id", "Mod" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamStats_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamStats_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStats_MapId_MapMod",
                table: "PlayerStats",
                columns: new[] { "MapId", "MapMod" });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStats_PlayerId",
                table: "PlayerStats",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStats_RoundId",
                table: "PlayerStats",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStats_MapId_MapMod",
                table: "TeamStats",
                columns: new[] { "MapId", "MapMod" });

            migrationBuilder.CreateIndex(
                name: "IX_TeamStats_RoundId",
                table: "TeamStats",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStats_TeamId",
                table: "TeamStats",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerStats");

            migrationBuilder.DropTable(
                name: "TeamStats");

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    MapId = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    RoundId = table.Column<int>(type: "integer", nullable: false),
                    MapMod = table.Column<string>(type: "text", nullable: false),
                    Acc = table.Column<decimal>(type: "numeric", nullable: false),
                    MatchId = table.Column<int>(type: "integer", nullable: false),
                    Mods = table.Column<List<string>>(type: "text[]", nullable: true),
                    Score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => new { x.MapId, x.PlayerId, x.RoundId });
                    table.ForeignKey(
                        name: "FK_Stats_Maps_MapId_MapMod",
                        columns: x => new { x.MapId, x.MapMod },
                        principalTable: "Maps",
                        principalColumns: new[] { "Id", "Mod" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stats_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stats_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stats_MapId_MapMod",
                table: "Stats",
                columns: new[] { "MapId", "MapMod" });

            migrationBuilder.CreateIndex(
                name: "IX_Stats_PlayerId",
                table: "Stats",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_RoundId",
                table: "Stats",
                column: "RoundId");
        }
    }
}
