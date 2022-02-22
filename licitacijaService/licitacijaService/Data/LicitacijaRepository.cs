using licitacijaService.DBContexts;
using licitacijaService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.Data
{
    public class LicitacijaRepository : ILicitacijaRepository
    {
        private readonly LicitacijaContext context;


        public LicitacijaRepository(LicitacijaContext context)
        {
            this.context = context;

        }
        public void CreateLicitacija(Licitacija licitacija)
        {
            licitacija.licitacijaId = Guid.NewGuid();
            context.licitacije.Add(licitacija);
        }

        public void DeleteLicitacija(Guid id)
        {
            var licitacija = GetLicitacijaById(id);
            if (licitacija != null)
            {
                context.Remove(licitacija);
            }
        }

        public Licitacija GetLicitacijaById(Guid id)
        {
            return context.licitacije.FirstOrDefault(l => l.licitacijaId == id);
        }

        public List<Licitacija> GetLicitacije(string brojLicitacije = null)
        {
            
                return context.licitacije.Where(l => (brojLicitacije == null || l.brojLicitacije == Int32.Parse(brojLicitacije))).ToList();
            
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateLicitacija(Licitacija oldLicitacija, Licitacija newLicitacija)
        {
            oldLicitacija.brojLicitacije = newLicitacija.brojLicitacije;
            oldLicitacija.datumLicitacije = newLicitacija.datumLicitacije;
            oldLicitacija.goidna = newLicitacija.goidna;
            oldLicitacija.oznakaKomisije = newLicitacija.oznakaKomisije;
            oldLicitacija.korakCene = newLicitacija.korakCene;
            oldLicitacija.ogranicenjeLicitacije = newLicitacija.ogranicenjeLicitacije;
            oldLicitacija.rokZaDostavuPrijava = newLicitacija.rokZaDostavuPrijava;
        }
    }
}
