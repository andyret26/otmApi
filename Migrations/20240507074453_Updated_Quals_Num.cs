using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace otmApi.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Quals_Num : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Num",
                table: "QualsSchedules",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Num",
                table: "QualsSchedules",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
