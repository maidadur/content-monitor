using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Maid.Manga.DB.Migrations
{
    public partial class MangaSourceLookup1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MangaSource",
                columns: new[] { "Id", "CreatedOn", "Name" },
                values: new object[] { new Guid("5540cd3a-755d-4b4c-9125-71a3c0258640"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FanFox" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MangaSource",
                keyColumn: "Id",
                keyValue: new Guid("5540cd3a-755d-4b4c-9125-71a3c0258640"));
        }
    }
}
