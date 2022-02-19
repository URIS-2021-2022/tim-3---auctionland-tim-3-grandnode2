using licitacijaService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.Data
{
    public interface ILicitacijaDokumentRepository
    {
        List<LicitacijaDokument> GetDokumnetByLicitacijaId(Guid idLicitacije);

        LicitacijaDokument GetDokumnetById(Guid idLicitacije, Guid dokumentId);
        List<LicitacijaDokument> GetDokumnetByLicitacijaIdAndVrstaPodnosioca(Guid idLicitacije, string indikatorVrste);
        void CreateLicitacijaDokument(LicitacijaDokument licitacijaDokument);
        void UpdateLicitacijaDokument(LicitacijaDokument oldLicitacijaDokument, LicitacijaDokument newLicitacijaDokument);
        void DeleteLicitacijaDokumentByLicitacijaId(Guid idLicitacije);
        bool SaveChanges();
    }
}
