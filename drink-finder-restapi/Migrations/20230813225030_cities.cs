using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace drink_finder_restapi.Migrations
{
    /// <inheritdoc />
    public partial class cities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_establishments_City_cityId",
                table: "establishments");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.RenameColumn(
                name: "cityId",
                table: "establishments",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_establishments_cityId",
                table: "establishments",
                newName: "IX_establishments_CityId");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "establishments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_establishments_cities_CityId",
                table: "establishments",
                column: "CityId",
                principalTable: "cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_establishments_cities_CityId",
                table: "establishments");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "establishments",
                newName: "cityId");

            migrationBuilder.RenameIndex(
                name: "IX_establishments_CityId",
                table: "establishments",
                newName: "IX_establishments_cityId");

            migrationBuilder.AlterColumn<int>(
                name: "cityId",
                table: "establishments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_establishments_City_cityId",
                table: "establishments",
                column: "cityId",
                principalTable: "City",
                principalColumn: "Id");
        }
    }
}
