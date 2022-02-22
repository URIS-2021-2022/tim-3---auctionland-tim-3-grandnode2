using komisijaService.DBContexts;
using komisijaService.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace komisijaService.Data
{
    public class LicnostiKomisijeRepository : ILicnostKomisijeRepository
    {
        private readonly KomisijaContext context;
        private readonly IKomisijaRepository komisijaRepository;

        public LicnostiKomisijeRepository(KomisijaContext context, IKomisijaRepository komisijaRepository)
        {
            this.context = context;
            this.komisijaRepository = komisijaRepository;
        }
        public void CreateLicnostKomisije(LicnostKomisije licnostKomisije)
        {
            licnostKomisije.licnostKomisijeId = Guid.NewGuid();
            context.LicnostiKomisije.Add(licnostKomisije);
        }

        public void DeleteLicnostKomisije(Guid id)
        {
            var licnostKomsije = GetLicnostKomisijeById(id);
            if (licnostKomsije != null)
            {
                context.Remove(licnostKomsije);
            }
        }

        public LicnostKomisije GetLicnostKomisijeById(Guid id)
        {
            return context.LicnostiKomisije.FirstOrDefault(lk => lk.licnostKomisijeId == id);
        }

        public List<LicnostKomisije> GetLicnostiKomisije(string imeLicnostiKomisije = null, string prezimeLicnostiKomisije = null, string oznakaKomisije = null)
        {
            return context.LicnostiKomisije.Where(lk => ((imeLicnostiKomisije == null || lk.imeLicnostiKomisije.Equals(imeLicnostiKomisije))
                                                        && (prezimeLicnostiKomisije == null || lk.prezimeLicnostiKomisije.Equals(prezimeLicnostiKomisije))
                                                        && (oznakaKomisije == null || lk.oznakaKomisije == oznakaKomisije))).ToList();
        }
        public List<LicnostKomisije> GetLicnosiKomisijeByOznakaKomisije(String oznakaKomisije)
        {
            return context.LicnostiKomisije.Where(lk => (lk.oznakaKomisije == oznakaKomisije)).ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateLicnostKomisije(LicnostKomisije oldLicnostKomisije, LicnostKomisije newLicnostKomisije)
        {
            oldLicnostKomisije.imeLicnostiKomisije = newLicnostKomisije.imeLicnostiKomisije;
            oldLicnostKomisije.prezimeLicnostiKomisije = newLicnostKomisije.prezimeLicnostiKomisije;
            oldLicnostKomisije.datumRodjenjaLicnostiKomisije = newLicnostKomisije.datumRodjenjaLicnostiKomisije;
            oldLicnostKomisije.kontaktLicnostiKomisije = newLicnostKomisije.kontaktLicnostiKomisije;
            oldLicnostKomisije.funkcijaLicnostiKomisije = newLicnostKomisije.funkcijaLicnostiKomisije;
            oldLicnostKomisije.oznakaKomisije = newLicnostKomisije.oznakaKomisije;
            oldLicnostKomisije.komisijaId = komisijaRepository.GetKomisjaByOznaka(newLicnostKomisije.oznakaKomisije).komisijaId; 
        }

     
        public LicnostKomisije GetPredsednikaKomisije(Guid komisijaId)
        {
            return context.LicnostiKomisije.FirstOrDefault(lk => lk.funkcijaLicnostiKomisije.Equals("Predsednik") && lk.komisijaId == komisijaId);
        }

        public void UpdateOznakuKomisije(LicnostKomisije oldLicnostKomisije, string oznakaKomisije)
        {
            oldLicnostKomisije.oznakaKomisije = oznakaKomisije;
        }
    }
}
