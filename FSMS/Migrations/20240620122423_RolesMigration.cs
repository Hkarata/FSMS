using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSMS.Migrations
{
    /// <inheritdoc />
    public partial class RolesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "WeeklyRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeeklyRoles_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    WeeklyRoleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_WeeklyRoles_WeeklyRoleId",
                        column: x => x.WeeklyRoleId,
                        principalTable: "WeeklyRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("118f5d67-3db6-4022-8041-9a5edcfe3354"), false, "Marketing" },
                    { new Guid("75a8a5fe-e9c5-4e0c-a2c4-66aea103cea8"), false, "Procurement & Maintenance" },
                    { new Guid("bf64a5d0-1784-4b8d-9c89-31bb0ddf3dd1"), false, "HR" },
                    { new Guid("c7ddcf11-f16c-4358-82aa-2930da852d13"), false, "Accounts" },
                    { new Guid("e35b6f46-5bea-4e9b-adc2-6d0783341daf"), false, "Sales" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_WeeklyRoleId",
                table: "Roles",
                column: "WeeklyRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyRoles_EmployeeId",
                table: "WeeklyRoles",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "WeeklyRoles");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("118f5d67-3db6-4022-8041-9a5edcfe3354"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("75a8a5fe-e9c5-4e0c-a2c4-66aea103cea8"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("bf64a5d0-1784-4b8d-9c89-31bb0ddf3dd1"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("c7ddcf11-f16c-4358-82aa-2930da852d13"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("e35b6f46-5bea-4e9b-adc2-6d0783341daf"));

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
    }
}
