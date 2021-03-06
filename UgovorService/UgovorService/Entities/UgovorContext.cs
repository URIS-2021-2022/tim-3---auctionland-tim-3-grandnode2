using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Data;

namespace UgovorService.Entities
{
    public class UgovorContext : DbContext
    {
        public UgovorContext(DbContextOptions<UgovorContext> options) : base(options)
        {

        }

        public DbSet<Ugovor> Ugovori { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ugovor>()
                .HasData(new
                {
                    UgovorId = Guid.Parse("9ea5d63f-f2b0-43ec-afb4-598f70958cf1"),
                    LicitacijaId = Guid.Parse("4E1F1F8D-A8F7-44B1-9BDA-1C1EE122628D"),
                    TipGarancije = TipGarancije.Jemstvo,
                    LiceId = Guid.Parse("919b3994-6c05-44b9-8f6b-47e0378491d1"),
                    RokDospeca = 30,
                    ZavodniBroj = "1233",
                    DatumZavodjenja = DateTime.Now,
                    RokZaVracanjeZemljista = DateTime.Parse("2021-12-23T00:00:00"),
                    MestoPotpisivanja = "Subotica",
                    DatumPotpisa = DateTime.Now

                });
            modelBuilder.Entity<Ugovor>()
              .HasData(new
              {
                  UgovorId = Guid.Parse("950713d6-f551-4b46-af25-5f8ec8f3e0aa"),
                  LicitacijaId = Guid.Parse("3F8AA5B3-A67F-45B5-B518-771A7C09A944"),
                  TipGarancije = TipGarancije.GarancijaNekretninom,
                  LiceId = Guid.Parse("e6aefc80-8d6b-42df-b5da-f5ec9f24600f"),
                  RokDospeca = 30,
                  ZavodniBroj = "4521",
                  DatumZavodjenja = DateTime.Now,
                  RokZaVracanjeZemljista = DateTime.Parse("2021-12-27T00:00:00"),
                  MestoPotpisivanja = "Subotica",
                  DatumPotpisa = DateTime.Now

              });
        }
    }
}
