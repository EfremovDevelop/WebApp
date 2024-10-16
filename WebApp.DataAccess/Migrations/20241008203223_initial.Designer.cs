﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApp.DataAccess;

#nullable disable

namespace WebApp.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241008203223_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApp.Domain.Result", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CountSignIn")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserStatisticId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserStatisticId")
                        .IsUnique();

                    b.ToTable("Results");
                });

            modelBuilder.Entity("WebApp.Domain.UserStatisticRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Percent")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserStatisticRequests");
                });

            modelBuilder.Entity("WebApp.Domain.Result", b =>
                {
                    b.HasOne("WebApp.Domain.UserStatisticRequest", "UserStatistic")
                        .WithOne("Result")
                        .HasForeignKey("WebApp.Domain.Result", "UserStatisticId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserStatistic");
                });

            modelBuilder.Entity("WebApp.Domain.UserStatisticRequest", b =>
                {
                    b.Navigation("Result")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
