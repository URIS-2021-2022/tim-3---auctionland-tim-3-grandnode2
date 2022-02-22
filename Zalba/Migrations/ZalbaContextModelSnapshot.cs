﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zalba.Entities;

namespace Zalba.Migrations
{
    [DbContext(typeof(ZalbaContext))]
    partial class ZalbaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Zalba.Entities.TipZalbeE", b =>
                {
                    b.Property<Guid>("TipZalbeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NazivTipa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OpisTipa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipZalbeID");

                    b.ToTable("TipZalbes");

                    b.HasData(
                        new
                        {
                            TipZalbeID = new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                            NazivTipa = "nnnnnn",
                            OpisTipa = "..."
                        },
                        new
                        {
                            TipZalbeID = new Guid("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                            NazivTipa = "nnnnnn",
                            OpisTipa = "..."
                        });
                });

            modelBuilder.Entity("Zalba.Entities.ZalbaE", b =>
                {
                    b.Property<Guid>("ZalbaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BrojOdluke")
                        .HasColumnType("int");

                    b.Property<int>("BrojResenja")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatPodnosenjaZalbe")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatResenja")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LicitacijaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Obrazlozenje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PodnosilacZalbeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RadnjaZalbe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusZalbe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TipZalbeID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ZalbaID");

                    b.HasIndex("TipZalbeID");

                    b.ToTable("Zalbas");

                    b.HasData(
                        new
                        {
                            ZalbaID = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            BrojOdluke = 23,
                            BrojResenja = 345,
                            DatPodnosenjaZalbe = new DateTime(2020, 12, 15, 9, 5, 26, 0, DateTimeKind.Unspecified),
                            DatResenja = new DateTime(2020, 12, 17, 9, 9, 20, 0, DateTimeKind.Unspecified),
                            LicitacijaID = new Guid("3f8aa5b3-a67f-45b5-b518-771a7c09a944"),
                            Obrazlozenje = "Podneta zalba je usvojena",
                            PodnosilacZalbeID = new Guid("e03de167-e497-46e2-bcf2-9f22903ab55c"),
                            RadnjaZalbe = "JN ide u drugi krug sa novim uslovima",
                            StatusZalbe = "usvojena",
                            TipZalbeID = new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36")
                        },
                        new
                        {
                            ZalbaID = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            BrojOdluke = 89,
                            BrojResenja = 687,
                            DatPodnosenjaZalbe = new DateTime(2021, 12, 15, 9, 5, 26, 0, DateTimeKind.Unspecified),
                            DatResenja = new DateTime(2021, 12, 17, 9, 9, 20, 0, DateTimeKind.Unspecified),
                            LicitacijaID = new Guid("4e1f1f8d-a8f7-44b1-9bda-1c1ee122628d"),
                            Obrazlozenje = "Podneta zalba je usvojena",
                            PodnosilacZalbeID = new Guid("54001bad-2161-42ac-9241-54ead772ed11"),
                            RadnjaZalbe = "JN ide u drugi krug sa starim uslovima",
                            StatusZalbe = "usvojena",
                            TipZalbeID = new Guid("32cd906d-8bab-457c-ade2-fbc4ba523029")
                        });
                });

            modelBuilder.Entity("Zalba.Entities.ZalbaE", b =>
                {
                    b.HasOne("Zalba.Entities.TipZalbeE", "TipZalbe")
                        .WithMany()
                        .HasForeignKey("TipZalbeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipZalbe");
                });
#pragma warning restore 612, 618
        }
    }
}