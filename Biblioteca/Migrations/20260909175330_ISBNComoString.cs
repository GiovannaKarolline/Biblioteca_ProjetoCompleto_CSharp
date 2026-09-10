using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class ISBNComoString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "ObrasLiterarias",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Deletado", "Titulo" },
                values: new object[] { new Guid("8917733f-fe33-4f30-a0da-b96f13604a11"), false, "Terror" });

            migrationBuilder.InsertData(
                table: "Editoras",
                columns: new[] { "Id", "Deletado", "Nome" },
                values: new object[] { new Guid("b366f588-1a07-4c76-9637-48c9a718a2b4"), false, "Saraiva" });

            migrationBuilder.InsertData(
                table: "ObrasLiterarias",
                columns: new[] { "Id", "AnoPublicacao", "CategoriaId", "Deletado", "EditoraId", "FotoCapa", "ISBN", "Titulo" },
                values: new object[] { new Guid("4ff24196-23a0-4a3e-bf02-19c696de6d72"), 2015, new Guid("8917733f-fe33-4f30-a0da-b96f13604a11"), false, new Guid("b366f588-1a07-4c76-9637-48c9a718a2b4"), "https://static.skeelo.com/remote/320/480/100/https://skoob.s3.amazonaws.com/livros/122469134/APRENDA_INGLES_SOZINHO_COM_CON_1719414152122469134SK-V11719414153B.jpg", "B09KNNYS6M", "Aprenda Inglês Sozinho Com Contos de Terror" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ObrasLiterarias",
                keyColumn: "Id",
                keyValue: new Guid("4ff24196-23a0-4a3e-bf02-19c696de6d72"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("8917733f-fe33-4f30-a0da-b96f13604a11"));

            migrationBuilder.DeleteData(
                table: "Editoras",
                keyColumn: "Id",
                keyValue: new Guid("b366f588-1a07-4c76-9637-48c9a718a2b4"));

            migrationBuilder.AlterColumn<int>(
                name: "ISBN",
                table: "ObrasLiterarias",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
