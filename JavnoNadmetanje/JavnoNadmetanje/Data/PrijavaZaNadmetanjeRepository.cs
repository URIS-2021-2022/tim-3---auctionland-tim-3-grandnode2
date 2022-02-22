using AutoMapper;
using JavnoNadmetanje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class PrijavaZaNadmetanjeRepository : IPrijavaZaNadmetanjeRepository
    {
        private readonly JavnoNadmetanjeContext context;

        public PrijavaZaNadmetanjeRepository(JavnoNadmetanjeContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<PrijavaZaNadmetanjeEntity> GetPrijaveZaNadmetanje()
        {
            return (from p in context.PrijaveZaNadmetanje select p).ToList();
        }

        public PrijavaZaNadmetanjeEntity GetPrijavaZaNadmetanjeById(Guid prijavaZaNadmetanjeId)
        {
            return context.PrijaveZaNadmetanje.FirstOrDefault(p => p.PrijavaZaNadmetanjeId == prijavaZaNadmetanjeId);
        }

        public PrijavaZaNadmetanjeEntity CreatePrijavaZaNadmetanje(PrijavaZaNadmetanjeEntity prijavaZaNadmetanje)
        {
            prijavaZaNadmetanje.PrijavaZaNadmetanjeId= Guid.NewGuid();
            context.PrijaveZaNadmetanje.Add(prijavaZaNadmetanje);
            PrijavaZaNadmetanjeEntity prijavaNadmetanje = GetPrijavaZaNadmetanjeById(prijavaZaNadmetanje.PrijavaZaNadmetanjeId);
            return prijavaNadmetanje;
        }

        public void UpdatePrijavaZaNadmetanje(PrijavaZaNadmetanjeEntity prijavaZaNadmetanje)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeletePrijavaZaNadmetanje(Guid prijavaZaNadmetanjeId)
        {
            context.PrijaveZaNadmetanje.Remove(context.PrijaveZaNadmetanje.FirstOrDefault(p => p.PrijavaZaNadmetanjeId== prijavaZaNadmetanjeId));
        }

    }
}
