using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmoteka.API.Migrations
{
    /// <inheritdoc />
    public partial class DatumePrikazivanja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "KrajPrikazivanja",
                table: "Filmovi",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PocetakPrikazivanja",
                table: "Filmovi",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KrajPrikazivanja",
                table: "Filmovi");

            migrationBuilder.DropColumn(
                name: "PocetakPrikazivanja",
                table: "Filmovi");
        }
    }
}
