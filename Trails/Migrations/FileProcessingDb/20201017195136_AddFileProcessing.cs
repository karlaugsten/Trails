using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace trails.Migrations.FileProcessingDb
{
    public partial class AddFileProcessing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppliedFileTransforms",
                columns: table => new
                {
                    fileId = table.Column<int>(nullable: false),
                    transformJobId = table.Column<int>(nullable: false),
                    appliedTransforms = table.Column<string>(nullable: true),
                    transformName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedFileTransforms", x => new { x.fileId, x.transformJobId });
                });

            migrationBuilder.CreateTable(
                name: "FileTransforms",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    startTime = table.Column<DateTime>(nullable: false),
                    endTime = table.Column<DateTime>(nullable: false),
                    fileType = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: false),
                    context = table.Column<string>(nullable: true),
                    s3Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTransforms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Transforms",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    startTime = table.Column<DateTime>(nullable: false),
                    endTime = table.Column<DateTime>(nullable: false),
                    transform = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: false),
                    context = table.Column<string>(nullable: true),
                    input = table.Column<string>(nullable: true),
                    fileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transforms", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppliedFileTransforms");

            migrationBuilder.DropTable(
                name: "FileTransforms");

            migrationBuilder.DropTable(
                name: "Transforms");
        }
    }
}
