using KupacService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.Data
{
    public interface IPravnoLiceRepository
    {
        List<PravnoLice> GetPravnaLica();
        PravnoLice GetPravnoLiceById(Guid id);
        void CreatePravnoLice(PravnoLice pravnoLice);
        void UpdatePravnoLice(PravnoLice oldPravnoLice, PravnoLice newPravnoLice);
        void DeletePravnoLice(Guid id);
        bool SaveChanges();
    }
}
