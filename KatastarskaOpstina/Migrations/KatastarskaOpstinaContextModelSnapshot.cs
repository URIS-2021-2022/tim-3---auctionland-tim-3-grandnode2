// <auto-generated />
using System;
using KatastarskaOpstina.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KatastarskaOpstina.Migrations
{
    [DbContext(typeof(KatastarskaOpstinaContext))]
    partial class KatastarskaOpstinaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KatastarskaOpstina.Entities.KatastarskaOpstinaE", b =>
                {
                    b.Property<Guid>("KatastarskaOpstinaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NazivOpstine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StatutOpstineID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KatastarskaOpstinaID");

                    b.HasIndex("StatutOpstineID");

                    b.ToTable("KatastarskaOpstinas");

                    b.HasData(
                        new
                        {
                            KatastarskaOpstinaID = new Guid("6b411c13-a295-48f7-8dbd-67886c3974c0"),
                            NazivOpstine = "Bikovo",
                            StatutOpstineID = new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36")
                        },
                        new
                        {
                            KatastarskaOpstinaID = new Guid("1b411c13-a295-48f7-8dbd-67886c3974c0"),
                            NazivOpstine = "Novi Grad",
                            StatutOpstineID = new Guid("644f3de0-a9dd-4c2e-b745-89976a1b2a36")
                        });
                });

            modelBuilder.Entity("KatastarskaOpstina.Entities.StatutOpstineE", b =>
                {
                    b.Property<Guid>("StatutOpstineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DatumKreiranjaStatuta")
                        .HasColumnType("datetime2");

                    b.Property<string>("SadrzajStatuta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatutOpstineID");

                    b.ToTable("StatutOpstines");

                    b.HasData(
                        new
                        {
                            StatutOpstineID = new Guid("644f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                            DatumKreiranjaStatuta = new DateTime(2022, 2, 22, 13, 48, 21, 847, DateTimeKind.Local).AddTicks(2683),
                            SadrzajStatuta = "..."
                        },
                        new
                        {
                            StatutOpstineID = new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                            DatumKreiranjaStatuta = new DateTime(2022, 2, 22, 13, 48, 21, 850, DateTimeKind.Local).AddTicks(1855),
                            SadrzajStatuta = "..."
                        });
                });

            modelBuilder.Entity("KatastarskaOpstina.Entities.KatastarskaOpstinaE", b =>
                {
                    b.HasOne("KatastarskaOpstina.Entities.StatutOpstineE", "StatutOpstine")
                        .WithMany()
                        .HasForeignKey("StatutOpstineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StatutOpstine");
                });
#pragma warning restore 612, 618
        }
    }
}
