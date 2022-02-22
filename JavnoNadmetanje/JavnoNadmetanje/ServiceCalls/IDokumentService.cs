using JavnoNadmetanje.Models.DokumentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.ServiceCalls
{
    public interface IDokumentService
    {
        public Task<ResponseDokumentDto> GetDokumentById(Guid dokumentId, string accessToken);
    }
}
