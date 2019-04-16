using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Maid.Manga.DB.Migrations
{
    public partial class SourceFK1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MangaSource",
                keyColumn: "Id",
                keyValue: new Guid("adfb04fe-231c-41ed-8623-5ea3bb50c400"));

            migrationBuilder.InsertData(
                table: "MangaSource",
                columns: new[] { "Id", "CreatedOn", "DomainUrl", "Name" },
                values: new object[] { new Guid("cb5cea99-ff7e-4272-0c11-08d6c115fe81"), new DateTime(2019, 4, 16, 20, 30, 42, 842, DateTimeKind.Local), "http://fanfox.net", "FanFox" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MangaSource",
                keyColumn: "Id",
                keyValue: new Guid("cb5cea99-ff7e-4272-0c11-08d6c115fe81"));

            migrationBuilder.InsertData(
                table: "MangaSource",
                columns: new[] { "Id", "CreatedOn", "DomainUrl", "Name" },
                values: new object[] { new Guid("adfb04fe-231c-41ed-8623-5ea3bb50c400"), new DateTime(2019, 4, 16, 20, 29, 45, 569, DateTimeKind.Local), "http://fanfox.net", "FanFox" });
        }
    }
}
