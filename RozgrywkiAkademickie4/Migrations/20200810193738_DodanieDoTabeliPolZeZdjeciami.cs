using Microsoft.EntityFrameworkCore.Migrations;

namespace RozgrywkiAkademickie4.Migrations
{
    public partial class DodanieDoTabeliPolZeZdjeciami : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MiniaturkaUrl",
                table: "Zawody",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZdjecieUrl",
                table: "Zawody",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiniaturkaUrl",
                table: "Zawody");

            migrationBuilder.DropColumn(
                name: "ZdjecieUrl",
                table: "Zawody");
        }
    }
}
