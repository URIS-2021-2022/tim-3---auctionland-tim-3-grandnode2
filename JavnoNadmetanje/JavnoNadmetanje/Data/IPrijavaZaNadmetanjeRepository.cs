using JavnoNadmetanje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public interface IPrijavaZaNadmetanjeRepository
    {
        List<PrijavaZaNadmetanjeEntity> GetPrijaveZaNadmetanje();

        PrijavaZaNadmetanjeEntity GetPrijavaZaNadmetanjeById(Guid prijavaZaNadmetanjeId);

        PrijavaZaNadmetanjeEntity CreatePrijavaZaNadmetanje(PrijavaZaNadmetanjeEntity prijavaZaNadmetanje);

        void UpdatePrijavaZaNadmetanje(PrijavaZaNadmetanjeEntity prijavaZaNadmetanje);

        void DeletePrijavaZaNadmetanje(Guid prijavaZaNadmetanjeId);

        bool SaveChanges();
    }
}
