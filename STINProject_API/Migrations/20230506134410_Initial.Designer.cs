﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using STINProject_API.Services.PersistenceService;

#nullable disable

namespace STINProject_API.Migrations
{
    [DbContext(typeof(SQLitePersistenceService))]
    [Migration("20230506134410_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");
#pragma warning restore 612, 618
        }
    }
}
