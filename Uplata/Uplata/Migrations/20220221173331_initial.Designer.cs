﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Uplata.Entities;

namespace Uplata.Migrations
{
    [DbContext(typeof(UplataContext))]
    [Migration("20220221173331_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Uplata.Entities.BankaEntity", b =>
                {
                    b.Property<Guid>("BankaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("NazivBanke")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("BankaId");

                    b.ToTable("Banke");

                    b.HasData(
                        new
                        {
                            BankaId = new Guid("9aef1da1-d5af-4073-9d40-8794f9d33564"),
                            Adresa = "Bulevar Oslobodjenja 80",
                            Grad = "Novi Sad",
                            NazivBanke = "OTP banka"
                        },
                        new
                        {
                            BankaId = new Guid("ceed4ee2-ea12-499b-a0c9-be41d4ac0748"),
                            Adresa = "Resavska 28",
                            Grad = "Beograd",
                            NazivBanke = "UniCredit banka"
                        });
                });

            modelBuilder.Entity("Uplata.Entities.UplataEntity", b =>
                {
                    b.Property<Guid>("UplataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BankaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BrojRacuna")
                        .HasColumnType("int");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("Iznos")
                        .HasColumnType("int");

                    b.Property<int>("PozivNaBroj")
                        .HasColumnType("int");

                    b.Property<Guid>("PrijavaZaNadmetanjeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SvrhaUplate")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UplataId");

                    b.HasIndex("BankaId");

                    b.ToTable("Uplate");

                    b.HasData(
                        new
                        {
                            UplataId = new Guid("de24dc84-1744-41cd-b4d7-56b830dde7f9"),
                            BankaId = new Guid("9aef1da1-d5af-4073-9d40-8794f9d33564"),
                            BrojRacuna = 43604112,
                            Datum = new DateTime(2022, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Iznos = 1500,
                            PozivNaBroj = 43100222,
                            PrijavaZaNadmetanjeId = new Guid("1cd5c783-4bf5-4bbc-b7f0-bd66e2ba0bd7"),
                            SvrhaUplate = "Uplata za javno nadmetanje u 2022. godini"
                        },
                        new
                        {
                            UplataId = new Guid("4f3e6672-2456-4fa6-8bf1-a7974a097136"),
                            BankaId = new Guid("ceed4ee2-ea12-499b-a0c9-be41d4ac0748"),
                            BrojRacuna = 54715223,
                            Datum = new DateTime(2021, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Iznos = 1000,
                            PozivNaBroj = 54090221,
                            PrijavaZaNadmetanjeId = new Guid("07c0c62b-675e-4714-816c-b492720194d6"),
                            SvrhaUplate = "Uplata za javno nadmetanje u 2021. godini"
                        });
                });

            modelBuilder.Entity("Uplata.Entities.UplataEntity", b =>
                {
                    b.HasOne("Uplata.Entities.BankaEntity", "Banka")
                        .WithMany()
                        .HasForeignKey("BankaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Banka");
                });
#pragma warning restore 612, 618
        }
    }
}