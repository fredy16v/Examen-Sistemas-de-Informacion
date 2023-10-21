using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Materias.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "materias",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    requisito_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_materias", x => x.id);
                    table.ForeignKey(
                        name: "FK_materias_materias_requisito_id",
                        column: x => x.requisito_id,
                        principalTable: "materias",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_materias_requisito_id",
                table: "materias",
                column: "requisito_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "materias");
        }
    }
}
