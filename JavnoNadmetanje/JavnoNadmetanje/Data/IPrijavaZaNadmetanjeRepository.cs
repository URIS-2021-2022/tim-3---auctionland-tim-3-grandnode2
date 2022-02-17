using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public interface IPrijavaZaNadmetanjeRepository
    {
        List<PrijavaZaNadmetanjeModel> GetPrijaveZaNadmetanje();

        PrijavaZaNadmetanjeModel GetPrijavaZaNadmetanjeById(Guid prijavaZaNadmetanjeId);

        PrijavaZaNadmetanjeModel CreatePrijavaZaNadmetanje(PrijavaZaNadmetanjeModel prijavaZaNadmetanje);

        PrijavaZaNadmetanjeModel UpdatePrijavaZaNadmetanje(PrijavaZaNadmetanjeModel prijavaZaNadmetanje);

        void DeletePrijavaZaNadmetanje(Guid prijavaZaNadmetanjeId);
    }
}
