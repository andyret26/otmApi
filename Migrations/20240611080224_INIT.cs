using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MapSuggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Mod = table.Column<string>(type: "text", nullable: false),
                    OrderNumber = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Artist = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: false),
                    Difficulty_rating = table.Column<decimal>(type: "numeric", nullable: false),
                    Bpm = table.Column<decimal>(type: "numeric", nullable: false),
                    Total_length = table.Column<decimal>(type: "numeric", nullable: false),
                    Cs = table.Column<decimal>(type: "numeric", nullable: false),
                    Ar = table.Column<decimal>(type: "numeric", nullable: false),
                    Accuracy = table.Column<decimal>(type: "numeric", nullable: false),
                    Mapper = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapSuggestions", x => new { x.Id, x.Mod });
                });

            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Mod = table.Column<string>(type: "text", nullable: false),
                    OrderNumber = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Artist = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: false),
                    Difficulty_rating = table.Column<decimal>(type: "numeric", nullable: false),
                    Bpm = table.Column<decimal>(type: "numeric", nullable: false),
                    Total_length = table.Column<decimal>(type: "numeric", nullable: false),
                    Cs = table.Column<decimal>(type: "numeric", nullable: false),
                    Ar = table.Column<decimal>(type: "numeric", nullable: false),
                    Accuracy = table.Column<decimal>(type: "numeric", nullable: false),
                    Mapper = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => new { x.Id, x.Mod });
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    DiscordUsername = table.Column<string>(type: "text", nullable: true),
                    Avatar_url = table.Column<string>(type: "text", nullable: false),
                    Global_rank = table.Column<int>(type: "integer", nullable: false),
                    Country_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FormuPostLink = table.Column<string>(type: "text", nullable: true),
                    IsTeamTourney = table.Column<bool>(type: "boolean", nullable: false),
                    Format = table.Column<string>(type: "text", nullable: false),
                    MaxTeamSize = table.Column<int>(type: "integer", nullable: false),
                    RankRange = table.Column<string>(type: "text", nullable: false),
                    HowManyQualifies = table.Column<string>(type: "text", nullable: true),
                    HostId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_Hosts_HostId",
                        column: x => x.HostId,
                        principalTable: "Hosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsQualifier = table.Column<bool>(type: "boolean", nullable: false),
                    TournamentId = table.Column<int>(type: "integer", nullable: false),
                    IsMpLinksPublic = table.Column<bool>(type: "boolean", nullable: false),
                    IsStatsPublic = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rounds_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    TournamentId = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Roles = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => new { x.Id, x.TournamentId });
                    table.ForeignKey(
                        name: "FK_Staff_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TournamentId = table.Column<int>(type: "integer", nullable: false),
                    TeamName = table.Column<string>(type: "text", nullable: false),
                    Isknockout = table.Column<bool>(type: "boolean", nullable: false),
                    IsKnockedDown = table.Column<bool>(type: "boolean", nullable: false),
                    Seed = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TournamentPlayer",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    TournamentId = table.Column<int>(type: "integer", nullable: false),
                    Isknockout = table.Column<bool>(type: "boolean", nullable: false),
                    IsKnockedDown = table.Column<bool>(type: "boolean", nullable: false),
                    Seed = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentPlayer", x => new { x.PlayerId, x.TournamentId });
                    table.ForeignKey(
                        name: "FK_TournamentPlayer_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentPlayer_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "QualsSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoundId = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Names = table.Column<List<string>>(type: "text[]", nullable: true),
                    Referee = table.Column<string>(type: "text", nullable: true),
                    Num = table.Column<string>(type: "text", nullable: false),
                    MatchId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualsSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualsSchedules_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoundMap",
                columns: table => new
                {
                    RoundsId = table.Column<int>(type: "integer", nullable: false),
                    MappoolId = table.Column<int>(type: "integer", nullable: false),
                    MappoolMod = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundMap", x => new { x.RoundsId, x.MappoolId, x.MappoolMod });
                    table.ForeignKey(
                        name: "FK_RoundMap_Maps_MappoolId_MappoolMod",
                        columns: x => new { x.MappoolId, x.MappoolMod },
                        principalTable: "Maps",
                        principalColumns: new[] { "Id", "Mod" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoundMap_Rounds_RoundsId",
                        column: x => x.RoundsId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoundMapSuggestion",
                columns: table => new
                {
                    RoundsId = table.Column<int>(type: "integer", nullable: false),
                    MapSuggestionsId = table.Column<int>(type: "integer", nullable: false),
                    MapSuggestionsMod = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundMapSuggestion", x => new { x.RoundsId, x.MapSuggestionsId, x.MapSuggestionsMod });
                    table.ForeignKey(
                        name: "FK_RoundMapSuggestion_MapSuggestions_MapSuggestionsId_MapSugge~",
                        columns: x => new { x.MapSuggestionsId, x.MapSuggestionsMod },
                        principalTable: "MapSuggestions",
                        principalColumns: new[] { "Id", "Mod" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoundMapSuggestion_Rounds_RoundsId",
                        column: x => x.RoundsId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Num = table.Column<int>(type: "integer", nullable: false),
                    RoundId = table.Column<int>(type: "integer", nullable: false),
                    RoundNumber = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Referee = table.Column<string>(type: "text", nullable: true),
                    Streamer = table.Column<string>(type: "text", nullable: true),
                    Commentators = table.Column<List<string>>(type: "text[]", nullable: true),
                    Name1 = table.Column<string>(type: "text", nullable: true),
                    Name2 = table.Column<string>(type: "text", nullable: true),
                    Score1 = table.Column<int>(type: "integer", nullable: true),
                    Score2 = table.Column<int>(type: "integer", nullable: true),
                    Winner = table.Column<string>(type: "text", nullable: true),
                    Loser = table.Column<string>(type: "text", nullable: true),
                    IsInLosersBracket = table.Column<bool>(type: "boolean", nullable: false),
                    MpLinkId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamPlayer",
                columns: table => new
                {
                    PlayersId = table.Column<int>(type: "integer", nullable: false),
                    TeamsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayer", x => new { x.PlayersId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_TeamPlayer_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamPlayer_Teams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
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
                        name: "FK_TeamStats_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
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
                name: "IX_QualsSchedules_RoundId",
                table: "QualsSchedules",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundMap_MappoolId_MappoolMod",
                table: "RoundMap",
                columns: new[] { "MappoolId", "MappoolMod" });

            migrationBuilder.CreateIndex(
                name: "IX_RoundMapSuggestion_MapSuggestionsId_MapSuggestionsMod",
                table: "RoundMapSuggestion",
                columns: new[] { "MapSuggestionsId", "MapSuggestionsMod" });

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_TournamentId",
                table: "Rounds",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RoundId",
                table: "Schedules",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_TournamentId",
                table: "Staff",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_TeamsId",
                table: "TeamPlayer",
                column: "TeamsId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TournamentId",
                table: "Teams",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPlayer_TournamentId",
                table: "TournamentPlayer",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_HostId",
                table: "Tournaments",
                column: "HostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerStats");

            migrationBuilder.DropTable(
                name: "QualsSchedules");

            migrationBuilder.DropTable(
                name: "RoundMap");

            migrationBuilder.DropTable(
                name: "RoundMapSuggestion");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "TeamPlayer");

            migrationBuilder.DropTable(
                name: "TeamStats");

            migrationBuilder.DropTable(
                name: "TournamentPlayer");

            migrationBuilder.DropTable(
                name: "MapSuggestions");

            migrationBuilder.DropTable(
                name: "Maps");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "Hosts");
        }
    }
}
