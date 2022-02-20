using AutoMapper;
using ParcelaService.Entities;
using ParcelaService.Entities.Confirmations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Data
{
    public class ZasticenaZonaRepository : IZasticenaZonaRepository
    {
        private readonly ParcelaContext _context;
        private readonly IMapper _mapper;

        public ZasticenaZonaRepository(ParcelaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public List<ZasticenaZona> GetAll()
        {
            return _context.ZasticeneZone.ToList();
        }
        public ZasticenaZona GetById(Guid zasticenaZonaId)
        {
            return _context.ZasticeneZone.FirstOrDefault(e => e.ZasticenaZonaId == zasticenaZonaId);
        }

        public ZasticenaZonaConfirmation Create(ZasticenaZona zasticenaZona)
        {
            var zasticenaZonaDodat = _context.ZasticeneZone.Add(zasticenaZona);
            return _mapper.Map<ZasticenaZonaConfirmation>(zasticenaZonaDodat.Entity);
        }

        public void Update(ZasticenaZona zasticenaZona)
        {
            //Nije potrebna implementacija
        }

        public void Delete(Guid zasticenaZonaId)
        {
            var zasticenaZona = GetById(zasticenaZonaId);
            _context.Remove(zasticenaZona);
        }
    }
}
