using ParcelaService.Entities;
using ParcelaService.Entities.Confirmations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Data
{
    public interface IDozvoljeniRadRepository
    {
        List<DozvoljeniRad> GetAll();
        DozvoljeniRad GetById(Guid dozvoljeniRadId);
        DozvoljeniRadConfirmation Create(DozvoljeniRad dozvoljeniRad);
        void Update(DozvoljeniRad dozvoljeniRad);
        void Delete(Guid dozvoljeniRadId);
        bool SaveChanges();
    }
}
