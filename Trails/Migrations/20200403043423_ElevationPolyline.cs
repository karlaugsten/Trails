using Microsoft.EntityFrameworkCore.Migrations;

namespace trails.Migrations
{
    public partial class ElevationPolyline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ElevationPolyline",
                table: "Maps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElevationPolyline",
                table: "Maps");
        }
    }
}
