using Microsoft.EntityFrameworkCore.Migrations;

namespace QHomeGroup.WebApi.Migrations
{
    public partial class updateContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactDetails",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "ContactDetails");

            migrationBuilder.RenameTable(
                name: "ContactDetails",
                newName: "Contacts");

            migrationBuilder.RenameColumn(
                name: "Other",
                table: "Contacts",
                newName: "Content");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contacts",
                table: "Contacts",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Contacts",
                table: "Contacts");

            migrationBuilder.RenameTable(
                name: "Contacts",
                newName: "ContactDetails");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "ContactDetails",
                newName: "Other");

            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "ContactDetails",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Lng",
                table: "ContactDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "ContactDetails",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactDetails",
                table: "ContactDetails",
                column: "Id");
        }
    }
}
