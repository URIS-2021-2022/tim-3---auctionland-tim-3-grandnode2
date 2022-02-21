using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JavnoNadmetanje.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SluzbeniListovi",
                columns: table => new
                {
                    SluzbeniListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Opstina = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BrojSluzbenogLista = table.Column<int>(type: "int", nullable: false),
                    DatumIzdavanja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SluzbeniListovi", x => x.SluzbeniListId);
                });

            migrationBuilder.CreateTable(
                name: "Oglasi",
                columns: table => new
                {
                    OglasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatumObjavljivanjaOglasa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GodinaObjavljivanjaOglasa = table.Column<int>(type: "int", nullable: false),
                    SluzbeniListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oglasi", x => x.OglasId);
                    table.ForeignKey(
                        name: "FK_Oglasi_SluzbeniListovi_SluzbeniListId",
                        column: x => x.SluzbeniListId,
                        principalTable: "SluzbeniListovi",
                        principalColumn: "SluzbeniListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JavnaNadmetanja",
                columns: table => new
                {
                    JavnoNadmetanjeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VremePocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VremeKraja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PocetnaCenaPoHektaru = table.Column<int>(type: "int", nullable: false),
                    Izuzeto = table.Column<bool>(type: "bit", nullable: false),
                    Tip = table.Column<int>(type: "int", nullable: false),
                    IzlicitiranaCena = table.Column<int>(type: "int", nullable: false),
                    PeriodZakupa = table.Column<int>(type: "int", nullable: false),
                    BrojUcesnika = table.Column<int>(type: "int", nullable: false),
                    VisinaDopuneDepozita = table.Column<int>(type: "int", nullable: false),
                    Krug = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OglasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicitacijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParcelaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JavnaNadmetanja", x => x.JavnoNadmetanjeId);
                    table.ForeignKey(
                        name: "FK_JavnaNadmetanja_Oglasi_OglasId",
                        column: x => x.OglasId,
                        principalTable: "Oglasi",
                        principalColumn: "OglasId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrijaveZaNadmetanje",
                columns: table => new
                {
                    PrijavaZaNadmetanjeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatumPrijave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MestoPrijave = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    JavnoNadmetanjeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrijaveZaNadmetanje", x => x.PrijavaZaNadmetanjeId);
                    table.ForeignKey(
                        name: "FK_PrijaveZaNadmetanje_JavnaNadmetanja_JavnoNadmetanjeId",
                        column: x => x.JavnoNadmetanjeId,
                        principalTable: "JavnaNadmetanja",
                        principalColumn: "JavnoNadmetanjeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DokumentiPrijave",
                columns: table => new
                {
                    PrijavaZaNadmetanjeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DokumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatumDonosenjaDokumenta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DokumentiPrijave", x => new { x.PrijavaZaNadmetanjeId, x.DokumentId });
                    table.ForeignKey(
                        name: "FK_DokumentiPrijave_PrijaveZaNadmetanje_PrijavaZaNadmetanjeId",
                        column: x => x.PrijavaZaNadmetanjeId,
                        principalTable: "PrijaveZaNadmetanje",
                        principalColumn: "PrijavaZaNadmetanjeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SluzbeniListovi",
                columns: new[] { "SluzbeniListId", "BrojSluzbenogLista", "DatumIzdavanja", "Opstina" },
                values: new object[] { new Guid("1a0d7558-2ebc-45df-83d3-13066c36d42b"), 5, new DateTime(2021, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Novi Sad" });

            migrationBuilder.InsertData(
                table: "SluzbeniListovi",
                columns: new[] { "SluzbeniListId", "BrojSluzbenogLista", "DatumIzdavanja", "Opstina" },
                values: new object[] { new Guid("76e60dd7-0e18-4c7c-abe0-b59524eca5ff"), 8, new DateTime(2022, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Subotica" });

            migrationBuilder.InsertData(
                table: "Oglasi",
                columns: new[] { "OglasId", "DatumObjavljivanjaOglasa", "GodinaObjavljivanjaOglasa", "SluzbeniListId" },
                values: new object[] { new Guid("abd912e3-5962-463e-a04e-5fdd2b43e30f"), new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2022, new Guid("1a0d7558-2ebc-45df-83d3-13066c36d42b") });

            migrationBuilder.InsertData(
                table: "Oglasi",
                columns: new[] { "OglasId", "DatumObjavljivanjaOglasa", "GodinaObjavljivanjaOglasa", "SluzbeniListId" },
                values: new object[] { new Guid("382e1636-2705-477e-95c4-8727e819c5e9"), new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2021, new Guid("76e60dd7-0e18-4c7c-abe0-b59524eca5ff") });

            migrationBuilder.InsertData(
                table: "JavnaNadmetanja",
                columns: new[] { "JavnoNadmetanjeId", "BrojUcesnika", "Datum", "IzlicitiranaCena", "Izuzeto", "Krug", "KupacId", "LicitacijaId", "OglasId", "ParcelaId", "PeriodZakupa", "PocetnaCenaPoHektaru", "Status", "Tip", "VisinaDopuneDepozita", "VremeKraja", "VremePocetka" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), 10, new DateTime(2022, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 900, true, 1, new Guid("80b7a335-bc5f-4a72-861e-2c914e14e2b4"), new Guid("3f8aa5b3-a67f-45b5-b518-771a7c09a944"), new Guid("abd912e3-5962-463e-a04e-5fdd2b43e30f"), new Guid("afdc833f-faf6-4bc1-862c-4ad94273690d"), 5, 1500, 0, 1, 250, new DateTime(2022, 2, 21, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 21, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "JavnaNadmetanja",
                columns: new[] { "JavnoNadmetanjeId", "BrojUcesnika", "Datum", "IzlicitiranaCena", "Izuzeto", "Krug", "KupacId", "LicitacijaId", "OglasId", "ParcelaId", "PeriodZakupa", "PocetnaCenaPoHektaru", "Status", "Tip", "VisinaDopuneDepozita", "VremeKraja", "VremePocetka" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), 25, new DateTime(2022, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500, false, 1, new Guid("bc03a6fb-b322-4797-b6c4-0a899615f653"), new Guid("4e1f1f8d-a8f7-44b1-9bda-1c1ee122628d"), new Guid("382e1636-2705-477e-95c4-8727e819c5e9"), new Guid("35d3c2da-7e55-4730-a4ed-9f886e24e6f9"), 3, 2000, 0, 0, 500, new DateTime(2022, 2, 21, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 21, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "PrijaveZaNadmetanje",
                columns: new[] { "PrijavaZaNadmetanjeId", "DatumPrijave", "JavnoNadmetanjeId", "MestoPrijave" },
                values: new object[] { new Guid("1cd5c783-4bf5-4bbc-b7f0-bd66e2ba0bd7"), new DateTime(2022, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "Subotica" });

            migrationBuilder.InsertData(
                table: "PrijaveZaNadmetanje",
                columns: new[] { "PrijavaZaNadmetanjeId", "DatumPrijave", "JavnoNadmetanjeId", "MestoPrijave" },
                values: new object[] { new Guid("07c0c62b-675e-4714-816c-b492720194d6"), new DateTime(2022, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "Novi Sad" });

            migrationBuilder.InsertData(
                table: "DokumentiPrijave",
                columns: new[] { "DokumentId", "PrijavaZaNadmetanjeId", "DatumDonosenjaDokumenta" },
                values: new object[] { new Guid("a99d4b97-6984-43ef-b0a5-89d04569276e"), new Guid("1cd5c783-4bf5-4bbc-b7f0-bd66e2ba0bd7"), new DateTime(2022, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "DokumentiPrijave",
                columns: new[] { "DokumentId", "PrijavaZaNadmetanjeId", "DatumDonosenjaDokumenta" },
                values: new object[] { new Guid("b99d4b97-6984-43ef-b0a5-89d04569466e"), new Guid("07c0c62b-675e-4714-816c-b492720194d6"), new DateTime(2022, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_JavnaNadmetanja_OglasId",
                table: "JavnaNadmetanja",
                column: "OglasId");

            migrationBuilder.CreateIndex(
                name: "IX_Oglasi_SluzbeniListId",
                table: "Oglasi",
                column: "SluzbeniListId");

            migrationBuilder.CreateIndex(
                name: "IX_PrijaveZaNadmetanje_JavnoNadmetanjeId",
                table: "PrijaveZaNadmetanje",
                column: "JavnoNadmetanjeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DokumentiPrijave");

            migrationBuilder.DropTable(
                name: "PrijaveZaNadmetanje");

            migrationBuilder.DropTable(
                name: "JavnaNadmetanja");

            migrationBuilder.DropTable(
                name: "Oglasi");

            migrationBuilder.DropTable(
                name: "SluzbeniListovi");
        }
    }
}
