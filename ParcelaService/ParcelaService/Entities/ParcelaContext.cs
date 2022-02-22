using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParcelaService.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Entities
{
    public class ParcelaContext : DbContext
    {
        public ParcelaContext(DbContextOptions<ParcelaContext> options) : base(options)
        {
        }

        public DbSet<Parcela> Parcele { get; set; }
        public DbSet<DeoParcele> DeloviParcele { get; set; }
        public DbSet<KvalitetZemljista> KvalitetiZemljista { get; set; }
        public DbSet<ZasticenaZona> ZasticeneZone { get; set; }
        public DbSet<DozvoljeniRad> DozvoljeniRadovi { get; set; }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KvalitetZemljista>()
                .HasData(new
                {
                    KvalitetZemljistaId = Guid.Parse("0943c9e9-2dc0-4d8a-92a4-4c0d7297c8f1"),
                    OznakaKvaliteta = "LK",
                    Opis = "Los kvalitet"
                });
            modelBuilder.Entity<KvalitetZemljista>()
                .HasData(new
                {
                    KvalitetZemljistaId = Guid.Parse("b767f876-7462-40d7-918e-e32472e8e07f"),
                    OznakaKvaliteta = "DK",
                    Opis = "Dobar kvalitet"
                });

            modelBuilder.Entity<ZasticenaZona>()
                .HasData(new
                {
                    ZasticenaZonaId = Guid.Parse("80a231c2-f454-4bb9-bc55-df65440ef57e"),
                    BrojZasticeneZone = 1
                });

            modelBuilder.Entity<ZasticenaZona>()
                .HasData(new
                {
                    ZasticenaZonaId = Guid.Parse("da357d41-7086-49dc-857c-17ee3085f46f"),
                    BrojZasticeneZone = 2
                });

            modelBuilder.Entity<DozvoljeniRad>()
                .HasData(new
                {
                    DozvoljeniRadId = Guid.Parse("9dcc4f86-da91-4767-8256-20e865406e60"),
                    Opis = "Sed mattis, risus id tincidunt commodo, dui massa fermentum libero.",
                    ZasticenaZonaId= Guid.Parse("80a231c2-f454-4bb9-bc55-df65440ef57e")
                }
                );

            modelBuilder.Entity<DozvoljeniRad>()
                .HasData(new
                {
                    DozvoljeniRadId = Guid.Parse("bb7617ab-eb3e-4e67-a19e-49cdd2e4e4ef"),
                    Opis = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. ",
                    ZasticenaZonaId = Guid.Parse("da357d41-7086-49dc-857c-17ee3085f46f")
                });

            modelBuilder.Entity<Parcela>()
                .HasData(new
                {
                    ParcelaId = Guid.Parse("7e2bc8e2-a0dc-4b45-8068-8bb3a9ec9605"),
                    BrojParcele = "111",
                    BrojListaNepokretnosti = "111",
                    KatastarskaOpstinaId = Guid.Parse("1b411c13-a295-48f7-8dbd-67886c3974c0"),
                    Kultura = Kultura.Livade,
                    Klasa = Klasa.II,
                    Obradivost = Obradivost.Obradivo,
                    ZasticenaZonaId = Guid.Parse("80a231c2-f454-4bb9-bc55-df65440ef57e"),
                    OblikSvojine = OblikSvojine.DrustvenaSvojina
                });

            modelBuilder.Entity<Parcela>()
                .HasData(new
                {
                    ParcelaId = Guid.Parse("f97960ee-b9f2-4910-9faa-d5bd81998f4f"),
                    BrojParcele = "222",
                    BrojListaNepokretnosti = "222",
                    KatastarskaOpstinaId = Guid.Parse("6b411c13-a295-48f7-8dbd-67886c3974c0"),
                    Kultura = Kultura.Pasnjaci,
                    Klasa = Klasa.II,
                    Obradivost = Obradivost.Obradivo,
                    ZasticenaZonaId = Guid.Parse("da357d41-7086-49dc-857c-17ee3085f46f"),
                    OblikSvojine = OblikSvojine.PrivatnaSvojina
                }) ;

            modelBuilder.Entity<DeoParcele>()
                .HasData(new
                {
                    DeoParceleId = Guid.Parse("70037ed2-cefc-498c-8a04-819d1bbd415b"),
                    ParcelaId = Guid.Parse("7e2bc8e2-a0dc-4b45-8068-8bb3a9ec9605"),
                    RedniBrojDelaParcele = 1,
                    PovrsinaDelaParcele = 300,
                    KvalitetZemljistaId = Guid.Parse("b767f876-7462-40d7-918e-e32472e8e07f")
                });

            modelBuilder.Entity<DeoParcele>()
                .HasData(new
                {
                    DeoParceleId = Guid.Parse("45504801-01fa-4054-9601-1bb7216f22f6"),
                    ParcelaId = Guid.Parse("f97960ee-b9f2-4910-9faa-d5bd81998f4f"),
                    RedniBrojDelaParcele = 2,
                    PovrsinaDelaParcele = 300,
                    KvalitetZemljistaId = Guid.Parse("0943c9e9-2dc0-4d8a-92a4-4c0d7297c8f1")
                });
        }
    }
}
