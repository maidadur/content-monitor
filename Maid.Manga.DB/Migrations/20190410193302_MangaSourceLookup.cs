using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Maid.Manga.DB.Migrations
{
    public partial class MangaSourceLookup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SourceId",
                table: "MangaInfo",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MangaSource",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaSource", x => x.Id);
                });

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

            migrationBuilder.DropTable(
                name: "MangaSource");

            migrationBuilder.DropIndex(
                name: "IX_MangaInfo_SourceId",
                table: "MangaInfo");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "MangaInfo");
        }
    }
}
