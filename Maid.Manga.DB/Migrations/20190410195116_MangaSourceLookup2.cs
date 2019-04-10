using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Maid.Manga.DB.Migrations
{
    public partial class MangaSourceLookup2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MangaSource",
                keyColumn: "Id",
                keyValue: new Guid("5540cd3a-755d-4b4c-9125-71a3c0258640"));

            migrationBuilder.AddColumn<string>(
                name: "DomainUrl",
                table: "MangaSource",
                nullable: true);

            migrationBuilder.InsertData(
                table: "MangaSource",
                columns: new[] { "Id", "CreatedOn", "DomainUrl", "Name" },
                values: new object[] { new Guid("36577215-2137-44cb-8ccc-06da710d0de0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FanFox" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MangaSource",
                keyColumn: "Id",
                keyValue: new Guid("36577215-2137-44cb-8ccc-06da710d0de0"));

            migrationBuilder.DropColumn(
                name: "DomainUrl",
                table: "MangaSource");

            migrationBuilder.InsertData(
                table: "MangaSource",
                columns: new[] { "Id", "CreatedOn", "Name" },
                values: new object[] { new Guid("5540cd3a-755d-4b4c-9125-71a3c0258640"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FanFox" });
        }
    }
}
