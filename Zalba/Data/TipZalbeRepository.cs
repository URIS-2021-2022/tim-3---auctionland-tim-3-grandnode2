using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zalba.Entities;

namespace Zalba.Data
{
    public class TipZalbeRepository : ITipZalbeRepository
    {
        private readonly ZalbaContext context;
        private readonly IMapper mapper;

        public TipZalbeRepository(ZalbaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<TipZalbeE> GetTipZalbes()
        {
            return context.TipZalbes.ToList();
        }

        public TipZalbeE GetTipZalbe(Guid tipZalbeId)
        {
            return context.TipZalbes.FirstOrDefault(e => e.TipZalbeID == tipZalbeId);
        }

        public TipZalbeConfirmation CreateTipZalbe(TipZalbeE tipZalbe)
        {
            tipZalbe.TipZalbeID = Guid.NewGuid();
            var createdEntity = context.TipZalbes.Add(tipZalbe);
            return mapper.Map<TipZalbeConfirmation>(createdEntity.Entity);
        }

        public void UpdateTipZalbe(TipZalbeE tipZalbe)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteTipZalbe(Guid tipZalbeId)
        {
            var tipZalbe = GetTipZalbe(tipZalbeId);
            context.Remove(tipZalbe);
        }
    }
}
