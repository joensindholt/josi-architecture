﻿// <auto-generated />
using System;
using JosiArchitecture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JosiArchitecture.Data.Migrations
{
    [DbContext(typeof(DataStore))]
    partial class DataStoreModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("josi")
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("JosiArchitecture.Core.Users.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Profile", "josi");
                });

            modelBuilder.Entity("JosiArchitecture.Core.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", "josi");
                });

            modelBuilder.Entity("JosiArchitecture.Core.Users.Profile", b =>
                {
                    b.HasOne("JosiArchitecture.Core.Users.User", null)
                        .WithMany("Profiles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("JosiArchitecture.Core.Users.User", b =>
                {
                    b.Navigation("Profiles");
                });
#pragma warning restore 612, 618
        }
    }
}
