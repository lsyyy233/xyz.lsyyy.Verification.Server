﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using xyz.lsyyy.Verification.Data;

namespace xyz.lsyyy.Verification.Data.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("xyz.lsyyy.Verification.Data.ActionTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ActionName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ControllerName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TagName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("ActionTags");
                });

            modelBuilder.Entity("xyz.lsyyy.Verification.Data.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("SuperiorDepartmentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SuperiorDepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("xyz.lsyyy.Verification.Data.DepartmentActionMap", b =>
                {
                    b.Property<int>("ActionTagId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.HasIndex("ActionTagId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("DepartmentActionMaps");
                });

            modelBuilder.Entity("xyz.lsyyy.Verification.Data.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("SuperiorPositionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("SuperiorPositionId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("xyz.lsyyy.Verification.Data.PositionActionMap", b =>
                {
                    b.Property<int>("ActionTagId")
                        .HasColumnType("int");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.HasIndex("ActionTagId");

                    b.HasIndex("PositionId");

                    b.ToTable("PositionActionMaps");
                });

            modelBuilder.Entity("xyz.lsyyy.Verification.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("xyz.lsyyy.Verification.Data.UserActionMap", b =>
                {
                    b.Property<int>("AccessType")
                        .HasColumnType("int");

                    b.Property<int>("ActionTagId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasIndex("ActionTagId");

                    b.HasIndex("UserId");

                    b.ToTable("UserActionMaps");
                });

            modelBuilder.Entity("xyz.lsyyy.Verification.Data.Department", b =>
                {
                    b.HasOne("xyz.lsyyy.Verification.Data.Department", "SuperiorDepartment")
                        .WithMany()
                        .HasForeignKey("SuperiorDepartmentId");
                });

            modelBuilder.Entity("xyz.lsyyy.Verification.Data.DepartmentActionMap", b =>
                {
                    b.HasOne("xyz.lsyyy.Verification.Data.ActionTag", "ActionTag")
                        .WithMany()
                        .HasForeignKey("ActionTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("xyz.lsyyy.Verification.Data.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("xyz.lsyyy.Verification.Data.Position", b =>
                {
                    b.HasOne("xyz.lsyyy.Verification.Data.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("xyz.lsyyy.Verification.Data.Position", "SuperiorPosition")
                        .WithMany()
                        .HasForeignKey("SuperiorPositionId");
                });

            modelBuilder.Entity("xyz.lsyyy.Verification.Data.PositionActionMap", b =>
                {
                    b.HasOne("xyz.lsyyy.Verification.Data.ActionTag", "ActionTag")
                        .WithMany()
                        .HasForeignKey("ActionTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("xyz.lsyyy.Verification.Data.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("xyz.lsyyy.Verification.Data.User", b =>
                {
                    b.HasOne("xyz.lsyyy.Verification.Data.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId");
                });

            modelBuilder.Entity("xyz.lsyyy.Verification.Data.UserActionMap", b =>
                {
                    b.HasOne("xyz.lsyyy.Verification.Data.ActionTag", "ActionTag")
                        .WithMany()
                        .HasForeignKey("ActionTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("xyz.lsyyy.Verification.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
