using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Part2.Migrations
{
    /// <inheritdoc />
    public partial class IsNotProcessedaddedtoOrdermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNotProcessed",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNotProcessed",
                table: "Orders");
        }
    }
}
