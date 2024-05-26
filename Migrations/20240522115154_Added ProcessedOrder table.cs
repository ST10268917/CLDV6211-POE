using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Part2.Migrations
{
    /// <inheritdoc />
    public partial class AddedProcessedOrdertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CraftName",
                table: "Crafts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CraftDescription",
                table: "Crafts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProcessedOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CraftId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessedOrders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_ProcessedOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessedOrders_Crafts_CraftId",
                        column: x => x.CraftId,
                        principalTable: "Crafts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessedOrders_CraftId",
                table: "ProcessedOrders",
                column: "CraftId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessedOrders_UserId",
                table: "ProcessedOrders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessedOrders");

            migrationBuilder.AlterColumn<string>(
                name: "CraftName",
                table: "Crafts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CraftDescription",
                table: "Crafts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
