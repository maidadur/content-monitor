using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Maid.Manga.DB.Migrations
{
    public partial class Sources1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MangaSource",
                keyColumn: "Id",
                keyValue: new Guid("cb5cea99-ff7e-4272-0c11-08d6c115fe81"));

            migrationBuilder.DeleteData(
                table: "MangaSource",
                keyColumn: "Id",
                keyValue: new Guid("db5cea99-ff7e-4272-0c11-08d6c115fe81"));

            migrationBuilder.AddColumn<string>(
                name: "ChapterItemXpath",
                table: "MangaSource",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageXpath",
                table: "MangaSource",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleXpath",
                table: "MangaSource",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChapterItemXpath",
                table: "MangaSource");

            migrationBuilder.DropColumn(
                name: "ImageXpath",
                table: "MangaSource");

            migrationBuilder.DropColumn(
                name: "TitleXpath",
                table: "MangaSource");

            migrationBuilder.InsertData(
                table: "MangaSource",
                columns: new[] { "Id", "CreatedOn", "DomainUrl", "Name" },
                values: new object[] { new Guid("cb5cea99-ff7e-4272-0c11-08d6c115fe81"), new DateTime(2019, 7, 9, 20, 32, 24, 422, DateTimeKind.Local), "http://fanfox.net", "FanFox" });

            migrationBuilder.InsertData(
                table: "MangaSource",
                columns: new[] { "Id", "CreatedOn", "DomainUrl", "Name" },
                values: new object[] { new Guid("db5cea99-ff7e-4272-0c11-08d6c115fe81"), new DateTime(2019, 7, 9, 20, 32, 24, 422, DateTimeKind.Local), "https://mangarock.com", "MangaRock" });
        }
    }
}
