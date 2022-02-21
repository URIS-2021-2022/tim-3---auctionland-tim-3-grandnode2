using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zalba.Models;
using Zalba.Entities;

namespace Zalba.Data
{
    public interface IZalbaRepository
    {
        List<ZalbaE> GetZalbas();

        ZalbaE GetZalba(Guid zalbaId);

        ZalbaConfirmation CreateZalba(ZalbaE zalba);

        void UpdateZalba(ZalbaE zalba);

        void DeleteZalba(Guid zalbaId);

        bool SaveChanges();

    }
}
