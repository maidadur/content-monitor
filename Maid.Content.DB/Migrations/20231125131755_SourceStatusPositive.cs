using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maid.Content.DB.Migrations
{
    /// <inheritdoc />
    public partial class SourceStatusPositive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PositiveStatusText",
                table: "ContentSource",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositiveStatusText",
                table: "ContentSource");
        }
    }
}
