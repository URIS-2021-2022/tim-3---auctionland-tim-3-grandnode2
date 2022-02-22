using JavnoNadmetanje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class JavnoNadmetanjeRepository : IJavnoNadmetanjeRepository
    {
        private readonly JavnoNadmetanjeContext context;

        public JavnoNadmetanjeRepository(JavnoNadmetanjeContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<JavnoNadmetanjeEntity> GetJavnaNadmetanja()
        {
            return (from j in context.JavnaNadmetanja select j).ToList();
        }

        public JavnoNadmetanjeEntity GetJavnoNadmetanjeById(Guid javnoNadmetanjeId)
        {
            return context.JavnaNadmetanja.FirstOrDefault(j => j.JavnoNadmetanjeId == javnoNadmetanjeId);
        }

        public List<JavnoNadmetanjeEntity> GetJavnaNadmetanjaByLicitacijaId(Guid licitacijaId)
        {
            return context.JavnaNadmetanja.Where(j => (j.LicitacijaId == licitacijaId)).ToList();
        }

        public JavnoNadmetanjeEntity CreateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje)
        {
            javnoNadmetanje.JavnoNadmetanjeId = Guid.NewGuid();
            context.JavnaNadmetanja.Add(javnoNadmetanje);
            JavnoNadmetanjeEntity jNadmetanje = GetJavnoNadmetanjeById(javnoNadmetanje.JavnoNadmetanjeId);
            return jNadmetanje;
        }

        public void UpdateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje)
        {
           //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
           //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteJavnoNadmetanje(Guid javnoNadmetanjeId)
        {
            context.JavnaNadmetanja.Remove(context.JavnaNadmetanja.FirstOrDefault(j => j.JavnoNadmetanjeId == javnoNadmetanjeId));
        }

       
    }
}
