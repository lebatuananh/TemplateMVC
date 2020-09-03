using Microsoft.EntityFrameworkCore.Migrations;

namespace QHomeGroup.WebApi.Migrations
{
    public partial class changeTheSlideTableStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Slides");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Slides");

            migrationBuilder.DropColumn(
                name: "GroupAlias",
                table: "Slides");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Slides");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Slides");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Slides",
                newName: "SlideVideoContents");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Slides",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<string>(
                name: "SlideImageContents",
                table: "Slides",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SlideOption",
                table: "Slides",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlideImageContents",
                table: "Slides");

            migrationBuilder.DropColumn(
                name: "SlideOption",
                table: "Slides");

            migrationBuilder.RenameColumn(
                name: "SlideVideoContents",
                table: "Slides",
                newName: "Content");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Slides",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Slides",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Slides",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupAlias",
                table: "Slides",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Slides",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Slides",
                maxLength: 250,
                nullable: true);
        }
    }
}
