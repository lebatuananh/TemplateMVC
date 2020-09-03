using Microsoft.EntityFrameworkCore.Migrations;

namespace QHomeGroup.WebApi.Migrations
{
    public partial class changeProjectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Projects",
                newName: "BlockContents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlockContents",
                table: "Projects",
                newName: "Content");
        }
    }
}
