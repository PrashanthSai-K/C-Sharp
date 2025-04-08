﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagement_RESTAPI.AppDataContext;

#nullable disable

namespace TaskManagement_RESTAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250408084603_User")]
    partial class User
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("TaskManagement_RESTAPI.Entities.Models.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TaskItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "to completed the sample task given",
                            DueDate = new DateTime(2025, 4, 8, 14, 16, 0, 581, DateTimeKind.Local).AddTicks(9817),
                            IsCompleted = false,
                            Name = "Sample task",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "to completed the demo task given",
                            DueDate = new DateTime(2025, 4, 8, 14, 16, 0, 581, DateTimeKind.Local).AddTicks(9829),
                            IsCompleted = false,
                            Name = "Demo task",
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "to completed the test task given",
                            DueDate = new DateTime(2025, 4, 8, 14, 16, 0, 581, DateTimeKind.Local).AddTicks(9831),
                            IsCompleted = false,
                            Name = "test task",
                            UserId = 2
                        },
                        new
                        {
                            Id = 4,
                            Description = "to completed the example task given",
                            DueDate = new DateTime(2025, 4, 8, 14, 16, 0, 581, DateTimeKind.Local).AddTicks(9833),
                            IsCompleted = false,
                            Name = "example task",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("TaskManagement_RESTAPI.Entities.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Mobile")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "test1@gmail.com",
                            Mobile = "0987654321",
                            Password = "password",
                            Role = "User",
                            Username = "test1"
                        },
                        new
                        {
                            Id = 2,
                            Email = "test2@gmail.com",
                            Mobile = "0987654321",
                            Password = "password",
                            Role = "User",
                            Username = "test2"
                        },
                        new
                        {
                            Id = 3,
                            Email = "admin@gmail.com",
                            Mobile = "0987654321",
                            Password = "password",
                            Role = "Admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("TaskManagement_RESTAPI.Entities.Models.TaskItem", b =>
                {
                    b.HasOne("TaskManagement_RESTAPI.Entities.Models.User", "User")
                        .WithMany("TaskItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManagement_RESTAPI.Entities.Models.User", b =>
                {
                    b.Navigation("TaskItems");
                });
#pragma warning restore 612, 618
        }
    }
}
