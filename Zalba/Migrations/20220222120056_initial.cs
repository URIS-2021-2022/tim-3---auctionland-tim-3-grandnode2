using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zalba.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipZalbes",
                columns: table => new
                {
                    TipZalbeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivTipa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpisTipa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipZalbes", x => x.TipZalbeID);
                });

            migrationBuilder.CreateTable(
                name: "Zalbas",
                columns: table => new
                {
                    ZalbaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipZalbeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PodnosilacZalbeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicitacijaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatPodnosenjaZalbe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Obrazlozenje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatResenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojResenja = table.Column<int>(type: "int", nullable: false),
                    StatusZalbe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojOdluke = table.Column<int>(type: "int", nullable: false),
                    RadnjaZalbe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zalbas", x => x.ZalbaID);
                    table.ForeignKey(
                        name: "FK_Zalbas_TipZalbes_TipZalbeID",
                        column: x => x.TipZalbeID,
                        principalTable: "TipZalbes",
                        principalColumn: "TipZalbeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TipZalbes",
                columns: new[] { "TipZalbeID", "NazivTipa", "OpisTipa" },
                values: new object[] { new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"), "nnnnnn", "..." });

            migrationBuilder.InsertData(
                table: "TipZalbes",
                columns: new[] { "TipZalbeID", "NazivTipa", "OpisTipa" },
                values: new object[] { new Guid("32cd906d-8bab-457c-ade2-fbc4ba523029"), "nnnnnn", "..." });

            migrationBuilder.InsertData(
                table: "Zalbas",
                columns: new[] { "ZalbaID", "BrojOdluke", "BrojResenja", "DatPodnosenjaZalbe", "DatResenja", "LicitacijaID", "Obrazlozenje", "PodnosilacZalbeID", "RadnjaZalbe", "StatusZalbe", "TipZalbeID" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), 23, 345, new DateTime(2020, 12, 15, 9, 5, 26, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 17, 9, 9, 20, 0, DateTimeKind.Unspecified), new Guid("3f8aa5b3-a67f-45b5-b518-771a7c09a944"), "Podneta zalba je usvojena", new Guid("e03de167-e497-46e2-bcf2-9f22903ab55c"), "JN ide u drugi krug sa novim uslovima", "usvojena", new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36") });

            migrationBuilder.InsertData(
                table: "Zalbas",
                columns: new[] { "ZalbaID", "BrojOdluke", "BrojResenja", "DatPodnosenjaZalbe", "DatResenja", "LicitacijaID", "Obrazlozenje", "PodnosilacZalbeID", "RadnjaZalbe", "StatusZalbe", "TipZalbeID" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), 89, 687, new DateTime(2021, 12, 15, 9, 5, 26, 0, DateTimeKind.Unspecified), new DateTime(2021, 12, 17, 9, 9, 20, 0, DateTimeKind.Unspecified), new Guid("4e1f1f8d-a8f7-44b1-9bda-1c1ee122628d"), "Podneta zalba je usvojena", new Guid("54001bad-2161-42ac-9241-54ead772ed11"), "JN ide u drugi krug sa starim uslovima", "usvojena", new Guid("32cd906d-8bab-457c-ade2-fbc4ba523029") });

            migrationBuilder.CreateIndex(
                name: "IX_Zalbas_TipZalbeID",
                table: "Zalbas",
                column: "TipZalbeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zalbas");

            migrationBuilder.DropTable(
                name: "TipZalbes");
        }
    }
}
