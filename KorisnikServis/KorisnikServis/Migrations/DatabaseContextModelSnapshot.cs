﻿// <auto-generated />
using System;
using KorisnikServis.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KorisnikServis.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KorisnikServis.Database.Entities.Korisnik", b =>
                {
                    b.Property<Guid>("KorisnikID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImeKorisnika")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KorisnickoIme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrezimeKorisnika")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TipKorisnikaID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KorisnikID");

                    b.ToTable("Korisnik");
                });

            modelBuilder.Entity("KorisnikServis.Database.Entities.TipKorisnika", b =>
                {
                    b.Property<Guid>("TipKorisnikaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NazivTipa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipKorisnikaID");

                    b.ToTable("TipKorisnika");
                });
#pragma warning restore 612, 618
        }
    }
}
