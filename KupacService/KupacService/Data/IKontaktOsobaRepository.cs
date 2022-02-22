using KupacService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.Data
{
    public interface IKontaktOsobaRepository
    {
        List<KontaktOsoba> GetKontaktOsobe();
        KontaktOsoba GetKontaktOsobaById(Guid id);
        void CreateKontaktOsoba(KontaktOsoba kontaktOsoba);
        void UpdateKontaktOsoba(KontaktOsoba oldKontaktOsoba, KontaktOsoba newKontaktOsoba);
        void DeleteKontaktOsoba(Guid id);
        bool SaveChanges();
    }
}
