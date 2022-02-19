using licitacijaService.DTOs.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.Data
{
    public interface IJavnoNadmetanjeMockRepository
    {
        List<JavnoNadmetanjeDTO> GetJavnaNadmetanjaByLicitacijaId(Guid licitacijaId);
        void DeleteJvanoNadmetanjeByLicitacijaId(Guid idLicitacije);
    }
}
