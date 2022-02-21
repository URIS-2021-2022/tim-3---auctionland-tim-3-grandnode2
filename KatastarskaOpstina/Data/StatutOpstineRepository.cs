using AutoMapper;
using KatastarskaOpstina.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Data
{
    public class StatutOpstineRepository : IStatutOpstineRepository
    {
        private readonly KatastarskaOpstinaContext context;
        private readonly IMapper mapper;

        public StatutOpstineRepository(KatastarskaOpstinaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<StatutOpstineE> GetStatutOpstines()
        {
            return context.StatutOpstines.ToList();
        }

        public StatutOpstineE GetStatutOpstine(Guid statutOpstineId)
        {
            return context.StatutOpstines.FirstOrDefault(e => e.StatutOpstineID == statutOpstineId);
        }

        public StatutOpstineConfirmation CreateStatutOpstine(StatutOpstineE statutOpstine)
        {
            statutOpstine.StatutOpstineID = Guid.NewGuid();
            var createdEntity = context.StatutOpstines.Add(statutOpstine);
            return mapper.Map<StatutOpstineConfirmation>(createdEntity.Entity);
        }

        public void UpdateStatutOpstine(StatutOpstineE statutOpstine)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteStatutOpstine(Guid statutOpstineId)
        {
            var statutOpstine = GetStatutOpstine(statutOpstineId);
            context.StatutOpstines.Remove(statutOpstine);
        }



    }
}
