using AutoMapper;
using JavnoNadmetanje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class SluzbeniListRepository : ISluzbeniListRepository
    {
        private readonly JavnoNadmetanjeContext context;
        private readonly IMapper mapper;

        public SluzbeniListRepository(JavnoNadmetanjeContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<SluzbeniListEntity> GetSluzbeniListovi()
        {
            return (from s in context.SluzbeniListovi select s).ToList();
        }

        public SluzbeniListEntity GetSluzbeniListById(Guid sluzbeniListId)
        {
            return context.SluzbeniListovi.FirstOrDefault(s => s.SluzbeniListId == sluzbeniListId);
        }

        public SluzbeniListEntity CreateSluzbeniList(SluzbeniListEntity sluzbeniList)
        {
            sluzbeniList.SluzbeniListId = Guid.NewGuid();
            context.SluzbeniListovi.Add(sluzbeniList);
            SluzbeniListEntity sList = GetSluzbeniListById(sluzbeniList.SluzbeniListId);
            return sList;
        }

        public void UpdateSluzbeniList(SluzbeniListEntity sluzbeniList)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteSluzbeniList(Guid sluzbeniListId)
        {
            context.SluzbeniListovi.Remove(context.SluzbeniListovi.FirstOrDefault(s => s.SluzbeniListId == sluzbeniListId));
        }

    }
}
