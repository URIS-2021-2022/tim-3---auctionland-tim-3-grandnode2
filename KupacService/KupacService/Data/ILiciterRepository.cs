using KupacService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.Data
{
    interface ILiciterRepository
    {
        List<Liciter> GetLiciteri();
        Liciter GetLiciterById(Guid id);
        void CreateLiciter(Liciter liciter);
        void UpdateLiciter(Liciter oldLiciter, Liciter newLiciter);
        void DeleteLiciter(Guid id);
        bool SaveChanges();
    }
}
