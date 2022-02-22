using licitacijaService.DTOs.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.ServiceCalls
{
    public interface IJavnoNadmetanjeService
    {
        public Task<List<JavnoNadmetanjeConfirmationDto>> GetJavnaNadmetanjaByLicitacijaId(Guid licitacijaId);
    }
}
