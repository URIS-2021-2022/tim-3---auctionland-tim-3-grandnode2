using ParcelaService.Entities;
using ParcelaService.Entities.Confirmations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Data
{
    public interface IParcelaRepository
    {
        List<Parcela> GetAll(Guid ?katastarskaOpstinaId);
        public List<DeoParcele> GetDeloveParcele(Guid parcelaId);
        Parcela GetById(Guid parcelaId);
        ParcelaConfirmation Create(Parcela parcela);
        void Update(Parcela parcela);
        void Delete(Guid parcelaId);
        bool SaveChanges();
    }
}
