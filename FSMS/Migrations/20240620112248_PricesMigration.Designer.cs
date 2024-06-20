﻿// <auto-generated />
using System;
using FSMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FSMS.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240620112248_PricesMigration")]
    partial class PricesMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FSMS.Entities.Allocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DispenserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DispenserId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Allocations");
                });

            modelBuilder.Entity("FSMS.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b6634914-ffe7-482c-a3f5-43594ad948f9"),
                            IsDeleted = false,
                            Name = "Accounts"
                        },
                        new
                        {
                            Id = new Guid("e6b69f3c-b09e-4bf9-81bc-b44bd2f2986e"),
                            IsDeleted = false,
                            Name = "HR"
                        },
                        new
                        {
                            Id = new Guid("34f9643c-17c1-4211-b47c-48511a2545b0"),
                            IsDeleted = false,
                            Name = "Marketing"
                        },
                        new
                        {
                            Id = new Guid("3d7c11fc-09ec-486d-a553-16cfe5ed7edc"),
                            IsDeleted = false,
                            Name = "Sales"
                        },
                        new
                        {
                            Id = new Guid("75464478-b879-4796-9301-421a2ba8eb84"),
                            IsDeleted = false,
                            Name = "Procurement & Maintenance"
                        });
                });

            modelBuilder.Entity("FSMS.Entities.Dispenser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Dispensers");
                });

            modelBuilder.Entity("FSMS.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("FSMS.Entities.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Fuel")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Validity")
                        .HasColumnType("integer");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("FSMS.Entities.Tank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Capacity")
                        .HasColumnType("double precision");

                    b.Property<int>("Fuel")
                        .HasColumnType("integer");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tanks");
                });

            modelBuilder.Entity("FSMS.Entities.Allocation", b =>
                {
                    b.HasOne("FSMS.Entities.Dispenser", "Dispenser")
                        .WithMany("Allocations")
                        .HasForeignKey("DispenserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FSMS.Entities.Employee", "Employee")
                        .WithMany("Allocations")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dispenser");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("FSMS.Entities.Employee", b =>
                {
                    b.HasOne("FSMS.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("FSMS.Entities.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("FSMS.Entities.Dispenser", b =>
                {
                    b.Navigation("Allocations");
                });

            modelBuilder.Entity("FSMS.Entities.Employee", b =>
                {
                    b.Navigation("Allocations");
                });
#pragma warning restore 612, 618
        }
    }
}
