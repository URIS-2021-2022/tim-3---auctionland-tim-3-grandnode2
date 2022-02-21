using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Models;

namespace UgovorService.ServiceCalls
{
    public interface ILicitacijaService
    {
        public Task<LicitacijaDto> GetLicitacijaById(Guid licitacijaId);
    }
}
