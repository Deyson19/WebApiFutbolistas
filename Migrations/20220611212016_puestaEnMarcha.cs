using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiFutbolistas.Migrations
{
    public partial class puestaEnMarcha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Continentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continentes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posiciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posiciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jugadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dorsal = table.Column<int>(type: "int", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    PosicionId = table.Column<int>(type: "int", nullable: false),
                    ContinenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jugadores_Continentes_ContinenteId",
                        column: x => x.ContinenteId,
                        principalTable: "Continentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jugadores_Posiciones_PosicionId",
                        column: x => x.PosicionId,
                        principalTable: "Posiciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jugadores_ContinenteId",
                table: "Jugadores",
                column: "ContinenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Jugadores_PosicionId",
                table: "Jugadores",
                column: "PosicionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jugadores");

            migrationBuilder.DropTable(
                name: "Continentes");

            migrationBuilder.DropTable(
                name: "Posiciones");
        }
    }
}
