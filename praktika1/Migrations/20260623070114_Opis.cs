using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace praktika1.Migrations
{
    /// <inheritdoc />
    public partial class Opis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Opis",
                table: "Filmovi",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Opis",
                table: "Filmovi");
        }
    }
}
