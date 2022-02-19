using komisijaService.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace komisijaService.Data
{
    public interface ILicnostKomisijeRepository
    {
        List<LicnostKomisije> GetLicnostiKomisije(string imeLicnostiKomisije = null, string prezimeLicnostiKomisije = null, string oznakaKomisije = null);
        LicnostKomisije GetLicnostKomisijeById(Guid id);
        LicnostKomisije GetPredsednikaKomisije(Guid komisijaId);
        void CreateLicnostKomisije(LicnostKomisije licnostKomisije);
        void UpdateLicnostKomisije(LicnostKomisije oldLicnostKomisije, LicnostKomisije newLicnostKomisije);

        void UpdateOznakuKomisije(LicnostKomisije oldLicnostKomisije, string oznakaKomisije);
        void DeleteLicnostKomisije(Guid id);
        bool SaveChanges();
        List<LicnostKomisije> GetLicnosiKomisijeByOznakaKomisije(String oznakaKomisije);
    }
}
