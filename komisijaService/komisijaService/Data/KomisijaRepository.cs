using komisijaService.DBContexts;
using komisijaService.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace komisijaService.Data
{
    public class KomisijaRepository : IKomisijaRepository
    {
        private readonly KomisijaContext context;
      

        public KomisijaRepository(KomisijaContext context )
        {
            this.context = context;
         
        }
        public void CreateKomsija(Komisija komisija)
        {
            komisija.komisijaId = Guid.NewGuid();
            context.Komisija.Add(komisija);
        }

        public void DeleteKomsija(Guid id)
        {
            var komisija = GetKomisijaById(id);
            if (komisija != null)
            {
                context.Remove(komisija);
            }
        }

        public Komisija GetKomisijaById(Guid id)
        {
            return context.Komisija.FirstOrDefault(k => k.komisijaId == id);
        }

        public Komisija GetKomisjaByOznaka(string oznakaKomisije)
        {
            return context.Komisija.FirstOrDefault(k => k.oznakaKomisije.Equals(oznakaKomisije));
        }

        public List<Komisija> GetKomsijas(string naziv = null, string oznakaKomisije = null)
        {
            return context.Komisija.Where(k => ((naziv == null || k.nazivKomisije == naziv) && (oznakaKomisije == null || k.oznakaKomisije == oznakaKomisije))).ToList();
        }


        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateKomisija(Komisija oldKomisija, Komisija newKomisija)
        {
            oldKomisija.nazivKomisije = newKomisija.nazivKomisije;
            oldKomisija.oznakaKomisije = newKomisija.oznakaKomisije;
        }
    }
}
