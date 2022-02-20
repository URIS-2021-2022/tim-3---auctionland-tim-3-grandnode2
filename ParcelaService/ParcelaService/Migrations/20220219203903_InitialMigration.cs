using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelaService.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KvalitetiZemljista",
                columns: table => new
                {
                    KvalitetZemljistaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OznakaKvaliteta = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KvalitetiZemljista", x => x.KvalitetZemljistaId);
                });

            migrationBuilder.CreateTable(
                name: "ZasticeneZone",
                columns: table => new
                {
                    ZasticenaZonaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrojZasticeneZone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZasticeneZone", x => x.ZasticenaZonaId);
                });

            migrationBuilder.CreateTable(
                name: "DozvoljeniRadovi",
                columns: table => new
                {
                    DozvoljeniRadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ZasticenaZonaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DozvoljeniRadovi", x => x.DozvoljeniRadId);
                    table.ForeignKey(
                        name: "FK_DozvoljeniRadovi_ZasticeneZone_ZasticenaZonaId",
                        column: x => x.ZasticenaZonaId,
                        principalTable: "ZasticeneZone",
                        principalColumn: "ZasticenaZonaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parcele",
                columns: table => new
                {
                    ParcelaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrojParcele = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojListaNepokretnosti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KatastarskaOpstinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kultura = table.Column<int>(type: "int", nullable: false),
                    Klasa = table.Column<int>(type: "int", nullable: false),
                    Obradivost = table.Column<int>(type: "int", nullable: false),
                    ZasticenaZonaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OblikSvojine = table.Column<int>(type: "int", nullable: false),
                    Odvodnjavanje = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    KulturaStvarnoStanje = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    KlasaStvarnoStanje = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ObradivostStvarnoStanje = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ZasticenaZonaStvarnoStanje = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OdvodnjavanjeStvarnoStanje = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcele", x => x.ParcelaId);
                    table.ForeignKey(
                        name: "FK_Parcele_ZasticeneZone_ZasticenaZonaId",
                        column: x => x.ZasticenaZonaId,
                        principalTable: "ZasticeneZone",
                        principalColumn: "ZasticenaZonaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeloviParcele",
                columns: table => new
                {
                    DeoParceleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParcelaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RedniBrojDelaParcele = table.Column<int>(type: "int", nullable: false),
                    PovrsinaDelaParcele = table.Column<int>(type: "int", nullable: false),
                    KvalitetZemljistaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeloviParcele", x => x.DeoParceleId);
                    table.ForeignKey(
                        name: "FK_DeloviParcele_KvalitetiZemljista_KvalitetZemljistaId",
                        column: x => x.KvalitetZemljistaId,
                        principalTable: "KvalitetiZemljista",
                        principalColumn: "KvalitetZemljistaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeloviParcele_Parcele_ParcelaId",
                        column: x => x.ParcelaId,
                        principalTable: "Parcele",
                        principalColumn: "ParcelaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "KvalitetiZemljista",
                columns: new[] { "KvalitetZemljistaId", "Opis", "OznakaKvaliteta" },
                values: new object[,]
                {
                    { new Guid("0943c9e9-2dc0-4d8a-92a4-4c0d7297c8f1"), "Los kvalitet", "LK" },
                    { new Guid("b767f876-7462-40d7-918e-e32472e8e07f"), "Dobar kvalitet", "DK" }
                });

            migrationBuilder.InsertData(
                table: "ZasticeneZone",
                columns: new[] { "ZasticenaZonaId", "BrojZasticeneZone" },
                values: new object[,]
                {
                    { new Guid("80a231c2-f454-4bb9-bc55-df65440ef57e"), 1 },
                    { new Guid("da357d41-7086-49dc-857c-17ee3085f46f"), 2 }
                });

            migrationBuilder.InsertData(
                table: "DozvoljeniRadovi",
                columns: new[] { "DozvoljeniRadId", "Opis", "ZasticenaZonaId" },
                values: new object[,]
                {
                    { new Guid("9dcc4f86-da91-4767-8256-20e865406e60"), "Sed mattis, risus id tincidunt commodo, dui massa fermentum libero.", new Guid("80a231c2-f454-4bb9-bc55-df65440ef57e") },
                    { new Guid("bb7617ab-eb3e-4e67-a19e-49cdd2e4e4ef"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. ", new Guid("da357d41-7086-49dc-857c-17ee3085f46f") }
                });

            migrationBuilder.InsertData(
                table: "Parcele",
                columns: new[] { "ParcelaId", "BrojListaNepokretnosti", "BrojParcele", "KatastarskaOpstinaId", "Klasa", "KlasaStvarnoStanje", "Kultura", "KulturaStvarnoStanje", "OblikSvojine", "Obradivost", "ObradivostStvarnoStanje", "Odvodnjavanje", "OdvodnjavanjeStvarnoStanje", "ZasticenaZonaId", "ZasticenaZonaStvarnoStanje" },
                values: new object[,]
                {
                    { new Guid("7e2bc8e2-a0dc-4b45-8068-8bb3a9ec9605"), "111", "111", new Guid("7257a49d-029c-4102-98e0-5c1bf62b3c7a"), 1, null, 4, null, 3, 0, null, null, null, new Guid("80a231c2-f454-4bb9-bc55-df65440ef57e"), null },
                    { new Guid("f97960ee-b9f2-4910-9faa-d5bd81998f4f"), "222", "222", new Guid("d5e9759a-9aa0-4eed-9d69-4d8d049b598c"), 1, null, 5, null, 0, 0, null, null, null, new Guid("da357d41-7086-49dc-857c-17ee3085f46f"), null }
                });

            migrationBuilder.InsertData(
                table: "DeloviParcele",
                columns: new[] { "DeoParceleId", "KvalitetZemljistaId", "ParcelaId", "PovrsinaDelaParcele", "RedniBrojDelaParcele" },
                values: new object[] { new Guid("70037ed2-cefc-498c-8a04-819d1bbd415b"), new Guid("b767f876-7462-40d7-918e-e32472e8e07f"), new Guid("7e2bc8e2-a0dc-4b45-8068-8bb3a9ec9605"), 300, 1 });

            migrationBuilder.InsertData(
                table: "DeloviParcele",
                columns: new[] { "DeoParceleId", "KvalitetZemljistaId", "ParcelaId", "PovrsinaDelaParcele", "RedniBrojDelaParcele" },
                values: new object[] { new Guid("45504801-01fa-4054-9601-1bb7216f22f6"), new Guid("0943c9e9-2dc0-4d8a-92a4-4c0d7297c8f1"), new Guid("f97960ee-b9f2-4910-9faa-d5bd81998f4f"), 300, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_DeloviParcele_KvalitetZemljistaId",
                table: "DeloviParcele",
                column: "KvalitetZemljistaId");

            migrationBuilder.CreateIndex(
                name: "IX_DeloviParcele_ParcelaId",
                table: "DeloviParcele",
                column: "ParcelaId");

            migrationBuilder.CreateIndex(
                name: "IX_DozvoljeniRadovi_ZasticenaZonaId",
                table: "DozvoljeniRadovi",
                column: "ZasticenaZonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcele_ZasticenaZonaId",
                table: "Parcele",
                column: "ZasticenaZonaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeloviParcele");

            migrationBuilder.DropTable(
                name: "DozvoljeniRadovi");

            migrationBuilder.DropTable(
                name: "KvalitetiZemljista");

            migrationBuilder.DropTable(
                name: "Parcele");

            migrationBuilder.DropTable(
                name: "ZasticeneZone");
        }
    }
}
