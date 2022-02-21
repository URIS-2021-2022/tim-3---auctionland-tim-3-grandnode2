using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UgovorService.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ugovori",
                columns: table => new
                {
                    UgovorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicitacijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipGarancije = table.Column<int>(type: "int", nullable: false),
                    LiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RokDospeca = table.Column<int>(type: "int", nullable: false),
                    ZavodniBroj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumZavodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RokZaVracanjeZemljista = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MestoPotpisivanja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumPotpisa = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ugovori", x => x.UgovorId);
                });

            migrationBuilder.InsertData(
                table: "Ugovori",
                columns: new[] { "UgovorId", "DatumPotpisa", "DatumZavodjenja", "LiceId", "LicitacijaId", "MestoPotpisivanja", "RokDospeca", "RokZaVracanjeZemljista", "TipGarancije", "ZavodniBroj" },
                values: new object[] { new Guid("9ea5d63f-f2b0-43ec-afb4-598f70958cf1"), new DateTime(2022, 2, 20, 21, 8, 46, 196, DateTimeKind.Local).AddTicks(7031), new DateTime(2022, 2, 20, 21, 8, 46, 187, DateTimeKind.Local).AddTicks(6322), new Guid("919b3994-6c05-44b9-8f6b-47e0378491d1"), new Guid("0e644068-2ec7-4f5c-be94-9539ed6e9a13"), "Subotica", 30, new DateTime(2021, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "1233" });

            migrationBuilder.InsertData(
                table: "Ugovori",
                columns: new[] { "UgovorId", "DatumPotpisa", "DatumZavodjenja", "LiceId", "LicitacijaId", "MestoPotpisivanja", "RokDospeca", "RokZaVracanjeZemljista", "TipGarancije", "ZavodniBroj" },
                values: new object[] { new Guid("950713d6-f551-4b46-af25-5f8ec8f3e0aa"), new DateTime(2022, 2, 20, 21, 8, 46, 197, DateTimeKind.Local).AddTicks(8440), new DateTime(2022, 2, 20, 21, 8, 46, 197, DateTimeKind.Local).AddTicks(8378), new Guid("e6aefc80-8d6b-42df-b5da-f5ec9f24600f"), new Guid("b46100fa-b51c-46ac-b122-0a80bb14d5e0"), "Subotica", 30, new DateTime(2021, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "4521" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ugovori");
        }
    }
}
