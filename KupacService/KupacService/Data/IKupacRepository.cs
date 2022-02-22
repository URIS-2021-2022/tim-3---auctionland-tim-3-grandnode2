using KupacService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.Data
{
    public interface IKupacRepository
    {
        List<Kupac> GetKupci(bool imaZabranu = false);
        Kupac GetKupacById(Guid id);

        List<Guid> GetJavnaNadmetanjaByKupacId(Guid kupacId);
        void CreateKupac(Kupac kupac);
        void UpdateKupac(Kupac oldKupac, Kupac newKupac);
        void DeleteKupac(Guid id);
        bool SaveChanges();
    }
}
