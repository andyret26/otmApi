using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class is_qualifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsQualifier",
                table: "Rounds",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsQualifier",
                table: "Rounds");
        }
    }
}
