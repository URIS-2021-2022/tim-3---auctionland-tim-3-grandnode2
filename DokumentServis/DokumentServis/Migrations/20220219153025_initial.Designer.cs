﻿// <auto-generated />
using System;
using DokumentServis.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DokumentServis.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220219153025_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DokumentServis.Database.Entities.Dokument", b =>
                {
                    b.Property<int>("DokumentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumDonosenja")
                        .HasColumnType("datetime2");

                    b.Property<int>("KorisnikID")
                        .HasColumnType("int");

                    b.Property<int>("KupacID")
                        .HasColumnType("int");

                    b.Property<int>("LiciterID")
                        .HasColumnType("int");

                    b.Property<string>("Sablon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VerzijaDokumentaID")
                        .HasColumnType("int");

                    b.Property<string>("ZavodniBroj")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DokumentID");

                    b.HasIndex("VerzijaDokumentaID");

                    b.ToTable("Dokument");
                });

            modelBuilder.Entity("DokumentServis.Database.Entities.VerzijaDokumenta", b =>
                {
                    b.Property<int>("VerzijaDokumentaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Revizija")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Verzija")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VerzijaDokumentaID");

                    b.ToTable("VerzijaDokumenta");
                });

            modelBuilder.Entity("DokumentServis.Database.Entities.Dokument", b =>
                {
                    b.HasOne("DokumentServis.Database.Entities.VerzijaDokumenta", "VerzijaDokumenta")
                        .WithMany()
                        .HasForeignKey("VerzijaDokumentaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VerzijaDokumenta");
                });
#pragma warning restore 612, 618
        }
    }
}
