using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace trails.Migrations
{
    public partial class Maps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "UserRoles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "MapId",
                table: "TrailEdits",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Polyline = table.Column<string>(nullable: true),
                    Start_Latitude = table.Column<double>(nullable: true),
                    Start_Longitude = table.Column<double>(nullable: true),
                    End_Latitude = table.Column<double>(nullable: true),
                    End_Longitude = table.Column<double>(nullable: true),
                    RawFileUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrailEdits_MapId",
                table: "TrailEdits",
                column: "MapId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrailEdits_Maps_MapId",
                table: "TrailEdits",
                column: "MapId",
                principalTable: "Maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrailEdits_Maps_MapId",
                table: "TrailEdits");

            migrationBuilder.DropTable(
                name: "Maps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_TrailEdits_MapId",
                table: "TrailEdits");

            migrationBuilder.DropColumn(
                name: "MapId",
                table: "TrailEdits");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                column: "RoleId");
        }
    }
}
