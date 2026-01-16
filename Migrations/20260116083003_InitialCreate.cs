using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management_Atelier_Reparatii.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mecanic",
                columns: table => new
                {
                    MecanicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specializare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mecanic", x => x.MecanicId);
                });

            migrationBuilder.CreateTable(
                name: "Masina",
                columns: table => new
                {
                    MasinaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnFabricatie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumarInmatriculare = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masina", x => x.MasinaId);
                    table.ForeignKey(
                        name: "FK_Masina_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComandaService",
                columns: table => new
                {
                    ComandaServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasinaId = table.Column<int>(type: "int", nullable: false),
                    MecanicId = table.Column<int>(type: "int", nullable: false),
                    DataPrimire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Observatii = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComandaService", x => x.ComandaServiceId);
                    table.ForeignKey(
                        name: "FK_ComandaService_Masina_MasinaId",
                        column: x => x.MasinaId,
                        principalTable: "Masina",
                        principalColumn: "MasinaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComandaService_Mecanic_MecanicId",
                        column: x => x.MecanicId,
                        principalTable: "Mecanic",
                        principalColumn: "MecanicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComandaService_MasinaId",
                table: "ComandaService",
                column: "MasinaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaService_MecanicId",
                table: "ComandaService",
                column: "MecanicId");

            migrationBuilder.CreateIndex(
                name: "IX_Masina_ClientId",
                table: "Masina",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComandaService");

            migrationBuilder.DropTable(
                name: "Masina");

            migrationBuilder.DropTable(
                name: "Mecanic");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
