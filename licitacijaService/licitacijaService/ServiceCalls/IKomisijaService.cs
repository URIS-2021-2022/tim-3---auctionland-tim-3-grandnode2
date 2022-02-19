using licitacijaService.DTOs.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.ServiceCalls
{
    public interface IKomisijaService
    {
        public Task<List<KomisijaConfirmationDTO>> GetKomisijaByOznaka(string oznakaKomisije);
    }
}
