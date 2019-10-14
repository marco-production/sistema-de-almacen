using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace proyectofinal.Migrations
{
    public partial class _3Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Empresas_EmpresaId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_EmpresaId",
                table: "Productos");

            migrationBuilder.AddColumn<string>(
                name: "RegisterId",
                table: "Productos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegisterViewModelId",
                table: "Productos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_RegisterViewModelId",
                table: "Productos",
                column: "RegisterViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_RegisterViewModel_RegisterViewModelId",
                table: "Productos",
                column: "RegisterViewModelId",
                principalTable: "RegisterViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_RegisterViewModel_RegisterViewModelId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_RegisterViewModelId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "RegisterId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "RegisterViewModelId",
                table: "Productos");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_EmpresaId",
                table: "Productos",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Empresas_EmpresaId",
                table: "Productos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
