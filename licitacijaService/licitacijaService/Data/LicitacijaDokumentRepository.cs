using licitacijaService.DBContexts;
using licitacijaService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.Data
{
    public class LicitacijaDokumentRepository : ILicitacijaDokumentRepository
    {
        private readonly LicitacijaContext context;


        public LicitacijaDokumentRepository(LicitacijaContext context)
        {
            this.context = context;

        }
        public void CreateLicitacijaDokument(LicitacijaDokument licitacijaDokument)
        {
            licitacijaDokument.datumPodnosenjaDokumenta = DateTime.Now;
            context.dokumentiLicitacije.Add(licitacijaDokument);
        }

        public void DeleteLicitacijaDokumentByLicitacijaId(Guid idLicitacije)
        {
            List<LicitacijaDokument> licitacijaDokuments = GetDokumnetByLicitacijaId(idLicitacije);
            if (licitacijaDokuments != null && licitacijaDokuments.Count>0)
            {
                foreach(var lic in licitacijaDokuments)
                {
                    context.Remove(lic);
                }
            }
        }

        public LicitacijaDokument GetDokumnetById(Guid idLicitacije, Guid dokumentId)
        {
            return context.dokumentiLicitacije.FirstOrDefault(l =>  (l.licitacijaId == idLicitacije && l.dokumentId == dokumentId));
        }

        public List<LicitacijaDokument> GetDokumnetByLicitacijaId(Guid idLicitacije)
        {
            return context.dokumentiLicitacije.Where(l => (l.licitacijaId==idLicitacije)).ToList();
        }

        public List<LicitacijaDokument> GetDokumnetByLicitacijaIdAndVrstaPodnosioca(Guid idLicitacije, string indikatorVrste)
        {
            return context.dokumentiLicitacije.Where(l => (l.licitacijaId == idLicitacije && l.vrstaPodnosiocaDokumenta.Equals(indikatorVrste))).ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateLicitacijaDokument(LicitacijaDokument oldLicitacijaDokument, LicitacijaDokument newLicitacijaDokument)
        {
            context.Remove(oldLicitacijaDokument);
            newLicitacijaDokument.datumPodnosenjaDokumenta = DateTime.Now;
            context.dokumentiLicitacije.Add(newLicitacijaDokument);
        }
    }
}
