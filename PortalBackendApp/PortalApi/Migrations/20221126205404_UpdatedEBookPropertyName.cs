using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalApi.Migrations
{
    public partial class UpdatedEBookPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "EBookMetaData",
                newName: "FileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "EBookMetaData",
                newName: "FilePath");
        }
    }
}
