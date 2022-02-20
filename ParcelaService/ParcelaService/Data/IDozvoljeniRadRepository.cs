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
        DozvoljeniRad GetById(Guid dozvoljeniRadoviId);
        DozvoljeniRadConfirmation Create(DozvoljeniRad dozvoljeniRadovi);
        void Update(DozvoljeniRad dozvoljeniRadovi);
        void Delete(Guid dozvoljeniRadoviId);
        bool SaveChanges();
    }
}
