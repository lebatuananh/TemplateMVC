using Microsoft.EntityFrameworkCore.Migrations;

namespace QHomeGroup.WebApi.Migrations
{
    public partial class addIntroduceConfigTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IntroduceConfigs",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    Image = table.Column<string>(maxLength: 255, nullable: false),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntroduceConfigs", x => x.Key);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntroduceConfigs");
        }
    }
}
