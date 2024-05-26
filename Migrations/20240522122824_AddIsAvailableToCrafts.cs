using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Part2.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAvailableToCrafts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Crafts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Crafts");
        }
    }
}
