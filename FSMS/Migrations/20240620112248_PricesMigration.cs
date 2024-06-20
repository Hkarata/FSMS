using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSMS.Migrations
{
    /// <inheritdoc />
    public partial class PricesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("0d76be87-a5ab-4042-b0a7-6c6e1fecf580"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("181dc1ac-fe9e-4af2-adb3-824bc13b5bbf"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("3b141870-5cdf-4879-9d3e-3b361bd04730"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("d0b02152-aad9-40bf-ba0f-fbc671a25f6d"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("f6542eee-1d94-494e-8026-65609184eea6"));

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Fuel = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Validity = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("34f9643c-17c1-4211-b47c-48511a2545b0"), false, "Marketing" },
                    { new Guid("3d7c11fc-09ec-486d-a553-16cfe5ed7edc"), false, "Sales" },
                    { new Guid("75464478-b879-4796-9301-421a2ba8eb84"), false, "Procurement & Maintenance" },
                    { new Guid("b6634914-ffe7-482c-a3f5-43594ad948f9"), false, "Accounts" },
                    { new Guid("e6b69f3c-b09e-4bf9-81bc-b44bd2f2986e"), false, "HR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("34f9643c-17c1-4211-b47c-48511a2545b0"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("3d7c11fc-09ec-486d-a553-16cfe5ed7edc"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("75464478-b879-4796-9301-421a2ba8eb84"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("b6634914-ffe7-482c-a3f5-43594ad948f9"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("e6b69f3c-b09e-4bf9-81bc-b44bd2f2986e"));

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("0d76be87-a5ab-4042-b0a7-6c6e1fecf580"), false, "Sales" },
                    { new Guid("181dc1ac-fe9e-4af2-adb3-824bc13b5bbf"), false, "Procurement & Maintenance" },
                    { new Guid("3b141870-5cdf-4879-9d3e-3b361bd04730"), false, "Accounts" },
                    { new Guid("d0b02152-aad9-40bf-ba0f-fbc671a25f6d"), false, "HR" },
                    { new Guid("f6542eee-1d94-494e-8026-65609184eea6"), false, "Marketing" }
                });
        }
    }
}
