using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public interface ISluzbeniListRepository
    {
        List<SluzbeniListModel> GetSluzbeniListovi();

        SluzbeniListModel GetSluzbeniListById(Guid sluzbeniListId);

        SluzbeniListModel CreateSluzbeniList(SluzbeniListModel sluzbeniList);

        SluzbeniListModel UpdateSluzbeniList(SluzbeniListModel sluzbeniList);

        void DeleteSluzbeniList(Guid sluzbeniListId);
    }
}
