using AutoMapper;
using ParcelaService.Entities;
using ParcelaService.Entities.Confirmations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Data
{
    public class ParcelaRepository : IParcelaRepository
    {
        private readonly ParcelaContext _context;
        private readonly IMapper _mapper;

        public ParcelaRepository(ParcelaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public List<Parcela> GetAll(Guid ?katastarskaOpstinaId)
        {
            return _context.Parcele.Where(e => (katastarskaOpstinaId == null || e.KatastarskaOpstinaId == katastarskaOpstinaId)).ToList();
        }

        public List<DeoParcele> GetDeloveParcele(Guid parcelaId)
        {
            return _context.DeloviParcele.Where(e => (parcelaId == null || e.ParcelaId == parcelaId)).ToList();
        }

        public Parcela GetById(Guid parcelaId)
        {
            return _context.Parcele.FirstOrDefault(e => e.ParcelaId == parcelaId);
        }

        public ParcelaConfirmation Create(Parcela parcela)
        {
            var parcelaDodata = _context.Parcele.Add(parcela);
            return _mapper.Map<ParcelaConfirmation>(parcelaDodata.Entity);
        }

        public void Update(Parcela parcela)
        {
            //Nije potrebna implementacija
        }

        public void Delete(Guid parcelaId)
        {
            var parcela = GetById(parcelaId);
            _context.Remove(parcela);
        }
    }
}
