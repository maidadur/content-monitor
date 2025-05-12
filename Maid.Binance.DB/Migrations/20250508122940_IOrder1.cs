using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maid.Binance.DB.Migrations
{
    /// <inheritdoc />
    public partial class IOrder1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "BinanceOrders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Leverage",
                table: "BinanceOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "BinanceOrders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "BinanceOrders");

            migrationBuilder.DropColumn(
                name: "Leverage",
                table: "BinanceOrders");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "BinanceOrders");
        }
    }
}
