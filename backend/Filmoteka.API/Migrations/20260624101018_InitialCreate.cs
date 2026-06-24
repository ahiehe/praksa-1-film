using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmoteka.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reziseri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reziseri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zanrovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zanrovi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filmovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false),
                    GodinaIzdanja = table.Column<int>(type: "int", nullable: false),
                    ZanrId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmovi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filmovi_Zanrovi_ZanrId",
                        column: x => x.ZanrId,
                        principalTable: "Zanrovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmReziser",
                columns: table => new
                {
                    FilmoviId = table.Column<int>(type: "int", nullable: false),
                    ReziseriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmReziser", x => new { x.FilmoviId, x.ReziseriId });
                    table.ForeignKey(
                        name: "FK_FilmReziser_Filmovi_FilmoviId",
                        column: x => x.FilmoviId,
                        principalTable: "Filmovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmReziser_Reziseri_ReziseriId",
                        column: x => x.ReziseriId,
                        principalTable: "Reziseri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filmovi_ZanrId",
                table: "Filmovi",
                column: "ZanrId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmReziser_ReziseriId",
                table: "FilmReziser",
                column: "ReziseriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmReziser");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Filmovi");

            migrationBuilder.DropTable(
                name: "Reziseri");

            migrationBuilder.DropTable(
                name: "Zanrovi");
        }
    }
}
