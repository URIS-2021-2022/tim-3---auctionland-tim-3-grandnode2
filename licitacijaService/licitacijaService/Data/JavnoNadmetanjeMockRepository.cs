using licitacijaService.DTOs.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.Data
{
    public class JavnoNadmetanjeMockRepository : IJavnoNadmetanjeMockRepository
    {
        public static List<JavnoNadmetanjeDTO> JavnaNadmetanja { get; set; } = new List<JavnoNadmetanjeDTO>();

        public JavnoNadmetanjeMockRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            JavnaNadmetanja.AddRange(new List<JavnoNadmetanjeDTO>
            {
                 new JavnoNadmetanjeDTO
                {
                    javnoNadmetanjeId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Datum = DateTime.Parse("10-02-2022"),
                    VremePocetka = DateTime.Parse("9:00:00"),
                    VremeKraja = DateTime.Parse("13:00:00"),
                    PocetnaCenaPoHektaru = 2000,
                    Izuzeto = false,
                    Tip = {},
                    IzlicitiranaCena = 1500,
                    PeriodZakupa = 3,
                    BrojUcesnika = 25,
                    VisinaDopuneDepozita = 500,
                    Krug = 1,
                    Status = {},
                    licitacijaId = Guid.Parse("1F8AA5B3-A67F-45C5-B519-771A7C09A944")

                },
                 new JavnoNadmetanjeDTO
                 {
                    javnoNadmetanjeId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Datum = DateTime.Parse("11-02-2022"),
                    VremePocetka = DateTime.Parse("10:00:00"),
                    VremeKraja = DateTime.Parse("14:00:00"),
                    PocetnaCenaPoHektaru = 1500,
                    Izuzeto = true,
                    Tip = {},
                    IzlicitiranaCena = 900,
                    PeriodZakupa = 5,
                    BrojUcesnika = 10,
                    VisinaDopuneDepozita = 250,
                    Krug = 1,
                    Status = {},
                    licitacijaId = Guid.Parse("1F8AA5B3-A67F-45C5-B519-771A7C09A944")
                 }
            }) ;
        }
        public void DeleteJvanoNadmetanjeByLicitacijaId(Guid idLicitacije)
        {
            JavnaNadmetanja.RemoveAll(jn => (jn.licitacijaId == idLicitacije));
        }

        public List<JavnoNadmetanjeDTO> GetJavnaNadmetanjaByLicitacijaId(Guid licitacijaId)
        {
            return (from j in JavnaNadmetanja select j).Where(jn => (jn.licitacijaId==licitacijaId)).ToList();
        }
    }
}
