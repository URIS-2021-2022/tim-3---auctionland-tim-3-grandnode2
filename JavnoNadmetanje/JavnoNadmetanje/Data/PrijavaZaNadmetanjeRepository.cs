using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class PrijavaZaNadmetanjeRepository : IPrijavaZaNadmetanjeRepository
    {
        public static List<PrijavaZaNadmetanjeModel> PrijaveZaNadmetanje { get; set; } = new List<PrijavaZaNadmetanjeModel>();

        public PrijavaZaNadmetanjeRepository()
        {
            FillData();
        }

        private void FillData()
        {
            PrijaveZaNadmetanje.AddRange(new List<PrijavaZaNadmetanjeModel>
            {
                new PrijavaZaNadmetanjeModel
                {
                    PrijavaZaNadmetanjeId = Guid.Parse("07c0c62b-675e-4714-816c-b492720194d6"),
                    DatumPrijave = DateTime.Parse("09-02-2022"),
                    MestoPrijave = "Novi Sad"
                },
                new PrijavaZaNadmetanjeModel
                {
                    PrijavaZaNadmetanjeId = Guid.Parse("1cd5c783-4bf5-4bbc-b7f0-bd66e2ba0bd7"),
                    DatumPrijave = DateTime.Parse("08-02-2022"),
                    MestoPrijave = "Subotica"
                }
            });
        }

        public List<PrijavaZaNadmetanjeModel> GetPrijaveZaNadmetanje()
        {
            return (from p in PrijaveZaNadmetanje select p).ToList();
        }

        public PrijavaZaNadmetanjeModel GetPrijavaZaNadmetanjeById(Guid prijavaZaNadmetanjeId)
        {
            return PrijaveZaNadmetanje.FirstOrDefault(p => p.PrijavaZaNadmetanjeId == prijavaZaNadmetanjeId);
        }

        public PrijavaZaNadmetanjeModel CreatePrijavaZaNadmetanje(PrijavaZaNadmetanjeModel prijavaZaNadmetanje)
        {
            prijavaZaNadmetanje.PrijavaZaNadmetanjeId= Guid.NewGuid();
            PrijaveZaNadmetanje.Add(prijavaZaNadmetanje);
            PrijavaZaNadmetanjeModel prijavaNadmetanje = GetPrijavaZaNadmetanjeById(prijavaZaNadmetanje.PrijavaZaNadmetanjeId);
            return prijavaNadmetanje;
        }

        public PrijavaZaNadmetanjeModel UpdatePrijavaZaNadmetanje(PrijavaZaNadmetanjeModel prijavaZaNadmetanje)
        {
            PrijavaZaNadmetanjeModel prijavaNadmetanje = GetPrijavaZaNadmetanjeById(prijavaZaNadmetanje.PrijavaZaNadmetanjeId);

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
