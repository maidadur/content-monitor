using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maid.Binance.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddEmotionsAndTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BinanceOrderEmotion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BinanceOrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Emotion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinanceOrderEmotion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BinanceOrderEmotion_BinanceOrders_BinanceOrderId",
                        column: x => x.BinanceOrderId,
                        principalTable: "BinanceOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BinanceOrderTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BinanceOrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Tag = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinanceOrderTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BinanceOrderTag_BinanceOrders_BinanceOrderId",
                        column: x => x.BinanceOrderId,
                        principalTable: "BinanceOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BinanceOrderEmotion_BinanceOrderId",
                table: "BinanceOrderEmotion",
                column: "BinanceOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_BinanceOrderTag_BinanceOrderId",
                table: "BinanceOrderTag",
                column: "BinanceOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinanceOrderEmotion");

            migrationBuilder.DropTable(
                name: "BinanceOrderTag");
        }
    }
}
