using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class NomeNormalizadoRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3f027812-847d-4b0b-81dd-68bcbf70dd8a"),
                column: "NormalizedName",
                value: "VISITANTE");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a75fe880-f314-4a02-8109-21931ba9e0b7"),
                column: "NormalizedName",
                value: "ADMINISTRADOR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3f027812-847d-4b0b-81dd-68bcbf70dd8a"),
                column: "NormalizedName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a75fe880-f314-4a02-8109-21931ba9e0b7"),
                column: "NormalizedName",
                value: null);
        }
    }
}
