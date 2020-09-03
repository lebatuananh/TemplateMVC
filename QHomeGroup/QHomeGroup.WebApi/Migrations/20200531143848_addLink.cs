using Microsoft.EntityFrameworkCore.Migrations;

namespace QHomeGroup.WebApi.Migrations
{
    public partial class addLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "HomeConfigs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "HomeConfigs");
        }
    }
}
