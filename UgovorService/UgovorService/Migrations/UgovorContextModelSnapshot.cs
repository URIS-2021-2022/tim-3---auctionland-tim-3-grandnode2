﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UgovorService.Entities;

namespace UgovorService.Migrations
{
    [DbContext(typeof(UgovorContext))]
    partial class UgovorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UgovorService.Entities.Ugovor", b =>
                {
                    b.Property<Guid>("UgovorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DatumPotpisa")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumZavodjenja")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LicitacijaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MestoPotpisivanja")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RokDospeca")
                        .HasColumnType("int");

                    b.Property<DateTime>("RokZaVracanjeZemljista")
                        .HasColumnType("datetime2");

                    b.Property<int>("TipGarancije")
                        .HasColumnType("int");

                    b.Property<string>("ZavodniBroj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UgovorId");

                    b.ToTable("Ugovori");

                    b.HasData(
                        new
                        {
                            UgovorId = new Guid("9ea5d63f-f2b0-43ec-afb4-598f70958cf1"),
                            DatumPotpisa = new DateTime(2022, 2, 20, 21, 8, 46, 196, DateTimeKind.Local).AddTicks(7031),
                            DatumZavodjenja = new DateTime(2022, 2, 20, 21, 8, 46, 187, DateTimeKind.Local).AddTicks(6322),
                            LiceId = new Guid("919b3994-6c05-44b9-8f6b-47e0378491d1"),
                            LicitacijaId = new Guid("0e644068-2ec7-4f5c-be94-9539ed6e9a13"),
                            MestoPotpisivanja = "Subotica",
                            RokDospeca = 30,
                            RokZaVracanjeZemljista = new DateTime(2021, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TipGarancije = 0,
                            ZavodniBroj = "1233"
                        },
                        new
                        {
                            UgovorId = new Guid("950713d6-f551-4b46-af25-5f8ec8f3e0aa"),
                            DatumPotpisa = new DateTime(2022, 2, 20, 21, 8, 46, 197, DateTimeKind.Local).AddTicks(8440),
                            DatumZavodjenja = new DateTime(2022, 2, 20, 21, 8, 46, 197, DateTimeKind.Local).AddTicks(8378),
                            LiceId = new Guid("e6aefc80-8d6b-42df-b5da-f5ec9f24600f"),
                            LicitacijaId = new Guid("b46100fa-b51c-46ac-b122-0a80bb14d5e0"),
                            MestoPotpisivanja = "Subotica",
                            RokDospeca = 30,
                            RokZaVracanjeZemljista = new DateTime(2021, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TipGarancije = 2,
                            ZavodniBroj = "4521"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}