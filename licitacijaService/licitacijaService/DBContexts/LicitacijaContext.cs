using licitacijaService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.DBContexts
{
    public class LicitacijaContext : DbContext
    {
        private readonly IConfiguration configuration;
        public LicitacijaContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Licitacija> licitacije { get; set; }
        public DbSet<LicitacijaDokument> dokumentiLicitacije { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DatabaseForKomisija"));
        }

        /// <summary>
        /// Filling the database with data
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LicitacijaDokument>().HasKey(dl => new { dl.licitacijaId, dl.dokumentId });

            modelBuilder.Entity<Licitacija>().HasData(
               new
               {
                   licitacijaId = Guid.Parse("1f8aa5b3-a67f-45c5-b519-771a7c09a944"),
                   brojLicitacije = 1,
                   goidna = 2019,
                   ogranicenjeLicitacije = 1,
                   oznakaKomisije = "kom345ef",
                   korakCene = 1,
                   datumLicitacije = DateTime.Parse("2019-01-02"),
                   rokZaDostavuPrijava = DateTime.Now
               },
               new
               {
                   licitacijaId = Guid.Parse("2d53fc22-eac4-43bb-8f55-d2b8495603cc"),
                   brojLicitacije = 2,
                   goidna = 2021,
                   ogranicenjeLicitacije = 0,
                   oznakaKomisije = "kom345ef",
                   korakCene = 2,
                   datumLicitacije = DateTime.Parse("2021-09-18"),
                   rokZaDostavuPrijava = DateTime.Now
               },
               new
               {
                   licitacijaId = Guid.Parse("4e1f1f8d-a8f7-44b1-9bda-1c1ee122628d"),
                   brojLicitacije = 13,
                   goidna = 2021,
                   ogranicenjeLicitacije = 0,
                   oznakaKomisije = "kom123ef",
                   korakCene = 3,
                   datumLicitacije = DateTime.Parse("1921-01-19"),
                   rokZaDostavuPrijava = DateTime.Now
               },
               new
               {
                   licitacijaId = Guid.Parse("3f8aa5b3-a67f-45b5-b518-771a7c09a944"),
                   brojLicitacije = 1323,
                   goidna = 2020,
                   ogranicenjeLicitacije = 0,
                   oznakaKomisije = "kom123ef",
                   korakCene = 1,
                   datumLicitacije = DateTime.Parse("2020-09-01"),
                   rokZaDostavuPrijava = DateTime.Now
               }) ;

            modelBuilder.Entity<LicitacijaDokument>().HasData(
               new
               {
                   licitacijaId = Guid.Parse("1f8aa5b3-a67f-45c5-b519-771a7c09a944"),
                   dokumentId = Guid.Parse("b99d4b97-6984-43ef-b0a5-89d04569466e"),
                   vrstaPodnosiocaDokumenta = "f",
                   datumPodnosenjaDokumenta = DateTime.Now
               },
               new
               {
                   licitacijaId = Guid.Parse("1f8aa5b3-a67f-45c5-b519-771a7c09a944"),
                   dokumentId = Guid.Parse("a99d4b97-6984-43ef-b0a5-89d04569276e"),
                   vrstaPodnosiocaDokumenta = "f",
                   datumPodnosenjaDokumenta = DateTime.Now
               },
                 new
                 {
                     licitacijaId = Guid.Parse("1f8aa5b3-a67f-45c5-b519-771a7c09a944"),
                     dokumentId = Guid.Parse("b99d4b97-6984-43ef-b0a5-19d04569276e"),
                     vrstaPodnosiocaDokumenta = "p",
                     datumPodnosenjaDokumenta = DateTime.Now
                 },
               new
               {
                   licitacijaId = Guid.Parse("2d53fc22-eac4-43bb-8f55-d2b8495603cc"),
                   dokumentId = Guid.Parse("a99d4b97-6984-43ef-b0a5-89d04569276e"),
                   vrstaPodnosiocaDokumenta = "f",
                   datumPodnosenjaDokumenta = DateTime.Now
               },
               new
               {
                   licitacijaId = Guid.Parse("4e1f1f8d-a8f7-44b1-9bda-1c1ee122628d"),
                   dokumentId = Guid.Parse("c99d5b97-6984-43ef-b0a5-89d04569466e"),
                   vrstaPodnosiocaDokumenta = "p",
                   datumPodnosenjaDokumenta = DateTime.Now
               },
               new
               {
                   licitacijaId = Guid.Parse("4e1f1f8d-a8f7-44b1-9bda-1c1ee122628d"),
                   dokumentId = Guid.Parse("f11d5b97-6984-43ef-b0a5-89d04569466e"),
                   vrstaPodnosiocaDokumenta = "f",
                   datumPodnosenjaDokumenta = DateTime.Now
               },
               new
               {
                   licitacijaId = Guid.Parse("3f8aa5b3-a67f-45b5-b518-771a7c09a944"),
                   dokumentId = Guid.Parse("e99d4b97-6984-43ef-b0a5-89d04569466e"),
                   vrstaPodnosiocaDokumenta = "f",
                   datumPodnosenjaDokumenta = DateTime.Now
               }) ;




        }
    }
}
