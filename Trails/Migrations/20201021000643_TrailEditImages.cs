using Microsoft.EntityFrameworkCore.Migrations;

namespace trails.Migrations
{
    public partial class TrailEditImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_TrailEdits_EditId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_EditId",
                table: "Images");

            migrationBuilder.CreateTable(
                name: "TrailEditImages",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false),
                    EditId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrailEditImages", x => new { x.ImageId, x.EditId });
                    table.ForeignKey(
                        name: "FK_TrailEditImages_TrailEdits_EditId",
                        column: x => x.EditId,
                        principalTable: "TrailEdits",
                        principalColumn: "EditId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrailEditImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrailEditImages_EditId",
                table: "TrailEditImages",
                column: "EditId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrailEditImages");

            migrationBuilder.CreateIndex(
                name: "IX_Images_EditId",
                table: "Images",
                column: "EditId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_TrailEdits_EditId",
                table: "Images",
                column: "EditId",
                principalTable: "TrailEdits",
                principalColumn: "EditId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
