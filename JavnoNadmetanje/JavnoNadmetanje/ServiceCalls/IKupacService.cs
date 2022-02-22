using JavnoNadmetanje.Models.KupacService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.ServiceCalls
{
    public interface IKupacService
    {
        public Task<KupacDto> GetKupacById(Guid kupacId);
    }
}
