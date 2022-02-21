using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Entities;

namespace Uplata.Data
{
    public interface IBankaRepository
    {
        List<BankaEntity> GetBanke();

        BankaEntity GetBankaById(Guid bankaId);

        BankaEntity CreateBanka(BankaEntity banka);

        void UpdateBanka(BankaEntity banka);

        void DeleteBanka(Guid bankaId);

        bool SaveChanges();
    }
}
