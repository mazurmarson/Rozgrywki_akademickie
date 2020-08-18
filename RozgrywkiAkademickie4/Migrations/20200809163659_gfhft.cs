using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RozgrywkiAkademickie4.Migrations
{
    public partial class gfhft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kierunki",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(nullable: true),
                    Rok = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kierunki", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sporty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sporty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zawody",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataZawodow = table.Column<DateTime>(nullable: false),
                    SportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zawody", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zawody_Sporty_SportId",
                        column: x => x.SportId,
                        principalTable: "Sporty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wyniki",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KierunekId = table.Column<int>(nullable: true),
                    ZawodyId = table.Column<int>(nullable: true),
                    Miejsce = table.Column<int>(nullable: false),
                    Punkty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wyniki", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wyniki_Kierunki_KierunekId",
                        column: x => x.KierunekId,
                        principalTable: "Kierunki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wyniki_Zawody_ZawodyId",
                        column: x => x.ZawodyId,
                        principalTable: "Zawody",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wyniki_KierunekId",
                table: "Wyniki",
                column: "KierunekId");

            migrationBuilder.CreateIndex(
                name: "IX_Wyniki_ZawodyId",
                table: "Wyniki",
                column: "ZawodyId");

            migrationBuilder.CreateIndex(
                name: "IX_Zawody_SportId",
                table: "Zawody",
                column: "SportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wyniki");

            migrationBuilder.DropTable(
                name: "Kierunki");

            migrationBuilder.DropTable(
                name: "Zawody");

            migrationBuilder.DropTable(
                name: "Sporty");
        }
    }
}
