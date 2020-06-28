using Microsoft.EntityFrameworkCore.Migrations;

namespace Maid.Manga.DB.Migrations
{
    public partial class source4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChapterItemXpath",
                table: "MangaSource",
                newName: "ChapterXpath");

            migrationBuilder.AddColumn<string>(
                name: "ChapterDateXpath",
                table: "MangaSource",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChapterHrefXpath",
                table: "MangaSource",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChapterTitleXpath",
                table: "MangaSource",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChapterDateXpath",
                table: "MangaSource");

            migrationBuilder.DropColumn(
                name: "ChapterHrefXpath",
                table: "MangaSource");

            migrationBuilder.DropColumn(
                name: "ChapterTitleXpath",
                table: "MangaSource");

            migrationBuilder.RenameColumn(
                name: "ChapterXpath",
                table: "MangaSource",
                newName: "ChapterItemXpath");
        }
    }
}
