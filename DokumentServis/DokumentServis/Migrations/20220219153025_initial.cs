using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DokumentServis.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VerzijaDokumenta",
                columns: table => new
                {
                    VerzijaDokumentaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Verzija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revizija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerzijaDokumenta", x => x.VerzijaDokumentaID);
                });

            migrationBuilder.CreateTable(
                name: "Dokument",
                columns: table => new
                {
                    DokumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZavodniBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumDonosenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sablon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KorisnikID = table.Column<int>(type: "int", nullable: false),
                    KupacID = table.Column<int>(type: "int", nullable: false),
                    LiciterID = table.Column<int>(type: "int", nullable: false),
                    VerzijaDokumentaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokument", x => x.DokumentID);
                    table.ForeignKey(
                        name: "FK_Dokument_VerzijaDokumenta_VerzijaDokumentaID",
                        column: x => x.VerzijaDokumentaID,
                        principalTable: "VerzijaDokumenta",
                        principalColumn: "VerzijaDokumentaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dokument_VerzijaDokumentaID",
                table: "Dokument",
                column: "VerzijaDokumentaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dokument");

            migrationBuilder.DropTable(
                name: "VerzijaDokumenta");
        }
    }
}
