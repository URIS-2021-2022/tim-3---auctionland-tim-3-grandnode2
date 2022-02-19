using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace licitacijaService.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dokumentiLicitacije",
                columns: table => new
                {
                    licitacijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    dokumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    vrstaPodnosiocaDokumenta = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    datumPodnosenjaDokumenta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dokumentiLicitacije", x => new { x.licitacijaId, x.dokumentId });
                });

            migrationBuilder.CreateTable(
                name: "licitacije",
                columns: table => new
                {
                    licitacijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    brojLicitacije = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    goidna = table.Column<int>(type: "int", nullable: false),
                    datumLicitacije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ogranicenjeLicitacije = table.Column<int>(type: "int", nullable: false),
                    korakCene = table.Column<int>(type: "int", nullable: false),
                    rokZaDostavuPrijava = table.Column<DateTime>(type: "datetime2", nullable: false),
                    oznakaKomisije = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_licitacije", x => x.licitacijaId);
                });

            migrationBuilder.InsertData(
                table: "dokumentiLicitacije",
                columns: new[] { "dokumentId", "licitacijaId", "datumPodnosenjaDokumenta", "vrstaPodnosiocaDokumenta" },
                values: new object[,]
                {
                    { new Guid("b99d4b97-6984-43ef-b0a5-89d04569466e"), new Guid("1f8aa5b3-a67f-45c5-b519-771a7c09a944"), new DateTime(2022, 2, 19, 11, 43, 29, 914, DateTimeKind.Local).AddTicks(877), "f" },
                    { new Guid("a99d4b97-6984-43ef-b0a5-89d04569276e"), new Guid("1f8aa5b3-a67f-45c5-b519-771a7c09a944"), new DateTime(2022, 2, 19, 11, 43, 29, 914, DateTimeKind.Local).AddTicks(1686), "f" },
                    { new Guid("b99d4b97-6984-43ef-b0a5-19d04569276e"), new Guid("1f8aa5b3-a67f-45c5-b519-771a7c09a944"), new DateTime(2022, 2, 19, 11, 43, 29, 914, DateTimeKind.Local).AddTicks(1705), "p" },
                    { new Guid("a99d4b97-6984-43ef-b0a5-89d04569276e"), new Guid("2d53fc22-eac4-43bb-8f55-d2b8495603cc"), new DateTime(2022, 2, 19, 11, 43, 29, 914, DateTimeKind.Local).AddTicks(1711), "f" },
                    { new Guid("c99d5b97-6984-43ef-b0a5-89d04569466e"), new Guid("4e1f1f8d-a8f7-44b1-9bda-1c1ee122628d"), new DateTime(2022, 2, 19, 11, 43, 29, 914, DateTimeKind.Local).AddTicks(1716), "p" },
                    { new Guid("f11d5b97-6984-43ef-b0a5-89d04569466e"), new Guid("4e1f1f8d-a8f7-44b1-9bda-1c1ee122628d"), new DateTime(2022, 2, 19, 11, 43, 29, 914, DateTimeKind.Local).AddTicks(1721), "f" },
                    { new Guid("e99d4b97-6984-43ef-b0a5-89d04569466e"), new Guid("3f8aa5b3-a67f-45b5-b518-771a7c09a944"), new DateTime(2022, 2, 19, 11, 43, 29, 914, DateTimeKind.Local).AddTicks(1727), "f" }
                });

            migrationBuilder.InsertData(
                table: "licitacije",
                columns: new[] { "licitacijaId", "brojLicitacije", "datumLicitacije", "goidna", "korakCene", "ogranicenjeLicitacije", "oznakaKomisije", "rokZaDostavuPrijava" },
                values: new object[,]
                {
                    { new Guid("1f8aa5b3-a67f-45c5-b519-771a7c09a944"), 1, new DateTime(2019, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2019, 1, 1, "bgt246", new DateTime(2022, 2, 19, 11, 43, 29, 911, DateTimeKind.Local).AddTicks(5164) },
                    { new Guid("2d53fc22-eac4-43bb-8f55-d2b8495603cc"), 2, new DateTime(2021, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2021, 2, 0, "bgt246", new DateTime(2022, 2, 19, 11, 43, 29, 913, DateTimeKind.Local).AddTicks(821) },
                    { new Guid("4e1f1f8d-a8f7-44b1-9bda-1c1ee122628d"), 13, new DateTime(1921, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2021, 3, 0, "kom123ef", new DateTime(2022, 2, 19, 11, 43, 29, 913, DateTimeKind.Local).AddTicks(864) },
                    { new Guid("3f8aa5b3-a67f-45b5-b518-771a7c09a944"), 1323, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2020, 1, 0, "kom123ef", new DateTime(2022, 2, 19, 11, 43, 29, 913, DateTimeKind.Local).AddTicks(876) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dokumentiLicitacije");

            migrationBuilder.DropTable(
                name: "licitacije");
        }
    }
}
