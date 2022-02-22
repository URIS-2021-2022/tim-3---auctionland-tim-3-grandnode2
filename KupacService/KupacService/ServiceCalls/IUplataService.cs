using KupacService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.ServiceCalls
{
    public interface IUplataService
    {
        public Task<List<UplataDto>> GetUplateByKupacId(Guid kupacId);
    }
}
