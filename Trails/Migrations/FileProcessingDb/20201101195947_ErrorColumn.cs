using Microsoft.EntityFrameworkCore.Migrations;

namespace trails.Migrations.FileProcessingDb
{
    public partial class ErrorColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "errorMessage",
                table: "Transforms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "errorMessage",
                table: "FileTransforms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "errorMessage",
                table: "Transforms");

            migrationBuilder.DropColumn(
                name: "errorMessage",
                table: "FileTransforms");
        }
    }
}
