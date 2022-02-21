using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Entities;

namespace Uplata.Data
{
    public class UplataMockRepository : IUplataRepository
    {
        public static List<UplataEntity> Uplate { get; set; } = new List<UplataEntity>();

        public UplataMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Uplate.AddRange(new List<UplataEntity>
            {
                new UplataEntity
                {
                    UplataId = Guid.Parse("de24dc84-1744-41cd-b4d7-56b830dde7f9"),
                    BrojRacuna = 43604112,
                    PozivNaBroj = 43100222,
                    Iznos = 1500,
                    SvrhaUplate = "Uplata za javno nadmetanje u 2022. godini",
                    Datum = DateTime.Parse("10-02-2022"),
                    BankaId = Guid.Parse("9aef1da1-d5af-4073-9d40-8794f9d33564"),
                    PrijavaZaNadmetanjeId = Guid.Parse("1cd5c783-4bf5-4bbc-b7f0-bd66e2ba0bd7")

                },
                new UplataEntity
                {
                    UplataId = Guid.Parse("4f3e6672-2456-4fa6-8bf1-a7974a097136"),
                    BrojRacuna = 54715223,
                    PozivNaBroj = 54090221,
                    Iznos = 1000,
                    SvrhaUplate = "Uplata za javno nadmetanje u 2021. godini",
                    Datum = DateTime.Parse("09-02-2021"),
                    BankaId = Guid.Parse("ceed4ee2-ea12-499b-a0c9-be41d4ac0748"),
                    PrijavaZaNadmetanjeId = Guid.Parse("07c0c62b-675e-4714-816c-b492720194d6")
                }
            }); 
        }

        public List<UplataEntity> GetUplate()
        {
            return (from u in Uplate select u).ToList();
        }

        public UplataEntity GetUplataById(Guid uplataId)
        {
            return Uplate.FirstOrDefault(u => u.UplataId == uplataId);
        }

        public List<UplataEntity> GetUplateByPrijavaZaNadmetanjeId(Guid prijavaZaNadmetanjeId)
        {
            return Uplate.Where(u => (prijavaZaNadmetanjeId == null || u.PrijavaZaNadmetanjeId == prijavaZaNadmetanjeId)).ToList();
        }

        public UplataEntity CreateUplata(UplataEntity uplata)
        {
            uplata.UplataId = Guid.NewGuid();
            Uplate.Add(uplata);
            UplataEntity uplata1 = GetUplataById(uplata.UplataId);
            return uplata1;
        }

        public void UpdateUplata(UplataEntity uplata)
        {
            UplataEntity uplata1 = GetUplataById(uplata.UplataId);

            uplata1.BrojRacuna = uplata.BrojRacuna;
            uplata1.PozivNaBroj = uplata.PozivNaBroj;
            uplata1.Iznos = uplata.Iznos;
            uplata1.SvrhaUplate = uplata.SvrhaUplate;
            uplata1.Datum = uplata.Datum;
            uplata1.BankaId = uplata.BankaId;
            uplata1.PrijavaZaNadmetanjeId = uplata.PrijavaZaNadmetanjeId;

        }
        public void DeleteUplata(Guid uplataId)
        {
            Uplate.Remove(Uplate.FirstOrDefault(u => u.UplataId == uplataId));
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
