using Microsoft.EntityFrameworkCore.Migrations;

namespace trails.Migrations.FileProcessingDb
{
    public partial class NewFileTransformIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppliedFileTransforms",
                table: "AppliedFileTransforms");

            migrationBuilder.AlterColumn<string>(
                name: "transformName",
                table: "AppliedFileTransforms",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppliedFileTransforms",
                table: "AppliedFileTransforms",
                columns: new[] { "fileId", "transformName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppliedFileTransforms",
                table: "AppliedFileTransforms");

            migrationBuilder.AlterColumn<string>(
                name: "transformName",
                table: "AppliedFileTransforms",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppliedFileTransforms",
                table: "AppliedFileTransforms",
                columns: new[] { "fileId", "transformJobId" });
        }
    }
}
