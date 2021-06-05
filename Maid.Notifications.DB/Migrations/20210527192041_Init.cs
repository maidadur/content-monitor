using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Maid.Notifications.DB.Migrations
{
	public partial class Init : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
				name: "Subscriptions",
				columns: table => new {
					Id = table.Column<Guid>(nullable: false),
					CreatedOn = table.Column<DateTime>(nullable: true)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					Endpoint = table.Column<string>(nullable: true),
					KeysObj = table.Column<string>(nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_Subscriptions", x => x.Id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "Subscriptions");
		}
	}
}