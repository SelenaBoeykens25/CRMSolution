using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.API.Migrations
{
    /// <inheritdoc />
    public partial class userwachtwoord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GebruikersAccounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Wachtwoord",
                value: "$2a$11$hO0heENjo4YZyHoPafKnzOP4sCXhAqDKmF4WBUtCSXizMM.UW96/m");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GebruikersAccounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Wachtwoord",
                value: "$2a$11$XJbsMCPJ4CAJMT0KD.0yLOlTGnhAn97IP.BLATYzBBdvV7W9LdhU2");
        }
    }
}
