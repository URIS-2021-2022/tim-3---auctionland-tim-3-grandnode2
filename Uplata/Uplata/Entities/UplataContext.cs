using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Entities
{
    public class UplataContext : DbContext
    {

        public UplataContext(DbContextOptions options) :base(options)
        {
            
        }

        public DbSet<UplataEntity> Uplate { get; set; }

        public DbSet<BankaEntity> Banke { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UplataEntity>()
                .HasData(new
                {
                    UplataId = Guid.Parse("de24dc84-1744-41cd-b4d7-56b830dde7f9"),
                    BrojRacuna = 43604112,
                    PozivNaBroj = 43100222,
                    Iznos = 1500,
                    SvrhaUplate = "Uplata za javno nadmetanje u 2022. godini",
                    Datum = DateTime.Parse("10-02-2022"),
                    BankaId = Guid.Parse("9aef1da1-d5af-4073-9d40-8794f9d33564"),
                    PrijavaZaNadmetanjeId = Guid.Parse("1cd5c783-4bf5-4bbc-b7f0-bd66e2ba0bd7"),
                    KupacId = Guid.Parse("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d")
                });

            modelBuilder.Entity<UplataEntity>()
              .HasData(new
              {
                  UplataId = Guid.Parse("4f3e6672-2456-4fa6-8bf1-a7974a097136"),
                  BrojRacuna = 54715223,
                  PozivNaBroj = 54090221,
                  Iznos = 1000,
                  SvrhaUplate = "Uplata za javno nadmetanje u 2021. godini",
                  Datum = DateTime.Parse("09-02-2021"),
                  BankaId = Guid.Parse("ceed4ee2-ea12-499b-a0c9-be41d4ac0748"),
                  PrijavaZaNadmetanjeId = Guid.Parse("07c0c62b-675e-4714-816c-b492720194d6"),
                  KupacId = Guid.Parse("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d")
              });

            modelBuilder.Entity<BankaEntity>()
                .HasData(new
                {
                    BankaId = Guid.Parse("9aef1da1-d5af-4073-9d40-8794f9d33564"),
                    NazivBanke = "OTP banka",
                    Adresa = "Bulevar Oslobodjenja 80",
                    Grad = "Novi Sad"
                });

            modelBuilder.Entity<BankaEntity>()
               .HasData(new
               {
                   BankaId = Guid.Parse("ceed4ee2-ea12-499b-a0c9-be41d4ac0748"),
                   NazivBanke = "UniCredit banka",
                   Adresa = "Resavska 28",
                   Grad = "Beograd"
               });

        }
    }
}
