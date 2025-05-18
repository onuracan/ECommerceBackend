using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceBackendSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ECommerceBackendSystemDBMigration_20250517 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ORDER",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PRODUCT_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QUANTITY = table.Column<int>(type: "integer", nullable: false),
                    PAYMENT_METHOD = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    USER_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SW_ACTIVE = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ORDER");
        }
    }
}
