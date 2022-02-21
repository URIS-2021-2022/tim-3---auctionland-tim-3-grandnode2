using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KatastarskaOpstina.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatutOpstines",
                columns: table => new
                {
                    StatutOpstineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SadrzajStatuta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumKreiranjaStatuta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatutOpstines", x => x.StatutOpstineID);
                });

            migrationBuilder.CreateTable(
                name: "KatastarskaOpstinas",
                columns: table => new
                {
                    KatastarskaOpstinaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatutOpstineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivOpstine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KatastarskaOpstinas", x => x.KatastarskaOpstinaID);
                    table.ForeignKey(
                        name: "FK_KatastarskaOpstinas_StatutOpstines_StatutOpstineID",
                        column: x => x.StatutOpstineID,
                        principalTable: "StatutOpstines",
                        principalColumn: "StatutOpstineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StatutOpstines",
                columns: new[] { "StatutOpstineID", "DatumKreiranjaStatuta", "SadrzajStatuta" },
                values: new object[] { new Guid("644f3de0-a9dd-4c2e-b745-89976a1b2a36"), new DateTime(2022, 2, 20, 23, 20, 1, 28, DateTimeKind.Local).AddTicks(1132), "..." });

            migrationBuilder.InsertData(
                table: "StatutOpstines",
                columns: new[] { "StatutOpstineID", "DatumKreiranjaStatuta", "SadrzajStatuta" },
                values: new object[] { new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"), new DateTime(2022, 2, 20, 23, 20, 1, 42, DateTimeKind.Local).AddTicks(8400), "..." });

            migrationBuilder.InsertData(
                table: "KatastarskaOpstinas",
                columns: new[] { "KatastarskaOpstinaID", "NazivOpstine", "StatutOpstineID" },
                values: new object[] { new Guid("1b411c13-a295-48f7-8dbd-67886c3974c0"), "Novi Grad", new Guid("644f3de0-a9dd-4c2e-b745-89976a1b2a36") });

            migrationBuilder.InsertData(
                table: "KatastarskaOpstinas",
                columns: new[] { "KatastarskaOpstinaID", "NazivOpstine", "StatutOpstineID" },
                values: new object[] { new Guid("6b411c13-a295-48f7-8dbd-67886c3974c0"), "Bikovo", new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36") });

            migrationBuilder.CreateIndex(
                name: "IX_KatastarskaOpstinas_StatutOpstineID",
                table: "KatastarskaOpstinas",
                column: "StatutOpstineID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KatastarskaOpstinas");

            migrationBuilder.DropTable(
                name: "StatutOpstines");
        }
    }
}
