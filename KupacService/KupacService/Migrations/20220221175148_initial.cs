using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KupacService.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FizickoLice",
                columns: table => new
                {
                    FizickoLiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Jmbg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojRacuna = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FizickoLice", x => x.FizickoLiceId);
                });

            migrationBuilder.CreateTable(
                name: "KontaktOsoba",
                columns: table => new
                {
                    KontaktOsobaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Funkcija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KontaktOsoba", x => x.KontaktOsobaId);
                });

            migrationBuilder.CreateTable(
                name: "Kupac",
                columns: table => new
                {
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OstvarenaPovrsina = table.Column<int>(type: "int", nullable: false),
                    ImaZabranu = table.Column<bool>(type: "bit", nullable: false),
                    DatumPocetkaZabrane = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DuzinaTrajanjaZabraneUGodinama = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupac", x => x.KupacId);
                });

            migrationBuilder.CreateTable(
                name: "Liciter",
                columns: table => new
                {
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OstvarenaPovrsina = table.Column<int>(type: "int", nullable: false),
                    ImaZabranu = table.Column<bool>(type: "bit", nullable: false),
                    DatumPocetkaZabrane = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DuzinaTrajanjaZabraneUGodinama = table.Column<int>(type: "int", nullable: true),
                    BrojLicence = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liciter", x => x.KupacId);
                });

            migrationBuilder.CreateTable(
                name: "PravnoLice",
                columns: table => new
                {
                    PravnoLiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaticniBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojRacuna = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PravnoLice", x => x.PravnoLiceId);
                });

            migrationBuilder.InsertData(
                table: "FizickoLice",
                columns: new[] { "FizickoLiceId", "BrojRacuna", "BrojTelefona_1", "BrojTelefona_2", "Email", "Ime", "Jmbg", "Prezime" },
                values: new object[] { new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"), "1234-42", "123450", "123456", "mail@mail.com", "Ime", "123456789", "Prezime" });

            migrationBuilder.InsertData(
                table: "KontaktOsoba",
                columns: new[] { "KontaktOsobaId", "Funkcija", "Ime", "Prezime", "Telefon" },
                values: new object[] { new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"), null, "Ime", "Prezime", "1233456" });

            migrationBuilder.InsertData(
                table: "Kupac",
                columns: new[] { "KupacId", "DatumPocetkaZabrane", "DuzinaTrajanjaZabraneUGodinama", "ImaZabranu", "OstvarenaPovrsina" },
                values: new object[] { new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"), null, null, false, 100 });

            migrationBuilder.InsertData(
                table: "Liciter",
                columns: new[] { "KupacId", "BrojLicence", "DatumPocetkaZabrane", "DuzinaTrajanjaZabraneUGodinama", "ImaZabranu", "OstvarenaPovrsina" },
                values: new object[] { new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"), "123456", null, null, false, 100 });

            migrationBuilder.InsertData(
                table: "PravnoLice",
                columns: new[] { "PravnoLiceId", "BrojRacuna", "BrojTelefona_1", "BrojTelefona_2", "Email", "Faks", "MaticniBroj" },
                values: new object[] { new Guid("3f8aa5b3-a67f-45b5-b518-771a7c09a944"), "12345-32", "123456", "1234567", "mail@mail.com", "123456", "123456" });

            migrationBuilder.CreateIndex(
                name: "IX_Kupac_KupacId",
                table: "Kupac",
                column: "KupacId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FizickoLice");

            migrationBuilder.DropTable(
                name: "KontaktOsoba");

            migrationBuilder.DropTable(
                name: "Kupac");

            migrationBuilder.DropTable(
                name: "Liciter");

            migrationBuilder.DropTable(
                name: "PravnoLice");
        }
    }
}
