using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class JavnoNadmetanjeRepository : IJavnoNadmetanjeRepository
    {
        public static List<JavnoNadmetanjeModel> JavnaNadmetanja { get; set; } = new List<JavnoNadmetanjeModel>();

        public JavnoNadmetanjeRepository()
        {
            FillData();
        }

        private void FillData()
        {
            JavnaNadmetanja.AddRange(new List<JavnoNadmetanjeModel>
            {
                new JavnoNadmetanjeModel
                {
                    JavnoNadmetanjeId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
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
                    Status = {}
                },
                 new JavnoNadmetanjeModel
                 {
                    JavnoNadmetanjeId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
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
                    Status = {}
                 }
            });
        }

        public List<JavnoNadmetanjeModel> GetJavnaNadmetanja()
        {
            return (from j in JavnaNadmetanja select j).ToList();
        }

        public JavnoNadmetanjeModel GetJavnoNadmetanjeById(Guid javnoNadmetanjeId)
        {
            return JavnaNadmetanja.FirstOrDefault(j => j.JavnoNadmetanjeId == javnoNadmetanjeId);
        }

        public JavnoNadmetanjeModel CreateJavnoNadmetanje(JavnoNadmetanjeModel javnoNadmetanje)
        {
            javnoNadmetanje.JavnoNadmetanjeId = Guid.NewGuid();
            JavnaNadmetanja.Add(javnoNadmetanje);
            JavnoNadmetanjeModel jNadmetanje = GetJavnoNadmetanjeById(javnoNadmetanje.JavnoNadmetanjeId);
            return jNadmetanje;

        }

        public JavnoNadmetanjeModel UpdateJavnoNadmetanje(JavnoNadmetanjeModel javnoNadmetanje)
        {
            JavnoNadmetanjeModel jNadmetanje = GetJavnoNadmetanjeById(javnoNadmetanje.JavnoNadmetanjeId);

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

            return jNadmetanje;
        }

        public void DeleteJavnoNadmetanje(Guid javnoNadmetanjeId)
        {
            JavnaNadmetanja.Remove(JavnaNadmetanja.FirstOrDefault(j => j.JavnoNadmetanjeId == javnoNadmetanjeId));
        }

       
    }
}
