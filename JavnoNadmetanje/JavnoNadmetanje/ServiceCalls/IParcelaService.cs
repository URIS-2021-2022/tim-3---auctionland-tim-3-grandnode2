using JavnoNadmetanje.Models.ParcelaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.ServiceCalls
{
    public interface IParcelaService
    {
        public Task<List<DeoParceleDto>> GetDeloveParcele(Guid parcelaId);
    }
}
