using Microsoft.EntityFrameworkCore.Migrations;

namespace trails.Migrations
{
    public partial class FileReferenceInImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "fileId",
                table: "Images",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fileId",
                table: "Images");
        }
    }
}
