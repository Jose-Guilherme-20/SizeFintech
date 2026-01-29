using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SizeFintech.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cart_EmpresaId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Invoice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_EmpresaId",
                table: "Cart",
                column: "EmpresaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cart_EmpresaId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Invoice");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Invoice",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Invoice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Invoice",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Company",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Company",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Company",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Cart",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Cart",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Cart",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_EmpresaId",
                table: "Cart",
                column: "EmpresaId");
        }
    }
}
