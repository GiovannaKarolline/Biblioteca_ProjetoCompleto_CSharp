using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCopiaSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Copias",
                columns: new[] { "Id", "Deletado", "ObraId", "StatusDisponibilidade" },
                values: new object[] { new Guid("57236b08-35cd-41c9-8ae4-2cf187c8b610"), false, new Guid("4ff24196-23a0-4a3e-bf02-19c696de6d72"), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Copias",
                keyColumn: "Id",
                keyValue: new Guid("57236b08-35cd-41c9-8ae4-2cf187c8b610"));
        }
    }
}
