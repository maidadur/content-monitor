using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Maid.Manga.DB.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MangaSource",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    DomainUrl = table.Column<string>(nullable: true),
                    TitleXpath = table.Column<string>(nullable: true),
                    ImageXpath = table.Column<string>(nullable: true),
                    ChapterXpath = table.Column<string>(nullable: true),
                    ChapterHrefXpath = table.Column<string>(nullable: true),
                    ChapterTitleXpath = table.Column<string>(nullable: true),
                    ChapterDateXpath = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaSource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MangaInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Href = table.Column<string>(nullable: true),
                    SourceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MangaInfo_MangaSource_SourceId",
                        column: x => x.SourceId,
                        principalTable: "MangaSource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MangaChapterInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    Href = table.Column<string>(nullable: true),
                    MangaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaChapterInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MangaChapterInfo_MangaInfo_MangaId",
                        column: x => x.MangaId,
                        principalTable: "MangaInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MangaChapterNotification",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                name: "IX_MangaChapterInfo_MangaId",
                table: "MangaChapterInfo",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaChapterNotification_MangaChapterInfoId",
                table: "MangaChapterNotification",
                column: "MangaChapterInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaInfo_SourceId",
                table: "MangaInfo",
                column: "SourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MangaChapterNotification");

            migrationBuilder.DropTable(
                name: "MangaChapterInfo");

            migrationBuilder.DropTable(
                name: "MangaInfo");

            migrationBuilder.DropTable(
                name: "MangaSource");
        }
    }
}
