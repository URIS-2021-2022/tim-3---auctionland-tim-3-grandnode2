using ParcelaService.Entities;
using ParcelaService.Entities.Confirmations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Data
{
    public interface IDeoParceleRepository
    {
        List<DeoParcele> GetAll(Guid ?parcelaId);
        DeoParcele GetById(Guid deoParceleId);
        DeoParceleConfirmation Create(DeoParcele deoParcele);
        void Update(DeoParcele deoParcele);
        void Delete(Guid deoParceleId);
        bool SaveChanges();
    }
}
