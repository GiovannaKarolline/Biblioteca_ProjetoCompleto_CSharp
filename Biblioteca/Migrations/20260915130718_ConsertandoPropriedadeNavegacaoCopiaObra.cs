using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class ConsertandoPropriedadeNavegacaoCopiaObra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copias_ObrasLiterarias_ObraId",
                table: "Copias");

            migrationBuilder.RenameColumn(
                name: "ObraId",
                table: "Copias",
                newName: "ObraLiterariaId");

            migrationBuilder.RenameIndex(
                name: "IX_Copias_ObraId",
                table: "Copias",
                newName: "IX_Copias_ObraLiterariaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Copias_ObrasLiterarias_ObraLiterariaId",
                table: "Copias",
                column: "ObraLiterariaId",
                principalTable: "ObrasLiterarias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copias_ObrasLiterarias_ObraLiterariaId",
                table: "Copias");

            migrationBuilder.RenameColumn(
                name: "ObraLiterariaId",
                table: "Copias",
                newName: "ObraId");

            migrationBuilder.RenameIndex(
                name: "IX_Copias_ObraLiterariaId",
                table: "Copias",
                newName: "IX_Copias_ObraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Copias_ObrasLiterarias_ObraId",
                table: "Copias",
                column: "ObraId",
                principalTable: "ObrasLiterarias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
