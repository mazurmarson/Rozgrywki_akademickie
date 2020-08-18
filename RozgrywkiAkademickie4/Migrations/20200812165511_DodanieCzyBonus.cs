using Microsoft.EntityFrameworkCore.Migrations;

namespace RozgrywkiAkademickie4.Migrations
{
    public partial class DodanieCzyBonus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CzyBonus",
                table: "Kierunki",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzyBonus",
                table: "Kierunki");
        }
    }
}
