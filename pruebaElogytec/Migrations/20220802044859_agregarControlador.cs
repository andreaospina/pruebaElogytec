using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pruebaElogytec.Migrations
{
    public partial class agregarControlador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarcasId",
                table: "productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_productos_MarcasId",
                table: "productos",
                column: "MarcasId");

            migrationBuilder.AddForeignKey(
                name: "FK_productos_marcas_MarcasId",
                table: "productos",
                column: "MarcasId",
                principalTable: "marcas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productos_marcas_MarcasId",
                table: "productos");

            migrationBuilder.DropIndex(
                name: "IX_productos_MarcasId",
                table: "productos");

            migrationBuilder.DropColumn(
                name: "MarcasId",
                table: "productos");
        }
    }
}
