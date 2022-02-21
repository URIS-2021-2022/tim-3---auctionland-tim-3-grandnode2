using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Entities;

namespace Uplata.Data
{
    public interface IUplataRepository
    {
        List<UplataEntity> GetUplate();

        UplataEntity GetUplataById(Guid uplataId);

        List<UplataEntity> GetUplateByPrijavaZaNadmetanjeId(Guid prijavaZaNadmetanjeId);

        UplataEntity CreateUplata(UplataEntity uplata);

        void UpdateUplata(UplataEntity uplata);

        void DeleteUplata(Guid uplataId);

        bool SaveChanges();
    }
}
