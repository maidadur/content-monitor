using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Maid.Manga.DB.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MangaSource",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DomainUrl = table.Column<string>(nullable: true)
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
                    CreatedOn = table.Column<DateTime>(nullable: true),
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
                    CreatedOn = table.Column<DateTime>(nullable: true),
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

            migrationBuilder.InsertData(
                table: "MangaSource",
                columns: new[] { "Id", "CreatedOn", "DomainUrl", "Name" },
                values: new object[] { new Guid("cb5cea99-ff7e-4272-0c11-08d6c115fe81"), new DateTime(2019, 4, 17, 22, 44, 4, 591, DateTimeKind.Local), "http://fanfox.net", "FanFox" });

            migrationBuilder.CreateIndex(
                name: "IX_MangaChapterInfo_MangaId",
                table: "MangaChapterInfo",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaInfo_SourceId",
                table: "MangaInfo",
                column: "SourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MangaChapterInfo");

            migrationBuilder.DropTable(
                name: "MangaInfo");

            migrationBuilder.DropTable(
                name: "MangaSource");
        }
    }
}
