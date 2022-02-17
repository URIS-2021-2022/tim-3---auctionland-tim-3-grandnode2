using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public interface IOglasRepository
    {
        List<OglasModel> GetOglasi();

        OglasModel GetOglasById(Guid oglasId);

        OglasModel CreateOglas(OglasModel oglas);

        OglasModel UpdateOglas(OglasModel oglas);

        void DeleteOglas(Guid oglasId);
    }
}
