using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Entities;
using UgovorService.Models;

namespace UgovorService.Data
{
    public class UgovorRepository : IUgovorRepository
    {
        private readonly UgovorContext _context;
        private readonly IMapper _mapper;

        public UgovorRepository(UgovorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public List<Ugovor> GetAll()
        {
            return _context.Ugovori.ToList();
        }
        public Ugovor GetById(Guid ugovorId)
        {
            return _context.Ugovori.FirstOrDefault(e => e.UgovorId == ugovorId);
        }

        public UgovorConfirmation Create(Ugovor ugovor)
        {
            ugovor.UgovorId = Guid.NewGuid();
            var ugovorDodat = _context.Ugovori.Add(ugovor);
            return _mapper.Map<UgovorConfirmation>(ugovorDodat.Entity);
        }

        public void Update(Ugovor ugovor)
        {
            //Nije potrebna implementacija
        }

        public void Delete(Guid ugovorId)
        {
            var ugovor = GetById(ugovorId);
            _context.Remove(ugovor);
        }
    }
}
