using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Entities;

namespace Uplata.Data
{
    public class UplataRepository : IUplataRepository
    {
        private readonly UplataContext context;
        private readonly IMapper mapper;

        public UplataRepository(UplataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<UplataEntity> GetUplate()
        {
            return (from u in context.Uplate select u).ToList();
        }

        public UplataEntity GetUplataById(Guid uplataId)
        {
            return context.Uplate.FirstOrDefault(u => u.UplataId == uplataId);
        }

        public List<UplataEntity> GetUplateByPrijavaZaNadmetanjeId(Guid prijavaZaNadmetanjeId)
        {
            return context.Uplate.Where(u => (prijavaZaNadmetanjeId == null || u.PrijavaZaNadmetanjeId == prijavaZaNadmetanjeId)).ToList();
        }

        public UplataEntity CreateUplata(UplataEntity uplata)
        {
            uplata.UplataId = Guid.NewGuid();
            context.Uplate.Add(uplata);
            UplataEntity uplata1 = GetUplataById(uplata.UplataId);
            return uplata1;
        }

        public void UpdateUplata(UplataEntity uplata)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }
        public void DeleteUplata(Guid uplataId)
        {
            context.Uplate.Remove(context.Uplate.FirstOrDefault(u => u.UplataId == uplataId));
        }
    }
}
