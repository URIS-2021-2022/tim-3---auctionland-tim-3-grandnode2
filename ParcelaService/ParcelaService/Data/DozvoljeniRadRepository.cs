using AutoMapper;
using ParcelaService.Entities;
using ParcelaService.Entities.Confirmations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Data
{
    public class DozvoljeniRadRepository : IDozvoljeniRadRepository
    {
        private readonly ParcelaContext _context;
        private readonly IMapper _mapper;

        public DozvoljeniRadRepository(ParcelaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public List<DozvoljeniRad> GetAll()
        {
            return _context.DozvoljeniRadovi.ToList();
        }
        public DozvoljeniRad GetById(Guid dozvoljeniRadId)
        {
            return _context.DozvoljeniRadovi.FirstOrDefault(e => e.DozvoljeniRadId == dozvoljeniRadId);
        }

        public DozvoljeniRadConfirmation Create(DozvoljeniRad dozvoljeniRad)
        {
            var radDodat = _context.DozvoljeniRadovi.Add(dozvoljeniRad);
            return _mapper.Map<DozvoljeniRadConfirmation>(radDodat.Entity);
        }

        public void Update(DozvoljeniRad dozvoljeniRad)
        {
            //Nije potrebna implementacija
        }

        public void Delete(Guid dozvoljeniRadId)
        {
            var dozvoljeniRad = GetById(dozvoljeniRadId);
            _context.Remove(dozvoljeniRad);
        }
    }
}
