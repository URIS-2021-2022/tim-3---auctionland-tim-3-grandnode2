using KatastarskaOpstina.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.ServiceCalls
{
    public interface IParcelaService
    {
        public Task<List<ParcelaDto>> GetParceleByKatastarskaOpstinaID(Guid katastarskaOpstinaID);
    }
}
