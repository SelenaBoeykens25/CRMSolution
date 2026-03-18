using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRM.API.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GebruikersAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wachtwoord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AanmaakDatum = table.Column<DateOnly>(type: "date", nullable: false),
                    SecurityLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GebruikersAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Landen",
                columns: table => new
                {
                    LandCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LandNaam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Landen", x => x.LandCode);
                });

            migrationBuilder.CreateTable(
                name: "Adressen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Straat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HuisNummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusNummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gemeente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adressen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adressen_Landen_LandCode",
                        column: x => x.LandCode,
                        principalTable: "Landen",
                        principalColumn: "LandCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Klanten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aanspreking = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefoonNummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAdres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeboorteDatum = table.Column<DateOnly>(type: "date", nullable: false),
                    AdresId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klanten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Klanten_Adressen_AdresId",
                        column: x => x.AdresId,
                        principalTable: "Adressen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Facturen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlantId = table.Column<int>(type: "int", nullable: false),
                    FactuurDatum = table.Column<DateOnly>(type: "date", nullable: false),
                    TeBetalenVoor = table.Column<DateOnly>(type: "date", nullable: false),
                    Prijs = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BetaalStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facturen_Klanten_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klanten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactuurLijnen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FactuurId = table.Column<int>(type: "int", nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NettoPrijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BtwPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BrutoPrijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactuurLijnen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactuurLijnen_Facturen_FactuurId",
                        column: x => x.FactuurId,
                        principalTable: "Facturen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "GebruikersAccounts",
                columns: new[] { "Id", "AanmaakDatum", "Email", "SecurityLevel", "Wachtwoord" },
                values: new object[,]
                {
                    { 1, new DateOnly(2026, 3, 18), "admin@admin.com", 1, "admin123" },
                    { 2, new DateOnly(2026, 3, 18), "owner@owner.com", 2, "owner123" },
                    { 3, new DateOnly(2026, 3, 18), "user@user.com", 0, "guest123" }
                });

            migrationBuilder.InsertData(
                table: "Landen",
                columns: new[] { "LandCode", "LandNaam" },
                values: new object[,]
                {
                    { "BE", "België" },
                    { "DE", "Duitsland" },
                    { "EN", "Engeland" },
                    { "FR", "Frankrijk" },
                    { "NL", "Nederland" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adressen_LandCode",
                table: "Adressen",
                column: "LandCode");

            migrationBuilder.CreateIndex(
                name: "IX_Facturen_KlantId",
                table: "Facturen",
                column: "KlantId");

            migrationBuilder.CreateIndex(
                name: "IX_FactuurLijnen_FactuurId",
                table: "FactuurLijnen",
                column: "FactuurId");

            migrationBuilder.CreateIndex(
                name: "IX_Klanten_AdresId",
                table: "Klanten",
                column: "AdresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactuurLijnen");

            migrationBuilder.DropTable(
                name: "GebruikersAccounts");

            migrationBuilder.DropTable(
                name: "Facturen");

            migrationBuilder.DropTable(
                name: "Klanten");

            migrationBuilder.DropTable(
                name: "Adressen");

            migrationBuilder.DropTable(
                name: "Landen");
        }
    }
}
