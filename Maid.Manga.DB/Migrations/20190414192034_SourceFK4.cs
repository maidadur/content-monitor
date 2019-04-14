using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Maid.Manga.DB.Migrations
{
    public partial class SourceFK4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MangaSource",
                keyColumn: "Id",
                keyValue: new Guid("adfb04fe-231c-41ed-8623-5ea3bb50c400"));

            migrationBuilder.AddColumn<Guid>(
                name: "SourceId",
                table: "MangaInfo",
                nullable: true);

            migrationBuilder.InsertData(
                table: "MangaSource",
                columns: new[] { "Id", "CreatedOn", "DomainUrl", "Name" },
                values: new object[] { new Guid("41bd3be4-9e5d-4b9e-9709-11458285afb8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FanFox" });

            migrationBuilder.CreateIndex(
                name: "IX_MangaInfo_SourceId",
                table: "MangaInfo",
                column: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_MangaInfo_MangaSource_SourceId",
                table: "MangaInfo",
                column: "SourceId",
                principalTable: "MangaSource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MangaInfo_MangaSource_SourceId",
                table: "MangaInfo");

            migrationBuilder.DropIndex(
                name: "IX_MangaInfo_SourceId",
                table: "MangaInfo");

            migrationBuilder.DeleteData(
                table: "MangaSource",
                keyColumn: "Id",
                keyValue: new Guid("41bd3be4-9e5d-4b9e-9709-11458285afb8"));

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "MangaInfo");

            migrationBuilder.InsertData(
                table: "MangaSource",
                columns: new[] { "Id", "CreatedOn", "DomainUrl", "Name" },
                values: new object[] { new Guid("adfb04fe-231c-41ed-8623-5ea3bb50c400"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FanFox" });
        }
    }
}
