using KupacService.DBContexts;
using KupacService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.Data
{
    public class PravnoLiceRepository : IPravnoLiceRepository
    {
        private readonly KupacContext context;

        public PravnoLiceRepository(KupacContext context)
        {
            this.context = context;
        }

        public void CreatePravnoLice(PravnoLice pravnoLice)
        {
            pravnoLice.PravnoLiceId = Guid.NewGuid();
            context.PravnoLice.Add(pravnoLice);
        }

        public void DeletePravnoLice(Guid id)
        {
            var pravnoLice = GetPravnoLiceById(id);
            if (pravnoLice != null)
            {
                context.Remove(pravnoLice);
            }
        }

        public PravnoLice GetPravnoLiceById(Guid id)
        {
            return context.PravnoLice.FirstOrDefault(pl => pl.PravnoLiceId.Equals(id));
        }

        public List<PravnoLice> GetPravnaLica()
        {
            return context.PravnoLice.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdatePravnoLice(PravnoLice oldPravnoLice, PravnoLice newPravnoLice)
        {

            oldPravnoLice.MaticniBroj = newPravnoLice.MaticniBroj;
            oldPravnoLice.AdresaId = newPravnoLice.AdresaId;
            oldPravnoLice.KontaktOsobaId = newPravnoLice.KontaktOsobaId;
            oldPravnoLice.BrojTelefona_1 = newPravnoLice.BrojTelefona_1;
            oldPravnoLice.BrojTelefona_2 = newPravnoLice.BrojTelefona_2;
            oldPravnoLice.Faks = newPravnoLice.Faks;
            oldPravnoLice.Email = newPravnoLice.Email;
            oldPravnoLice.BrojRacuna = newPravnoLice.BrojRacuna;
        }
    }
}
