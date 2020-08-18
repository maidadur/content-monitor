using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Maid.Manga.DB.Migrations
{
    public partial class MangaNotificationsV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MangaChapterNotification",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    MangaChapterInfoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaChapterNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MangaChapterNotification_MangaChapterInfo_MangaChapterInfoId",
                        column: x => x.MangaChapterInfoId,
                        principalTable: "MangaChapterInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MangaChapterNotification_MangaChapterInfoId",
                table: "MangaChapterNotification",
                column: "MangaChapterInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MangaChapterNotification");
        }
    }
}
