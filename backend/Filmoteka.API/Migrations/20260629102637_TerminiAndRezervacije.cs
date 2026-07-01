using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmoteka.API.Migrations
{
    /// <inheritdoc />
    public partial class TerminiAndRezervacije : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Termini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    SalaId = table.Column<int>(type: "int", nullable: false),
                    PocetakProjekcije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KrajProjekcije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojDostupnihMesta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Termini_Filmovi_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Filmovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Termini_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TerminId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Termini_TerminId",
                        column: x => x.TerminId,
                        principalTable: "Termini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_TerminId",
                table: "Rezervacije",
                column: "TerminId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_UserId",
                table: "Rezervacije",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Termini_FilmId",
                table: "Termini",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Termini_SalaId",
                table: "Termini",
                column: "SalaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rezervacije");

            migrationBuilder.DropTable(
                name: "Termini");
        }
    }
}
