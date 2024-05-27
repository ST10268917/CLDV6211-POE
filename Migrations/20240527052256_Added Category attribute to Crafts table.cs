using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Part2.Migrations
{
    /// <inheritdoc />
    public partial class AddedCategoryattributetoCraftstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Crafts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Crafts");
        }
    }
}
