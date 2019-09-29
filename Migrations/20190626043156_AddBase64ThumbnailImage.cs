using Microsoft.EntityFrameworkCore.Migrations;

namespace Trails.Migrations
{
    public partial class AddBase64ThumbnailImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrailEdits",
                columns: table => new
                {
                    EditId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    MaxDuration = table.Column<double>(nullable: false),
                    MinDuration = table.Column<double>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    Elevation = table.Column<int>(nullable: false),
                    MinSeason = table.Column<string>(nullable: true),
                    MaxSeason = table.Column<string>(nullable: true),
                    TrailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrailEdits", x => x.EditId);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EditId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    ThumbnailUrl = table.Column<string>(nullable: true),
                    Base64Preview = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_TrailEdits_EditId",
                        column: x => x.EditId,
                        principalTable: "TrailEdits",
                        principalColumn: "EditId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trails",
                columns: table => new
                {
                    TrailId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    MaxDuration = table.Column<double>(nullable: false),
                    MinDuration = table.Column<double>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    Elevation = table.Column<int>(nullable: false),
                    MinSeason = table.Column<string>(nullable: true),
                    MaxSeason = table.Column<string>(nullable: true),
                    EditId = table.Column<int>(nullable: true),
                    Approved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trails", x => x.TrailId);
                    table.ForeignKey(
                        name: "FK_Trails_TrailEdits_EditId",
                        column: x => x.EditId,
                        principalTable: "TrailEdits",
                        principalColumn: "EditId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_EditId",
                table: "Images",
                column: "EditId");

            migrationBuilder.CreateIndex(
                name: "IX_Trails_EditId",
                table: "Trails",
                column: "EditId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Trails");

            migrationBuilder.DropTable(
                name: "TrailEdits");
        }
    }
}
