﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using komisijaService.DBContexts;

namespace komisijaService.Migrations
{
    [DbContext(typeof(KomisijaContext))]
    partial class KomisijaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("komisijaService.Entites.Komisija", b =>
                {
                    b.Property<Guid>("komisijaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("nazivKomisije")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("oznakaKomisije")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("komisijaId");

                    b.HasIndex("oznakaKomisije")
                        .IsUnique();

                    b.ToTable("Komisija");

                    b.HasData(
                        new
                        {
                            komisijaId = new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                            nazivKomisije = "Prva komisija",
                            oznakaKomisije = "kom123ef"
                        },
                        new
                        {
                            komisijaId = new Guid("c99d5b97-6984-43ef-b0a5-89d04569466e"),
                            nazivKomisije = "Nova komisija",
                            oznakaKomisije = "kom345ef"
                        });
                });

            modelBuilder.Entity("komisijaService.Entites.LicnostKomisije", b =>
                {
                    b.Property<Guid>("licnostKomisijeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("datumRodjenjaLicnostiKomisije")
                        .HasColumnType("datetime2");

                    b.Property<string>("funkcijaLicnostiKomisije")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("imeLicnostiKomisije")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("komisijaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("kontaktLicnostiKomisije")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("oznakaKomisije")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("prezimeLicnostiKomisije")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("licnostKomisijeId");

                    b.HasIndex("komisijaId");

                    b.ToTable("LicnostiKomisije");

                    b.HasData(
                        new
                        {
                            licnostKomisijeId = new Guid("1f8aa5b3-a67f-45c5-b519-771a7c09a944"),
                            datumRodjenjaLicnostiKomisije = new DateTime(1999, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            funkcijaLicnostiKomisije = "Pomocnik",
                            imeLicnostiKomisije = "Marko",
                            komisijaId = new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                            kontaktLicnostiKomisije = "0645371333",
                            oznakaKomisije = "kom123ef",
                            prezimeLicnostiKomisije = "﻿﻿﻿Markovic"
                        },
                        new
                        {
                            licnostKomisijeId = new Guid("2d53fc22-eac4-43bb-8f55-d2b8495603cc"),
                            datumRodjenjaLicnostiKomisije = new DateTime(1989, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            funkcijaLicnostiKomisije = "Prva postava",
                            imeLicnostiKomisije = "Sonja",
                            komisijaId = new Guid("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                            kontaktLicnostiKomisije = "0617825713",
                            oznakaKomisije = "kom123ef",
                            prezimeLicnostiKomisije = "Stojanovic"
                        },
                        new
                        {
                            licnostKomisijeId = new Guid("4e1f1f8d-a8f7-44b1-9bda-1c1ee122628d"),
                            datumRodjenjaLicnostiKomisije = new DateTime(1976, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            funkcijaLicnostiKomisije = "Obican clan",
                            imeLicnostiKomisije = "Petar",
                            komisijaId = new Guid("c99d5b97-6984-43ef-b0a5-89d04569466e"),
                            kontaktLicnostiKomisije = "0672514739",
                            oznakaKomisije = "kom345ef",
                            prezimeLicnostiKomisije = "Petrovic"
                        },
                        new
                        {
                            licnostKomisijeId = new Guid("3f8aa5b3-a67f-45b5-b518-771a7c09a944"),
                            datumRodjenjaLicnostiKomisije = new DateTime(1971, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            funkcijaLicnostiKomisije = "Predsednik",
                            imeLicnostiKomisije = "Mina",
                            komisijaId = new Guid("c99d5b97-6984-43ef-b0a5-89d04569466e"),
                            kontaktLicnostiKomisije = "0651516733",
                            oznakaKomisije = "kom345ef",
                            prezimeLicnostiKomisije = "Zlatic"
                        });
                });

            modelBuilder.Entity("komisijaService.Entites.LicnostKomisije", b =>
                {
                    b.HasOne("komisijaService.Entites.Komisija", null)
                        .WithMany("clanoviKomisije")
                        .HasForeignKey("komisijaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("komisijaService.Entites.Komisija", b =>
                {
                    b.Navigation("clanoviKomisije");
                });
#pragma warning restore 612, 618
        }
    }
}