using JavnoNadmetanje.Models.UplataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.ServiceCalls
{
    public interface IUplataService
    {
        public Task<List<UplataDto>> GetUplateByPrijavaZaNadmetanjeId(Guid prijavaZaNadmetanjeId);
    }
}
