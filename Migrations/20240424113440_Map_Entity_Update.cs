using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class Map_Entity_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "TMapSuggestion");

            migrationBuilder.RenameColumn(
                name: "Sr",
                table: "TMapSuggestion",
                newName: "Total_length");

            migrationBuilder.RenameColumn(
                name: "Od",
                table: "TMapSuggestion",
                newName: "Difficulty_rating");

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "TMapSuggestion",
                newName: "Accuracy");

            migrationBuilder.AlterColumn<decimal>(
                name: "Bpm",
                table: "TMapSuggestion",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "TMapSuggestion",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "TMapSuggestion");

            migrationBuilder.RenameColumn(
                name: "Total_length",
                table: "TMapSuggestion",
                newName: "Sr");

            migrationBuilder.RenameColumn(
                name: "Difficulty_rating",
                table: "TMapSuggestion",
                newName: "Od");

            migrationBuilder.RenameColumn(
                name: "Accuracy",
                table: "TMapSuggestion",
                newName: "Length");

            migrationBuilder.AlterColumn<int>(
                name: "Bpm",
                table: "TMapSuggestion",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "TMapSuggestion",
                type: "text",
                nullable: true);
        }
    }
}
