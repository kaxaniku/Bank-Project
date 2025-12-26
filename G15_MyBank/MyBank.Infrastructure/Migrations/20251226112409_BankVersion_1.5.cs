using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BankVersion_15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activity_CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activity_IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Activity_UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logins");
        }
    }
}
