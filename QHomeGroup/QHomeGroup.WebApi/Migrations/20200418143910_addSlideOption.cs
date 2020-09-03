using Microsoft.EntityFrameworkCore.Migrations;

namespace QHomeGroup.WebApi.Migrations
{
    public partial class addSlideOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SlideOption",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SlideVideoContents",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlideOption",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SlideVideoContents",
                table: "Projects");
        }
    }
}
