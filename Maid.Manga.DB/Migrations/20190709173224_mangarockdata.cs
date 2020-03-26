using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Maid.Manga.DB.Migrations
{
    public partial class mangarockdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MangaSource",
                keyColumn: "Id",
                keyValue: new Guid("cb5cea99-ff7e-4272-0c11-08d6c115fe81"),
                column: "CreatedOn",
                value: new DateTime(2019, 7, 9, 20, 32, 24, 422, DateTimeKind.Local));

            migrationBuilder.InsertData(
                table: "MangaSource",
                columns: new[] { "Id", "CreatedOn", "DomainUrl", "Name" },
                values: new object[] { new Guid("db5cea99-ff7e-4272-0c11-08d6c115fe81"), new DateTime(2019, 7, 9, 20, 32, 24, 422, DateTimeKind.Local), "https://mangarock.com", "MangaRock" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MangaSource",
                keyColumn: "Id",
                keyValue: new Guid("db5cea99-ff7e-4272-0c11-08d6c115fe81"));

            migrationBuilder.UpdateData(
                table: "MangaSource",
                keyColumn: "Id",
                keyValue: new Guid("cb5cea99-ff7e-4272-0c11-08d6c115fe81"),
                column: "CreatedOn",
                value: new DateTime(2019, 4, 17, 22, 44, 4, 591, DateTimeKind.Local));
        }
    }
}
