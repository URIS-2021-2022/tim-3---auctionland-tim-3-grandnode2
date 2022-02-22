using KupacService.DBContexts;
using KupacService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.Data
{
    public class FizickoLiceRepository : IFizickoLiceRepository
    {
        private readonly KupacContext context;

        public FizickoLiceRepository(KupacContext context)
        {
            this.context = context;
        }

        public void CreateFizickoLice(FizickoLice fizickoLice)
        {
            fizickoLice.FizickoLiceId = Guid.NewGuid();
            context.FizickoLice.Add(fizickoLice);
        }

        public void DeleteFizickoLice(Guid id)
        {
            var fizickoLice = GetFizickoLiceById(id);
            if (fizickoLice != null)
            {
                context.Remove(fizickoLice);
            }
        }

        public FizickoLice GetFizickoLiceById(Guid id)
        {
            return context.FizickoLice.FirstOrDefault(fl => fl.FizickoLiceId.Equals(id));
        }

        public List<FizickoLice> GetFizickaLica()
        {
            return context.FizickoLice.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateFizickoLice(FizickoLice oldFizickoLice, FizickoLice newFizickoLice)
        {

            oldFizickoLice.Ime = newFizickoLice.Ime;
            oldFizickoLice.Prezime = newFizickoLice.Prezime;
            oldFizickoLice.Jmbg = newFizickoLice.Jmbg;
            oldFizickoLice.AdresaId = newFizickoLice.AdresaId;
            oldFizickoLice.BrojTelefona_1 = newFizickoLice.BrojTelefona_1;
            oldFizickoLice.BrojTelefona_2 = newFizickoLice.BrojTelefona_2;
            oldFizickoLice.Email = newFizickoLice.Email;
            oldFizickoLice.BrojRacuna = newFizickoLice.BrojRacuna;
        }
    }
}
