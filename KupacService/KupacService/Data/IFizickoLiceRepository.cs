using KupacService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.Data
{
    public interface IFizickoLiceRepository
    {
        List<FizickoLice> GetFizickaLica();
        FizickoLice GetFizickoLiceById(Guid id);
        List<Guid> GetOvlascenaLicaByKupacId(Guid kupacId);
        void CreateFizickoLice(FizickoLice fizickoLice);
        void UpdateFizickoLice(FizickoLice oldFizickoLice, FizickoLice newFizickoLice);
        void DeleteFizickoLice(Guid id);
        bool SaveChanges();
    }
}
