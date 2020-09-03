using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QHomeGroup.WebApi.Migrations
{
    public partial class addHomeConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomeConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VideoUrl = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ImageClassic = table.Column<string>(nullable: true),
                    ContentClassic = table.Column<string>(nullable: true),
                    ImageModern = table.Column<string>(nullable: true),
                    ContentModern = table.Column<string>(nullable: true),
                    ProductContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeConfigs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeConfigs");
        }
    }
}
