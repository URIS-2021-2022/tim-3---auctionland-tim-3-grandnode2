using KupacService.DBContexts;
using KupacService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.Data
{
    public class LiciterRepository : ILiciterRepository
    {
        private readonly KupacContext context;

        public LiciterRepository(KupacContext context)
        {
            this.context = context;
        }

        public void CreateLiciter(Liciter liciter)
        {
            liciter.KupacId = Guid.NewGuid();
            context.Liciter.Add(liciter);
        }

        public void DeleteLiciter(Guid id)
        {
            var liciter = GetLiciterById(id);
            if (liciter != null)
            {
                context.Remove(liciter);
            }
        }

        public Liciter GetLiciterById(Guid id)
        {
            return context.Liciter.FirstOrDefault(l => l.KupacId.Equals(id));
        }

        public List<Liciter> GetLiciteri()
        {
            return context.Liciter.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateLiciter(Liciter oldLiciter, Liciter newLiciter)
        {

            oldLiciter.Prioritet = newLiciter.Prioritet;
            oldLiciter.OstvarenaPovrsina = newLiciter.OstvarenaPovrsina;
            oldLiciter.ImaZabranu = newLiciter.ImaZabranu;
            oldLiciter.DatumPocetkaZabrane = newLiciter.DatumPocetkaZabrane;
            oldLiciter.DuzinaTrajanjaZabraneUGodinama = newLiciter.DuzinaTrajanjaZabraneUGodinama;
            oldLiciter.UplateId = newLiciter.UplateId;
            oldLiciter.OvlascenoLice = newLiciter.OvlascenoLice;
            oldLiciter.JavnaNadmetanjaId = newLiciter.JavnaNadmetanjaId;
            oldLiciter.BrojLicence = newLiciter.BrojLicence;
        }
    }
}
