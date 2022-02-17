using JavnoNadmetanje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class PrijavaZaNadmetanjeRepository : IPrijavaZaNadmetanjeRepository
    {
        public static List<PrijavaZaNadmetanjeEntity> PrijaveZaNadmetanje { get; set; } = new List<PrijavaZaNadmetanjeEntity>();

        public PrijavaZaNadmetanjeRepository()
        {
            FillData();
        }

        private void FillData()
        {
            PrijaveZaNadmetanje.AddRange(new List<PrijavaZaNadmetanjeEntity>
            {
                new PrijavaZaNadmetanjeEntity
                {
                    PrijavaZaNadmetanjeId = Guid.Parse("07c0c62b-675e-4714-816c-b492720194d6"),
                    DatumPrijave = DateTime.Parse("09-02-2022"),
                    MestoPrijave = "Novi Sad"
                },
                new PrijavaZaNadmetanjeEntity
                {
                    PrijavaZaNadmetanjeId = Guid.Parse("1cd5c783-4bf5-4bbc-b7f0-bd66e2ba0bd7"),
                    DatumPrijave = DateTime.Parse("08-02-2022"),
                    MestoPrijave = "Subotica"
                }
            });
        }

        public List<PrijavaZaNadmetanjeEntity> GetPrijaveZaNadmetanje()
        {
            return (from p in PrijaveZaNadmetanje select p).ToList();
        }

        public PrijavaZaNadmetanjeEntity GetPrijavaZaNadmetanjeById(Guid prijavaZaNadmetanjeId)
        {
            return PrijaveZaNadmetanje.FirstOrDefault(p => p.PrijavaZaNadmetanjeId == prijavaZaNadmetanjeId);
        }

        public PrijavaZaNadmetanjeEntity CreatePrijavaZaNadmetanje(PrijavaZaNadmetanjeEntity prijavaZaNadmetanje)
        {
            prijavaZaNadmetanje.PrijavaZaNadmetanjeId= Guid.NewGuid();
            PrijaveZaNadmetanje.Add(prijavaZaNadmetanje);
            PrijavaZaNadmetanjeEntity prijavaNadmetanje = GetPrijavaZaNadmetanjeById(prijavaZaNadmetanje.PrijavaZaNadmetanjeId);
            return prijavaNadmetanje;
        }

        public PrijavaZaNadmetanjeEntity UpdatePrijavaZaNadmetanje(PrijavaZaNadmetanjeEntity prijavaZaNadmetanje)
        {
            PrijavaZaNadmetanjeEntity prijavaNadmetanje = GetPrijavaZaNadmetanjeById(prijavaZaNadmetanje.PrijavaZaNadmetanjeId);

            prijavaNadmetanje.DatumPrijave = prijavaZaNadmetanje.DatumPrijave;
            prijavaNadmetanje.MestoPrijave = prijavaZaNadmetanje.MestoPrijave;

            return prijavaNadmetanje;
        }

        public void DeletePrijavaZaNadmetanje(Guid prijavaZaNadmetanjeId)
        {
            PrijaveZaNadmetanje.Remove(PrijaveZaNadmetanje.FirstOrDefault(p => p.PrijavaZaNadmetanjeId== prijavaZaNadmetanjeId));
        }

    }
}
