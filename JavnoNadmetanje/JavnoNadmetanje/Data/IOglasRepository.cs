using JavnoNadmetanje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public interface IOglasRepository
    {
        List<OglasEntity> GetOglasi();

        OglasEntity GetOglasById(Guid oglasId);

        OglasEntity CreateOglas(OglasEntity oglas);

        OglasEntity UpdateOglas(OglasEntity oglas);

        void DeleteOglas(Guid oglasId);
    }
}
