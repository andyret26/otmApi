using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class Staff_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QualsSchedules_Staff_RefereeId",
                table: "QualsSchedules");

            migrationBuilder.DropTable(
                name: "TournamentStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_QualsSchedules_RefereeId",
                table: "QualsSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Staff",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "Staff",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RefereeTournamentId",
                table: "QualsSchedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                table: "Staff",
                columns: new[] { "Id", "TournamentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Staff_TournamentId",
                table: "Staff",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_QualsSchedules_RefereeId_RefereeTournamentId",
                table: "QualsSchedules",
                columns: new[] { "RefereeId", "RefereeTournamentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_QualsSchedules_Staff_RefereeId_RefereeTournamentId",
                table: "QualsSchedules",
                columns: new[] { "RefereeId", "RefereeTournamentId" },
                principalTable: "Staff",
                principalColumns: new[] { "Id", "TournamentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Tournaments_TournamentId",
                table: "Staff",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QualsSchedules_Staff_RefereeId_RefereeTournamentId",
                table: "QualsSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Tournaments_TournamentId",
                table: "Staff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_TournamentId",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_QualsSchedules_RefereeId_RefereeTournamentId",
                table: "QualsSchedules");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "RefereeTournamentId",
                table: "QualsSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Staff",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                table: "Staff",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TournamentStaff",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "integer", nullable: false),
                    TournamentsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentStaff", x => new { x.StaffId, x.TournamentsId });
                    table.ForeignKey(
                        name: "FK_TournamentStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentStaff_Tournaments_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QualsSchedules_RefereeId",
                table: "QualsSchedules",
                column: "RefereeId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentStaff_TournamentsId",
                table: "TournamentStaff",
                column: "TournamentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_QualsSchedules_Staff_RefereeId",
                table: "QualsSchedules",
                column: "RefereeId",
                principalTable: "Staff",
                principalColumn: "Id");
        }
    }
}
