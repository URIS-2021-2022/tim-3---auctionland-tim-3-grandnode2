using ParcelaService.Entities;
using ParcelaService.Entities.Confirmations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Data
{
    public interface IZasticenaZonaRepository
    {
        List<ZasticenaZona> GetAll();
        ZasticenaZona GetById(Guid zasticenaZonaId);
        ZasticenaZonaConfirmation Create(ZasticenaZona zasticenaZona);
        void Update(ZasticenaZona zasticenaZona);
        void Delete(Guid zasticenaZonaId);
        bool SaveChanges();
    }
}
