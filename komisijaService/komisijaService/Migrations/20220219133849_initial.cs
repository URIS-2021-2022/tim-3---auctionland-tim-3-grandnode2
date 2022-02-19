using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace komisijaService.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Komisija",
                columns: table => new
                {
                    komisijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nazivKomisije = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    oznakaKomisije = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komisija", x => x.komisijaId);
                });

            migrationBuilder.CreateTable(
                name: "LicnostiKomisije",
                columns: table => new
                {
                    licnostKomisijeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    imeLicnostiKomisije = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    prezimeLicnostiKomisije = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    funkcijaLicnostiKomisije = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    kontaktLicnostiKomisije = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datumRodjenjaLicnostiKomisije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    komisijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    oznakaKomisije = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicnostiKomisije", x => x.licnostKomisijeId);
                    table.ForeignKey(
                        name: "FK_LicnostiKomisije_Komisija_komisijaId",
                        column: x => x.komisijaId,
                        principalTable: "Komisija",
                        principalColumn: "komisijaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Komisija",
                columns: new[] { "komisijaId", "nazivKomisije", "oznakaKomisije" },
                values: new object[] { new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"), "Prva komisija", "kom123ef" });

            migrationBuilder.InsertData(
                table: "Komisija",
                columns: new[] { "komisijaId", "nazivKomisije", "oznakaKomisije" },
                values: new object[] { new Guid("c99d5b97-6984-43ef-b0a5-89d04569466e"), "Nova komisija", "kom345ef" });

            migrationBuilder.InsertData(
                table: "LicnostiKomisije",
                columns: new[] { "licnostKomisijeId", "datumRodjenjaLicnostiKomisije", "funkcijaLicnostiKomisije", "imeLicnostiKomisije", "komisijaId", "kontaktLicnostiKomisije", "oznakaKomisije", "prezimeLicnostiKomisije" },
                values: new object[,]
                {
                    { new Guid("1f8aa5b3-a67f-45c5-b519-771a7c09a944"), new DateTime(1999, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pomocnik", "Marko", new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"), "0645371333", "kom123ef", "﻿﻿﻿Markovic" },
                    { new Guid("2d53fc22-eac4-43bb-8f55-d2b8495603cc"), new DateTime(1989, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prva postava", "Sonja", new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"), "0617825713", "kom123ef", "Stojanovic" },
                    { new Guid("4e1f1f8d-a8f7-44b1-9bda-1c1ee122628d"), new DateTime(1976, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Obican clan", "Petar", new Guid("c99d5b97-6984-43ef-b0a5-89d04569466e"), "0672514739", "kom345ef", "Petrovic" },
                    { new Guid("3f8aa5b3-a67f-45b5-b518-771a7c09a944"), new DateTime(1971, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Predsednik", "Mina", new Guid("c99d5b97-6984-43ef-b0a5-89d04569466e"), "0651516733", "kom345ef", "Zlatic" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Komisija_oznakaKomisije",
                table: "Komisija",
                column: "oznakaKomisije",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LicnostiKomisije_komisijaId",
                table: "LicnostiKomisije",
                column: "komisijaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LicnostiKomisije");

            migrationBuilder.DropTable(
                name: "Komisija");
        }
    }
}
