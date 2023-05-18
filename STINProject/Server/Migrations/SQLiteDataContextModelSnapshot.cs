﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using STINProject.Server.Services.PersistenceService;

#nullable disable

namespace STINProject.Server.Migrations
{
    [ExcludeFromCodeCoverage]
    [DbContext(typeof(SQLiteDataContext))]
    partial class SQLiteDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("STINProject.Server.Services.PersistenceService.Models.Account", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Balance")
                        .HasColumnType("REAL");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("TEXT");

                    b.HasKey("AccountId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            AccountId = new Guid("e4c91720-b231-45a4-80a0-85a7f2034891"),
                            Balance = 100.0,
                            Currency = "CZK",
                            OwnerId = new Guid("c6bcb46f-f210-4dcc-8e3f-ac66d9ad9cc4")
                        },
                        new
                        {
                            AccountId = new Guid("fe024929-3699-40b0-ab21-c619b1f83721"),
                            Balance = 0.0,
                            Currency = "USD",
                            OwnerId = new Guid("c6bcb46f-f210-4dcc-8e3f-ac66d9ad9cc4")
                        });
                });

            modelBuilder.Entity("STINProject.Server.Services.PersistenceService.Models.Transaction", b =>
                {
                    b.Property<Guid>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("TransactionID");

                    b.HasIndex("AccountID");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            TransactionID = new Guid("f42e3870-36ef-4e57-895a-86cc572823f5"),
                            AccountID = new Guid("e4c91720-b231-45a4-80a0-85a7f2034891"),
                            Date = new DateTime(2022, 1, 24, 0, 12, 0, 0, DateTimeKind.Unspecified),
                            Value = 20.0
                        },
                        new
                        {
                            TransactionID = new Guid("10030d26-c959-425e-87fe-f54b570d44eb"),
                            AccountID = new Guid("e4c91720-b231-45a4-80a0-85a7f2034891"),
                            Date = new DateTime(2022, 1, 5, 0, 5, 0, 0, DateTimeKind.Unspecified),
                            Value = -30.0
                        },
                        new
                        {
                            TransactionID = new Guid("18f9a75b-d766-4f4a-9347-6bafc0e5d4e6"),
                            AccountID = new Guid("fe024929-3699-40b0-ab21-c619b1f83721"),
                            Date = new DateTime(2022, 1, 10, 0, 8, 0, 0, DateTimeKind.Unspecified),
                            Value = -100.0
                        });
                });

            modelBuilder.Entity("STINProject.Server.Services.PersistenceService.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("c6bcb46f-f210-4dcc-8e3f-ac66d9ad9cc4"),
                            Email = "sampleuser@gmail.com",
                            Password = "password",
                            Username = "sampleuser"
                        });
                });

            modelBuilder.Entity("STINProject.Server.Services.PersistenceService.Models.Account", b =>
                {
                    b.HasOne("STINProject.Server.Services.PersistenceService.Models.User", "Owner")
                        .WithMany("Accounts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("STINProject.Server.Services.PersistenceService.Models.Transaction", b =>
                {
                    b.HasOne("STINProject.Server.Services.PersistenceService.Models.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("STINProject.Server.Services.PersistenceService.Models.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("STINProject.Server.Services.PersistenceService.Models.User", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
