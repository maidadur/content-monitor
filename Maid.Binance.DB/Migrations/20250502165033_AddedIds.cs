using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maid.Binance.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddedIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TradeId",
                table: "BinanceTrades",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "BinanceOrders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TradeId",
                table: "BinanceTrades");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "BinanceOrders");
        }
    }
}
