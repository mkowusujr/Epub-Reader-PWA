using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalApi.Migrations
{
    public partial class UpdatedEBookMetaDataProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "EBookMetaData",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CoverImageFilePath",
                table: "EBookMetaData",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "EBookMetaData",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "EBookMetaData");

            migrationBuilder.DropColumn(
                name: "CoverImageFilePath",
                table: "EBookMetaData");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "EBookMetaData");
        }
    }
}
