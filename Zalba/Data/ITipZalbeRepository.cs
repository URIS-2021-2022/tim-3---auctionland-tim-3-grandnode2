using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zalba.Entities;

namespace Zalba.Data
{
    public interface ITipZalbeRepository
    {
        List<TipZalbeE> GetTipZalbes();

        TipZalbeE GetTipZalbe(Guid tipZalbeId);

        TipZalbeConfirmation CreateTipZalbe(TipZalbeE tipZalbe);

        void UpdateTipZalbe(TipZalbeE tipZalbe);

        void DeleteTipZalbe(Guid tipZalbeId);

        bool SaveChanges();
    }
}
