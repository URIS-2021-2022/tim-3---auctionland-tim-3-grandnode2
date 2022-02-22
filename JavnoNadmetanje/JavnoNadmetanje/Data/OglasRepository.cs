using JavnoNadmetanje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class OglasRepository : IOglasRepository
    {
        private readonly JavnoNadmetanjeContext context;

        public OglasRepository(JavnoNadmetanjeContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<OglasEntity> GetOglasi()
        {
            return (from o in context.Oglasi select o).ToList();
        }

        public OglasEntity GetOglasById(Guid oglasId)
        {
            return context.Oglasi.FirstOrDefault(o => o.OglasId == oglasId);
        }

        public OglasEntity CreateOglas(OglasEntity oglas)
        {
            oglas.OglasId = Guid.NewGuid();
            context.Oglasi.Add(oglas);
            OglasEntity oglas1 = GetOglasById(oglas.OglasId);
            return oglas1;
        }

        public void UpdateOglas(OglasEntity oglas)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteOglas(Guid oglasId)
        {
            context.Oglasi.Remove(context.Oglasi.FirstOrDefault(o => o.OglasId == oglasId));
        }
    }
}
