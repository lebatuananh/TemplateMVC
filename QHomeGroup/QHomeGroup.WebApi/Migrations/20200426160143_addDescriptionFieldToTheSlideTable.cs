﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace QHomeGroup.WebApi.Migrations
{
    public partial class addDescriptionFieldToTheSlideTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Slides",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Slides");
        }
    }
}
