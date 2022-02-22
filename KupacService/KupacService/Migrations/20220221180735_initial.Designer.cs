﻿// <auto-generated />
using System;
using KupacService.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KupacService.Migrations
{
    [DbContext(typeof(KupacContext))]
    [Migration("20220221180735_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("KupacService.Entities.FizickoLice", b =>
                {
                    b.Property<Guid>("FizickoLiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrojRacuna")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona_1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona_2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Jmbg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FizickoLiceId");

                    b.ToTable("FizickoLice");

                    b.HasData(
                        new
                        {
                            FizickoLiceId = new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                            BrojRacuna = "1234-42",
                            BrojTelefona_1 = "123450",
                            BrojTelefona_2 = "123456",
                            Email = "mail@mail.com",
                            Ime = "Ime",
                            Jmbg = "123456789",
                            Prezime = "Prezime"
                        });
                });

            modelBuilder.Entity("KupacService.Entities.KontaktOsoba", b =>
                {
                    b.Property<Guid>("KontaktOsobaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Funkcija")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KontaktOsobaId");

                    b.ToTable("KontaktOsoba");

                    b.HasData(
                        new
                        {
                            KontaktOsobaId = new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                            Funkcija = "Funkcija1",
                            Ime = "Ime",
                            Prezime = "Prezime",
                            Telefon = "1233456"
                        });
                });

            modelBuilder.Entity("KupacService.Entities.Kupac", b =>
                {
                    b.Property<Guid>("KupacId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DatumPocetkaZabrane")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DuzinaTrajanjaZabraneUGodinama")
                        .HasColumnType("int");

                    b.Property<bool>("ImaZabranu")
                        .HasColumnType("bit");

                    b.Property<int>("OstvarenaPovrsina")
                        .HasColumnType("int");

                    b.HasKey("KupacId");

                    b.HasIndex("KupacId")
                        .IsUnique();

                    b.ToTable("Kupac");

                    b.HasData(
                        new
                        {
                            KupacId = new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                            ImaZabranu = false,
                            OstvarenaPovrsina = 100
                        });
                });

            modelBuilder.Entity("KupacService.Entities.Liciter", b =>
                {
                    b.Property<Guid>("KupacId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrojLicence")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DatumPocetkaZabrane")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DuzinaTrajanjaZabraneUGodinama")
                        .HasColumnType("int");

                    b.Property<bool>("ImaZabranu")
                        .HasColumnType("bit");

                    b.Property<int>("OstvarenaPovrsina")
                        .HasColumnType("int");

                    b.HasKey("KupacId");

                    b.ToTable("Liciter");

                    b.HasData(
                        new
                        {
                            KupacId = new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                            BrojLicence = "123456",
                            ImaZabranu = false,
                            OstvarenaPovrsina = 100
                        });
                });

            modelBuilder.Entity("KupacService.Entities.PravnoLice", b =>
                {
                    b.Property<Guid>("PravnoLiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrojRacuna")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona_1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona_2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Faks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaticniBroj")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PravnoLiceId");

                    b.ToTable("PravnoLice");

                    b.HasData(
                        new
                        {
                            PravnoLiceId = new Guid("3f8aa5b3-a67f-45b5-b518-771a7c09a944"),
                            BrojRacuna = "12345-32",
                            BrojTelefona_1 = "123456",
                            BrojTelefona_2 = "1234567",
                            Email = "mail@mail.com",
                            Faks = "123456",
                            MaticniBroj = "123456"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}