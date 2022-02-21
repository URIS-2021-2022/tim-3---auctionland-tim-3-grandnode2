using JavnoNadmetanje.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Entities
{
    public class JavnoNadmetanjeContext : DbContext
    {
        public JavnoNadmetanjeContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<JavnoNadmetanjeEntity> JavnaNadmetanja { get; set; }

        public DbSet<PrijavaZaNadmetanjeEntity> PrijaveZaNadmetanje { get; set; }

        public DbSet<OglasEntity> Oglasi { get; set; }

        public DbSet<SluzbeniListEntity> SluzbeniListovi { get; set; }

        public DbSet<DokumentPrijavaZaNadmetanjeEntity> DokumentiPrijave { get; set; }

        /// <summary>
        /// Metoda koja popunjava bazu sa inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DokumentPrijavaZaNadmetanjeEntity>().HasKey(dp => new { dp.PrijavaZaNadmetanjeId, dp.DokumentId });

            builder.Entity<JavnoNadmetanjeEntity>()
                .HasData(new
                {
                    JavnoNadmetanjeId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Datum = DateTime.Parse("10-02-2022"),
                    VremePocetka = DateTime.Parse("9:00:00"),
                    VremeKraja = DateTime.Parse("13:00:00"),
                    PocetnaCenaPoHektaru = 2000,
                    Izuzeto = false,
                    Tip = TipJavnogNadmetanja.JavnaLicitacija,
                    IzlicitiranaCena = 1500,
                    PeriodZakupa = 3,
                    BrojUcesnika = 25,
                    VisinaDopuneDepozita = 500,
                    Krug = 1,
                    Status = StatusJavnogNadmetanja.PrviKrug,
                    OglasId = Guid.Parse("382e1636-2705-477e-95c4-8727e819c5e9"),
                    LicitacijaId = Guid.Parse("861e7d2e-268f-495f-8bd3-dbfb4f0594a4"),
                    ParcelaId = Guid.Parse("35d3c2da-7e55-4730-a4ed-9f886e24e6f9"),
                    KupacId = Guid.Parse("bc03a6fb-b322-4797-b6c4-0a899615f653")
                });

            builder.Entity<JavnoNadmetanjeEntity>()
                .HasData(new
                {
                    JavnoNadmetanjeId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Datum = DateTime.Parse("11-02-2022"),
                    VremePocetka = DateTime.Parse("10:00:00"),
                    VremeKraja = DateTime.Parse("14:00:00"),
                    PocetnaCenaPoHektaru = 1500,
                    Izuzeto = true,
                    Tip = TipJavnogNadmetanja.OtvaranjeZatvorenihPonuda,
                    IzlicitiranaCena = 900,
                    PeriodZakupa = 5,
                    BrojUcesnika = 10,
                    VisinaDopuneDepozita = 250,
                    Krug = 1,
                    Status = StatusJavnogNadmetanja.PrviKrug,
                    OglasId = Guid.Parse("abd912e3-5962-463e-a04e-5fdd2b43e30f"),
                    LicitacijaId = Guid.Parse("eca6f180-d90e-4432-a9dc-3a50f5b704b5"),
                    ParcelaId = Guid.Parse("afdc833f-faf6-4bc1-862c-4ad94273690d"),
                    KupacId = Guid.Parse("80b7a335-bc5f-4a72-861e-2c914e14e2b4")

                });

            builder.Entity<PrijavaZaNadmetanjeEntity>()
                .HasData(new
                {
                    PrijavaZaNadmetanjeId = Guid.Parse("07c0c62b-675e-4714-816c-b492720194d6"),
                    DatumPrijave = DateTime.Parse("09-02-2022"),
                    MestoPrijave = "Novi Sad",
                    JavnoNadmetanjeId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0")
                });

            builder.Entity<PrijavaZaNadmetanjeEntity>()
                .HasData(new
                {
                    PrijavaZaNadmetanjeId = Guid.Parse("1cd5c783-4bf5-4bbc-b7f0-bd66e2ba0bd7"),
                    DatumPrijave = DateTime.Parse("08-02-2022"),
                    MestoPrijave = "Subotica",
                    JavnoNadmetanjeId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b")
                });

            builder.Entity<OglasEntity>()
                .HasData(new
                {
                    OglasId = Guid.Parse("382e1636-2705-477e-95c4-8727e819c5e9"),
                    DatumObjavljivanjaOglasa = DateTime.Parse("05-10-2021"),
                    GodinaObjavljivanjaOglasa = 2021,
                    SluzbeniListId = Guid.Parse("76e60dd7-0e18-4c7c-abe0-b59524eca5ff")

                });

            builder.Entity<OglasEntity>()
                .HasData(new
                {
                    OglasId = Guid.Parse("abd912e3-5962-463e-a04e-5fdd2b43e30f"),
                    DatumObjavljivanjaOglasa = DateTime.Parse("05-10-2022"),
                    GodinaObjavljivanjaOglasa = 2022,
                    SluzbeniListId = Guid.Parse("1a0d7558-2ebc-45df-83d3-13066c36d42b")
                });

            builder.Entity<SluzbeniListEntity>()
                .HasData(new
                {
                    SluzbeniListId = Guid.Parse("1a0d7558-2ebc-45df-83d3-13066c36d42b"),
                    Opstina = "Novi Sad",
                    BrojSluzbenogLista = 5,
                    DatumIzdavanja = DateTime.Parse("11-10-2021")
                });

            builder.Entity<SluzbeniListEntity>()
                .HasData(new
                {
                    SluzbeniListId = Guid.Parse("76e60dd7-0e18-4c7c-abe0-b59524eca5ff"),
                    Opstina = "Subotica",
                    BrojSluzbenogLista = 8,
                    DatumIzdavanja = DateTime.Parse("11-01-2022")
                });

            builder.Entity<DokumentPrijavaZaNadmetanjeEntity>()
                .HasData(new
                {
                    PrijavaZaNadmetanjeId = Guid.Parse("07c0c62b-675e-4714-816c-b492720194d6"),
                    DokumentId = Guid.Parse("b99d4b97-6984-43ef-b0a5-89d04569466e"),
                    DatumDonosenjaDokumenta = DateTime.Parse("09-02-2022")
                });

            builder.Entity<DokumentPrijavaZaNadmetanjeEntity>()
                .HasData(new
                {
                    PrijavaZaNadmetanjeId = Guid.Parse("1cd5c783-4bf5-4bbc-b7f0-bd66e2ba0bd7"),
                    DokumentId = Guid.Parse("a99d4b97-6984-43ef-b0a5-89d04569276e"),
                    DatumDonosenjaDokumenta = DateTime.Parse("08-02-2022")
                });
        }
    }
}
