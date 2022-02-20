using ParcelaService.Entities;
using ParcelaService.Entities.Confirmations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Data
{
    public interface IKvalitetZemljistaRepository
    {
        List<KvalitetZemljista> GetAll();
        KvalitetZemljista GetById(Guid kvalitetZemljistaId);
        KvalitetZemljistaConfirmation Create(KvalitetZemljista kvalitetZemljista);
        void Update(KvalitetZemljista kvalitetZemljista);
        void Delete(Guid kvalitetZemljistaId);
        bool SaveChanges();
    }
}
