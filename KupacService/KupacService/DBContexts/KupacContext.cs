using KupacService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.DBContexts
{
    public class KupacContext: DbContext
    {
        private readonly IConfiguration configuration;
        public KupacContext(DbContextOptions options, IConfiguration configuration): base(options)
        {
            this.configuration = configuration;
        }

        /*
         public DbSet<LicnostKomisije> LicnostiKomisije { get; set; }
        public DbSet<Komisija> Komisija { get; set; }

         */

        public DbSet<Kupac> Kupac { get; set; }
        public DbSet<PravnoLice> PravnoLice { get; set; }
        public DbSet<FizickoLice> FizickoLice { get; set; }
        public DbSet<Liciter> Liciter { get; set;}
        public DbSet<KontaktOsoba> KontaktOsoba { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("KupacDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kupac>()
                .HasIndex(b => b.KupacId)
                .IsUnique();
            modelBuilder.Entity<Kupac>().HasData(
                    new
                    {
                        KupacId = Guid.Parse("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                        Prioritet = new List<int> { 1, 2, 3 },
                        Lice = 1,
                        OstvarenaPovrsina = 100,
                        UplateId = new List<Guid> { Guid.Parse("3f8aa5b3-a67f-45b5-b518-771a7c09a944") },
                        OvlascenoLiceID = new List<Guid> { Guid.Parse("3f8aa5b3-a67f-45b5-b518-771a7c09a944") },
                        ImaZabranu = false,
                        JavnaNadmetanjaId = new List<Guid> { Guid.Parse("3f8aa5b3-a67f-45b5-b518-771a7c09a944") }
                    }
                );

            modelBuilder.Entity<PravnoLice>().HasData(
                    new
                    {
                        PravnoLiceId = Guid.Parse("3f8aa5b3-a67f-45b5-b518-771a7c09a944"),
                        MaticniBroj = "123456",
                        Adresa = Guid.Parse("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                        KontaktOsoba = Guid.Parse("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                        BrojTelefona_1 = "123456",
                        BrojTelefona_2 = "1234567",
                        Faks = "123456",
                        Email = "mail@mail.com",
                        BrojRacuna = "12345-32"
                    }
                );

            modelBuilder.Entity<FizickoLice>().HasData(
                    new
                    {
                        FizickoLiceId = Guid.Parse("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                        Ime = "Ime",
                        Prezime = "Prezime",
                        Jmbg = "123456789",
                        Adresa = Guid.Parse("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                        BrojTelefona_1 = "123450",
                        BrojTelefona_2 = "123456",
                        Email = "mail@mail.com",
                        BrojRacuna = "1234-42"
                    }
                );

            modelBuilder.Entity<Liciter>().HasData(
                    new
                    {
                        KupacId = Guid.Parse("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                        Prioritet = new List<int> { 1, 2, 3 },
                        Lice = 1,
                        OstvarenaPovrsina = 100,
                        UplateId = new List<Guid> { Guid.Parse("3f8aa5b3-a67f-45b5-b518-771a7c09a944") },
                        OvlascenoLiceID = new List<Guid> { Guid.Parse("3f8aa5b3-a67f-45b5-b518-771a7c09a944") },
                        ImaZabranu = false,
                        JavnaNadmetanjaId = new List<Guid> { Guid.Parse("3f8aa5b3-a67f-45b5-b518-771a7c09a944") },
                        BrojLicence = "123456"
                    }
                );

            modelBuilder.Entity<KontaktOsoba>().HasData(
                    new
                    {
                        KontaktOsobaId = Guid.Parse("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                        Ime = "Ime",
                        Prezime = "Prezime",
                        Funkcija = "Funkcija1",
                        Telefon = "1233456"
                    }
                );
        }
    }
}
