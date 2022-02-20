using AutoMapper;
using ParcelaService.Entities;
using ParcelaService.Entities.Confirmations;
using ParcelaService.Models.ConfirmationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Data
{
    public class DeoParceleRepository : IDeoParceleRepository
    {
        private readonly ParcelaContext _context;
        private readonly IMapper _mapper;

        public DeoParceleRepository(ParcelaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public List<DeoParcele> GetAll(Guid ?parcelaId)
        {
            return _context.DeloviParcele.Where(e => (parcelaId == null || e.ParcelaId == parcelaId)).ToList();
        }
        public DeoParcele GetById(Guid deoParceleId)
        {
            return _context.DeloviParcele.FirstOrDefault(e => e.DeoParceleId == deoParceleId);
        }

        public DeoParceleConfirmation Create(DeoParcele deoParcele)
        {
            var deoParceleDodat = _context.DeloviParcele.Add(deoParcele);
            return _mapper.Map<DeoParceleConfirmation>(deoParceleDodat.Entity);
        }

        public void Update(DeoParcele deoParcele)
        {
            //Nije potrebna implementacija
        }

        public void Delete(Guid deoParceleId)
        {
            var deoParcele = GetById(deoParceleId);
            _context.Remove(deoParcele);
        }
    }
}
