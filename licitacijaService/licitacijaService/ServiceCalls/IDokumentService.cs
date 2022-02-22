using licitacijaService.DTOs.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.ServiceCalls
{
    public interface IDokumentService
    {

        public Task<ResponseDokument> GetDokumentByDokumentId(Guid dokumentId, string accessToken);
    }
}
