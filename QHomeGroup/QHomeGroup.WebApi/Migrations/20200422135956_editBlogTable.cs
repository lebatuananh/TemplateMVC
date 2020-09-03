using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QHomeGroup.WebApi.Migrations
{
    public partial class editBlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Blogs",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "SlideImageContents",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SlideOption",
                table: "Blogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SlideVideoContents",
                table: "Blogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CreatedBy",
                table: "Blogs",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_CreatedBy",
                table: "Blogs",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_CreatedBy",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_CreatedBy",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "SlideImageContents",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "SlideOption",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "SlideVideoContents",
                table: "Blogs");
        }
    }
}
