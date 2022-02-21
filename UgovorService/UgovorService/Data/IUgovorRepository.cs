using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Entities;

namespace UgovorService.Data
{
    public interface IUgovorRepository
    {
        List<Ugovor> GetAll();
        Ugovor GetById(Guid ugovorId);
        UgovorConfirmation Create(Ugovor ugovor);
        void Update(Ugovor ugovor);
        void Delete(Guid ugovorId);
        bool SaveChanges();
    }
}
