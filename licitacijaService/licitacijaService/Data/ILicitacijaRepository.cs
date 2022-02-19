using licitacijaService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.Data
{
    public interface ILicitacijaRepository
    {
        List<Licitacija> GetLicitacije(string brojLicitacije = null);
        Licitacija GetLicitacijaById(Guid id);
        void CreateLicitacija (Licitacija licitacija);
        void UpdateLicitacija(Licitacija oldLicitacija, Licitacija newLicitacija);
        void DeleteLicitacija(Guid id);
        bool SaveChanges();
    }
}
