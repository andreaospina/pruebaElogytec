using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pruebaElogytec.Migrations
{
    public partial class agregarPropiedad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "IdMarca",
                table: "productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_productos_IdMarca",
                table: "productos",
                column: "IdMarca");

            migrationBuilder.AddForeignKey(
                name: "FK_productos_marcas_IdMarca",
                table: "productos",
                column: "IdMarca",
                principalTable: "marcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productos_marcas_IdMarca",
                table: "productos");

            migrationBuilder.DropIndex(
                name: "IX_productos_IdMarca",
                table: "productos");

            migrationBuilder.DropColumn(
                name: "IdMarca",
                table: "productos");

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
    }
}
