using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Map : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Maps");

            migrationBuilder.RenameColumn(
                name: "Sr",
                table: "Maps",
                newName: "Total_length");

            migrationBuilder.RenameColumn(
                name: "Od",
                table: "Maps",
                newName: "Difficulty_rating");

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "Maps",
                newName: "Accuracy");

            migrationBuilder.AlterColumn<decimal>(
                name: "Bpm",
                table: "Maps",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Maps",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Maps");

            migrationBuilder.RenameColumn(
                name: "Total_length",
                table: "Maps",
                newName: "Sr");

            migrationBuilder.RenameColumn(
                name: "Difficulty_rating",
                table: "Maps",
                newName: "Od");

            migrationBuilder.RenameColumn(
                name: "Accuracy",
                table: "Maps",
                newName: "Length");

            migrationBuilder.AlterColumn<int>(
                name: "Bpm",
                table: "Maps",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Maps",
                type: "text",
                nullable: true);
        }
    }
}
