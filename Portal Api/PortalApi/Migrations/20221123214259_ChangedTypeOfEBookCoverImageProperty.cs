using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal_Api.Migrations
{
    public partial class ChangedTypeOfEBookCoverImageProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageFilePath",
                table: "EBookMetaData");

            migrationBuilder.AddColumn<byte[]>(
                name: "CoverImage",
                table: "EBookMetaData",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "EBookMetaData");

            migrationBuilder.AddColumn<string>(
                name: "CoverImageFilePath",
                table: "EBookMetaData",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
