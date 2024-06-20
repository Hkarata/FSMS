using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSMS.Migrations
{
    /// <inheritdoc />
    public partial class FuelMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("0df0fb60-21a5-4d43-b5d9-967fc87c5ea7"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("5866cb5e-3ab0-49c0-8b92-518e331c9459"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("5c4b5e38-af4d-4d5a-8297-bae5b14c8da9"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("9356f685-bda0-473a-81d0-842b5ab37758"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("a2eae900-5c39-4027-884a-d9b48c2155f6"));

            migrationBuilder.AddColumn<int>(
                name: "Fuel",
                table: "Tanks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Fuel",
                table: "Tanks");

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("0df0fb60-21a5-4d43-b5d9-967fc87c5ea7"), false, "HR" },
                    { new Guid("5866cb5e-3ab0-49c0-8b92-518e331c9459"), false, "Procurement & Maintenance" },
                    { new Guid("5c4b5e38-af4d-4d5a-8297-bae5b14c8da9"), false, "Marketing" },
                    { new Guid("9356f685-bda0-473a-81d0-842b5ab37758"), false, "Sales" },
                    { new Guid("a2eae900-5c39-4027-884a-d9b48c2155f6"), false, "Accounts" }
                });
        }
    }
}
