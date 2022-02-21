using JavnoNadmetanje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public interface ISluzbeniListRepository
    {
        List<SluzbeniListEntity> GetSluzbeniListovi();

        SluzbeniListEntity GetSluzbeniListById(Guid sluzbeniListId);

        SluzbeniListEntity CreateSluzbeniList(SluzbeniListEntity sluzbeniList);

        void UpdateSluzbeniList(SluzbeniListEntity sluzbeniList);

        void DeleteSluzbeniList(Guid sluzbeniListId);

        bool SaveChanges();
    }
}
