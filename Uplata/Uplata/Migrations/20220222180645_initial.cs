using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplata.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banke",
                columns: table => new
                {
                    BankaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivBanke = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banke", x => x.BankaId);
                });

            migrationBuilder.CreateTable(
                name: "Uplate",
                columns: table => new
                {
                    UplataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrojRacuna = table.Column<int>(type: "int", nullable: false),
                    PozivNaBroj = table.Column<int>(type: "int", nullable: false),
                    Iznos = table.Column<int>(type: "int", nullable: false),
                    SvrhaUplate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrijavaZaNadmetanjeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplate", x => x.UplataId);
                    table.ForeignKey(
                        name: "FK_Uplate_Banke_BankaId",
                        column: x => x.BankaId,
                        principalTable: "Banke",
                        principalColumn: "BankaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Banke",
                columns: new[] { "BankaId", "Adresa", "Grad", "NazivBanke" },
                values: new object[] { new Guid("9aef1da1-d5af-4073-9d40-8794f9d33564"), "Bulevar Oslobodjenja 80", "Novi Sad", "OTP banka" });

            migrationBuilder.InsertData(
                table: "Banke",
                columns: new[] { "BankaId", "Adresa", "Grad", "NazivBanke" },
                values: new object[] { new Guid("ceed4ee2-ea12-499b-a0c9-be41d4ac0748"), "Resavska 28", "Beograd", "UniCredit banka" });

            migrationBuilder.InsertData(
                table: "Uplate",
                columns: new[] { "UplataId", "BankaId", "BrojRacuna", "Datum", "Iznos", "KupacId", "PozivNaBroj", "PrijavaZaNadmetanjeId", "SvrhaUplate" },
                values: new object[] { new Guid("de24dc84-1744-41cd-b4d7-56b830dde7f9"), new Guid("9aef1da1-d5af-4073-9d40-8794f9d33564"), 43604112, new DateTime(2022, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500, new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"), 43100222, new Guid("1cd5c783-4bf5-4bbc-b7f0-bd66e2ba0bd7"), "Uplata za javno nadmetanje u 2022. godini" });

            migrationBuilder.InsertData(
                table: "Uplate",
                columns: new[] { "UplataId", "BankaId", "BrojRacuna", "Datum", "Iznos", "KupacId", "PozivNaBroj", "PrijavaZaNadmetanjeId", "SvrhaUplate" },
                values: new object[] { new Guid("4f3e6672-2456-4fa6-8bf1-a7974a097136"), new Guid("ceed4ee2-ea12-499b-a0c9-be41d4ac0748"), 54715223, new DateTime(2021, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000, new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"), 54090221, new Guid("07c0c62b-675e-4714-816c-b492720194d6"), "Uplata za javno nadmetanje u 2021. godini" });

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_BankaId",
                table: "Uplate",
                column: "BankaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uplate");

            migrationBuilder.DropTable(
                name: "Banke");
        }
    }
}
