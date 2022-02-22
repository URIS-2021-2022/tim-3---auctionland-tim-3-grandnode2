using KupacService.DBContexts;
using KupacService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.Data
{
    public class KontaktOsobaRepository : IKontaktOsobaRepository
    {
        private readonly KupacContext context;

        public KontaktOsobaRepository(KupacContext context)
        {
            this.context = context;
        }

        public void CreateKontaktOsoba(KontaktOsoba kontaktOsoba)
        {
            kontaktOsoba.KontaktOsobaId = Guid.NewGuid();
            context.KontaktOsoba.Add(kontaktOsoba);
        }

        public void DeleteKontaktOsoba(Guid id)
        {
            var kontaktOsoba = GetKontaktOsobaById(id);
            if (kontaktOsoba != null)
            {
                context.Remove(kontaktOsoba);
            }
        }

        public KontaktOsoba GetKontaktOsobaById(Guid id)
        {
            return context.KontaktOsoba.FirstOrDefault(ko => ko.KontaktOsobaId.Equals(id));
        }

        public List<KontaktOsoba> GetKontaktOsobe()
        {
            return context.KontaktOsoba.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateKontaktOsoba(KontaktOsoba oldKontaktOsoba, KontaktOsoba newKontaktOsoba)
        {
            oldKontaktOsoba.Ime = newKontaktOsoba.Ime;
            oldKontaktOsoba.Prezime = newKontaktOsoba.Prezime;
            oldKontaktOsoba.Funkcija = newKontaktOsoba.Funkcija;
            oldKontaktOsoba.Telefon = newKontaktOsoba.Telefon; 
        }
    }
}
