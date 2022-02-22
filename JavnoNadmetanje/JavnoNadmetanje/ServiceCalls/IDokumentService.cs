using JavnoNadmetanje.Models.DokumentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.ServiceCalls
{
    public interface IDokumentService
    {
        public Task<DokumentDto> GetDokumentById(Guid dokumentId, string accessToken);
    }
}
