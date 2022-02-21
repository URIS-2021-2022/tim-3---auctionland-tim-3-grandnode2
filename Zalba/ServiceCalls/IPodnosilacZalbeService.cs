using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zalba.Models;

namespace Zalba.ServiceCalls
{
     public interface IPodnosilacZalbeService
    {
        public Task<PodnosilacZalbeDto> GetPodnosilacZalbeById(Guid podnosilacZalbeId);
    }
}
