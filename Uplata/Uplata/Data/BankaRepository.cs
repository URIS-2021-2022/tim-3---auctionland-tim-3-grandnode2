using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Entities;

namespace Uplata.Data
{
    public class BankaRepository : IBankaRepository
    {
        private readonly UplataContext context;

        public BankaRepository(UplataContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<BankaEntity> GetBanke()
        {
            return (from b in context.Banke select b).ToList();
        }

        public BankaEntity GetBankaById(Guid bankaId)
        {
            return context.Banke.FirstOrDefault(b => b.BankaId == bankaId);
        }

        public BankaEntity CreateBanka(BankaEntity banka)
        {
            banka.BankaId = Guid.NewGuid();
            context.Banke.Add(banka);
            BankaEntity banka1 = GetBankaById(banka.BankaId);
            return banka1;
        }

        public void UpdateBanka(BankaEntity banka)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }
        public void DeleteBanka(Guid bankaId)
        {
            context.Banke.Remove(context.Banke.FirstOrDefault(b => b.BankaId == bankaId));
        }
    }
}
