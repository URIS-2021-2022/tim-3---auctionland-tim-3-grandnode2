using AutoMapper;
using KatastarskaOpstina.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Data
{
    public class KatastarskaOpstinaRepository : IKatastarskaOpstinaRepository
    {
        private readonly KatastarskaOpstinaContext context;
        private readonly IMapper mapper;

        public KatastarskaOpstinaRepository(KatastarskaOpstinaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<KatastarskaOpstinaE> GetKatastarskaOpstinas()
        {
            return context.KatastarskaOpstinas.ToList();
        }

        public KatastarskaOpstinaE GetKatastarskaOpstinaById(Guid katastarskaOpstinaId)
        {
            return context.KatastarskaOpstinas.FirstOrDefault(e => e.KatastarskaOpstinaID == katastarskaOpstinaId);
        }

        public KatastarskaOpstinaConfirmation CreateKatastarskaOpstina(KatastarskaOpstinaE katastarskaOpstina)
        {
            katastarskaOpstina.KatastarskaOpstinaID = Guid.NewGuid();
            var createdEntity = context.KatastarskaOpstinas.Add(katastarskaOpstina);
            return mapper.Map<KatastarskaOpstinaConfirmation>(createdEntity.Entity);
        }

        public void UpdateKatastarskaOpstina(KatastarskaOpstinaE katastarskaOpstina)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteKatastarskaOpstina(Guid katastarskaOpstinaId)
        {
            var katastarskaOpstina = GetKatastarskaOpstinaById(katastarskaOpstinaId);
            context.KatastarskaOpstinas.Remove(katastarskaOpstina);
        }

        
    }
}
