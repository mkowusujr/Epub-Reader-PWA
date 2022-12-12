using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalApi.Migrations
{
    public partial class AddedeEBookEpubFileToEBookEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableOfContentsAsJson",
                table: "EBooks");

            migrationBuilder.AddColumn<byte[]>(
                name: "EpubFile",
                table: "EBooks",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpubFile",
                table: "EBooks");

            migrationBuilder.AddColumn<string>(
                name: "TableOfContentsAsJson",
                table: "EBooks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
