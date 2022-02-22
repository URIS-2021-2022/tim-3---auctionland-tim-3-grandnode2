using KupacService.DBContexts;
using KupacService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.Data
{
    public class KupacRepository : IKupacRepository
    {
        private readonly KupacContext context;

        public KupacRepository(KupacContext context)
        {
            this.context = context;
        }

        public void CreateKupac(Kupac kupac)
        {
            kupac.KupacId = Guid.NewGuid();
            context.Kupac.Add(kupac);
        }

        public void DeleteKupac(Guid id)
        {
            var kupac = GetKupacById(id);
            if (kupac != null)
            {
                context.Remove(kupac);
            }
        }

        public Kupac GetKupacById(Guid id)
        {
            return context.Kupac.FirstOrDefault(k => k.KupacId.Equals(id));
        }

        public List<Kupac> GetKupci(bool imaZabranu = false)
        {
            return context.Kupac.Where(k => k.ImaZabranu == imaZabranu).ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateKupac(Kupac oldKupac, Kupac newKupac)
        {
            oldKupac.Prioritet = newKupac.Prioritet;
            oldKupac.OstvarenaPovrsina = newKupac.OstvarenaPovrsina;
            oldKupac.ImaZabranu = newKupac.ImaZabranu;
            oldKupac.DatumPocetkaZabrane = newKupac.DatumPocetkaZabrane;
            oldKupac.DuzinaTrajanjaZabraneUGodinama = newKupac.DuzinaTrajanjaZabraneUGodinama;
            oldKupac.UplateId = newKupac.UplateId;
            oldKupac.OvlascenoLice = newKupac.OvlascenoLice;
            oldKupac.JavnaNadmetanjaId = newKupac.JavnaNadmetanjaId;
        }

        public List<Guid> GetJavnaNadmetanjaByKupacId(Guid kupacId)
        {
            return new List<Guid>
            {
                Guid.Parse("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                Guid.Parse("3f8aa5b3-a67f-45b5-b518-771a7c09a944"),
                Guid.Parse("4e1f1f8d-af35-44b1-9abd-1c1ee122628d")
            };
        }
    }
}
