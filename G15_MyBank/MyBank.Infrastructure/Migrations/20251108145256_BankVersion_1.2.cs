using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BankVersion_12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Transactions_Amount",
                table: "Transactions",
                sql: "[Amount] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Transactions_Amount",
                table: "Transactions");
        }
    }
}
