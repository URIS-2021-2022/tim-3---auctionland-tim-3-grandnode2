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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Ugovor>()
                .HasData(new
                {
                    UgovorId = Guid.Parse("9ea5d63f-f2b0-43ec-afb4-598f70958cf1"),
                    LicitacijaId = Guid.Parse("0e644068-2ec7-4f5c-be94-9539ed6e9a13"),
                    TipGarancije = TipGarancije.Jemstvo,
                    LiceId = Guid.Parse("919b3994-6c05-44b9-8f6b-47e0378491d1"),
                    RokDospeca = 30,
                    ZavodniBroj = "1233",
                    DatumZavodjenja = DateTime.Now,
                    RokZaVracanjeZemljista = DateTime.Parse("2021-12-23T00:00:00"),
                    MestoPotpisivanja = "Subotica",
                    DatumPotpisa = DateTime.Now

                });
            builder.Entity<Ugovor>()
              .HasData(new
              {
                  UgovorId = Guid.Parse("950713d6-f551-4b46-af25-5f8ec8f3e0aa"),
                  LicitacijaId = Guid.Parse("b46100fa-b51c-46ac-b122-0a80bb14d5e0"),
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
