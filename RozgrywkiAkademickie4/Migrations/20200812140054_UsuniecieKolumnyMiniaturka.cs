using Microsoft.EntityFrameworkCore.Migrations;

namespace RozgrywkiAkademickie4.Migrations
{
    public partial class UsuniecieKolumnyMiniaturka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiniaturkaUrl",
                table: "Zawody");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MiniaturkaUrl",
                table: "Zawody",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
