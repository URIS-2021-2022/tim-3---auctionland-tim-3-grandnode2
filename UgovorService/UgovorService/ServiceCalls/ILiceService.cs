using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Models;

namespace UgovorService.ServiceCalls
{
    public interface ILiceService
    {
        public Task<LiceDto> GetLiceById(Guid liceId);
    }
}
