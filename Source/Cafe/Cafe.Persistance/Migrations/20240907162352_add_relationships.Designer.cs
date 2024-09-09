﻿// <auto-generated />
using System;
using Cafe.Persistance.EFCustomizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cafe.Persistance.Migrations
{
    [DbContext(typeof(CafeDbContext))]
    [Migration("20240907162352_add_relationships")]
    partial class add_relationships
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Cafe.Domain.Entities.Cafe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Cafes");
                });

            modelBuilder.Entity("Cafe.Domain.Entities.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<Guid?>("CafeId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CafeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Cafe.Domain.Entities.Employee", b =>
                {
                    b.HasOne("Cafe.Domain.Entities.Cafe", "Cafe")
                        .WithMany("Employees")
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.OwnsOne("Cafe.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<string>("EmployeeId")
                                .HasColumnType("varchar(255)");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)")
                                .HasColumnName("Email");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employees");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.OwnsOne("Cafe.Domain.ValueObjects.Gender", "Gender", b1 =>
                        {
                            b1.Property<string>("EmployeeId")
                                .HasColumnType("varchar(255)");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)")
                                .HasColumnName("Gender");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employees");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.OwnsOne("Cafe.Domain.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<string>("EmployeeId")
                                .HasColumnType("varchar(255)");

                            b1.Property<long>("Value")
                                .HasMaxLength(8)
                                .HasColumnType("bigint")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employees");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.Navigation("Cafe");

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Gender")
                        .IsRequired();

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("Cafe.Domain.Entities.Cafe", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
