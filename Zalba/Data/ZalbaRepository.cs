using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zalba.Entities;

namespace Zalba.Data
{
    public class ZalbaRepository : IZalbaRepository
    {
        private readonly ZalbaContext context;
        private readonly IMapper mapper;

        public ZalbaRepository(ZalbaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<ZalbaE> GetZalbas()
        {
            return context.Zalbas.ToList();
        }

        public ZalbaE GetZalba(Guid zalbaId)
        {
            return context.Zalbas.FirstOrDefault(e => e.ZalbaID == zalbaId);
        }

        public ZalbaConfirmation CreateZalba(ZalbaE zalba)
        {
            zalba.ZalbaID = Guid.NewGuid();
            var createdEntity = context.Zalbas.Add(zalba);
            return mapper.Map<ZalbaConfirmation>(createdEntity.Entity);
        }

        public void UpdateZalba(ZalbaE zalba)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteZalba(Guid zalbaId)
        {
            var zalba = GetZalba(zalbaId);
            context.Remove(zalba);
        }
    }
}
