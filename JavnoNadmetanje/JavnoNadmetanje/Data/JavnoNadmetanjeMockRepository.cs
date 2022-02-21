using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class JavnoNadmetanjeMockRepository : IJavnoNadmetanjeRepository
    {

        public static List<JavnoNadmetanjeEntity> JavnaNadmetanja { get; set; } = new List<JavnoNadmetanjeEntity>();

        public JavnoNadmetanjeMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            JavnaNadmetanja.AddRange(new List<JavnoNadmetanjeEntity>
            {
                new JavnoNadmetanjeEntity
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
                },
                 new JavnoNadmetanjeEntity
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
                 }
            });
        }

        public List<JavnoNadmetanjeEntity> GetJavnaNadmetanja()
        {
            return (from j in JavnaNadmetanja select j).ToList();
        }

        public JavnoNadmetanjeEntity GetJavnoNadmetanjeById(Guid javnoNadmetanjeId)
        {
            return JavnaNadmetanja.FirstOrDefault(j => j.JavnoNadmetanjeId == javnoNadmetanjeId);
        }

        public List<JavnoNadmetanjeEntity> GetJavnaNadmetanjaByLicitacijaId(Guid licitacijaId)
        {
            return JavnaNadmetanja.Where(j => (licitacijaId == null || j.LicitacijaId == licitacijaId)).ToList();
        }

        public JavnoNadmetanjeEntity CreateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje)
        {
            javnoNadmetanje.JavnoNadmetanjeId = Guid.NewGuid();
            JavnaNadmetanja.Add(javnoNadmetanje);
            JavnoNadmetanjeEntity jNadmetanje = GetJavnoNadmetanjeById(javnoNadmetanje.JavnoNadmetanjeId);
            return jNadmetanje;
        }

        public void UpdateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje)
        {
            JavnoNadmetanjeEntity jNadmetanje = GetJavnoNadmetanjeById(javnoNadmetanje.JavnoNadmetanjeId);

            jNadmetanje.Datum = javnoNadmetanje.Datum;
            jNadmetanje.VremePocetka = javnoNadmetanje.VremePocetka;
            jNadmetanje.VremeKraja = javnoNadmetanje.VremeKraja;
            jNadmetanje.PocetnaCenaPoHektaru = javnoNadmetanje.PocetnaCenaPoHektaru;
            jNadmetanje.Izuzeto = javnoNadmetanje.Izuzeto;
            jNadmetanje.Tip = javnoNadmetanje.Tip;
            jNadmetanje.IzlicitiranaCena = javnoNadmetanje.IzlicitiranaCena;
            jNadmetanje.PeriodZakupa = javnoNadmetanje.PeriodZakupa;
            jNadmetanje.BrojUcesnika = javnoNadmetanje.BrojUcesnika;
            jNadmetanje.VisinaDopuneDepozita = javnoNadmetanje.VisinaDopuneDepozita;
            jNadmetanje.Krug = javnoNadmetanje.Krug;
            jNadmetanje.Status = javnoNadmetanje.Status;

        }

        public void DeleteJavnoNadmetanje(Guid javnoNadmetanjeId)
        {
            JavnaNadmetanja.Remove(JavnaNadmetanja.FirstOrDefault(j => j.JavnoNadmetanjeId == javnoNadmetanjeId));
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
