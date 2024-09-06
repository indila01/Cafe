using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafe.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class createtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cafes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cafes", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<long>(type: "bigint", maxLength: 9, nullable: false),
                    Gender = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CafeId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Cafes_CafeId",
                        column: x => x.CafeId,
                        principalTable: "Cafes",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CafeId",
                table: "Employees",
                column: "CafeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Cafes");
        }
    }
}
