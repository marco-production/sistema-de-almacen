using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace proyectofinal.Migrations
{
    public partial class _4Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Precion",
                table: "Productos",
                newName: "Precio");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Empresas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Empresas");

            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "Productos",
                newName: "Precion");
        }
    }
}
