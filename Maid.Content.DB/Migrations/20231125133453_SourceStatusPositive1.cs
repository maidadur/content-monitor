using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maid.Content.DB.Migrations
{
    /// <inheritdoc />
    public partial class SourceStatusPositive1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStatusPositive",
                table: "ContentInfo",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStatusPositive",
                table: "ContentInfo");
        }
    }
}
