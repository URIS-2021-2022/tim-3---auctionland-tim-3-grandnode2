using AutoMapper;
using ParcelaService.Entities;
using ParcelaService.Entities.Confirmations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Data
{
    public class KvalitetZemljistaRepository : IKvalitetZemljistaRepository
    {
        private readonly ParcelaContext _context;
        private readonly IMapper _mapper;

        public KvalitetZemljistaRepository(ParcelaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public List<KvalitetZemljista> GetAll()
        {
            return _context.KvalitetiZemljista.ToList();
        }
        public KvalitetZemljista GetById(Guid kvalitetZemljistaId)
        {
            return _context.KvalitetiZemljista.FirstOrDefault(e => e.KvalitetZemljistaId == kvalitetZemljistaId);
        }

        public KvalitetZemljistaConfirmation Create(KvalitetZemljista kvalitetZemljista)
        {
            var kvalitetZemljistaDodat = _context.KvalitetiZemljista.Add(kvalitetZemljista);
            return _mapper.Map<KvalitetZemljistaConfirmation>(kvalitetZemljistaDodat.Entity);
        }

        public void Update(KvalitetZemljista kvalitetZemljista)
        {
            //Nije potrebna implementacija
        }

        public void Delete(Guid kvalitetZemljistaId)
        {
            var kvalitetZemljista = GetById(kvalitetZemljistaId);
            _context.Remove(kvalitetZemljista);
        }
    }
}
