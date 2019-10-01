using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesuarusAPI.Migrations
{
    public partial class Iteration_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SynonymGroups",
                columns: table => new
                {
                    SynonymGroupID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SynonymGroups", x => x.SynonymGroupID);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    WordID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SynonymGroupID = table.Column<int>(nullable: true),
                    Characters = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.WordID);
                    table.ForeignKey(
                        name: "FK_Words_SynonymGroups_SynonymGroupID",
                        column: x => x.SynonymGroupID,
                        principalTable: "SynonymGroups",
                        principalColumn: "SynonymGroupID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Words_SynonymGroupID",
                table: "Words",
                column: "SynonymGroupID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "SynonymGroups");
        }
    }
}
