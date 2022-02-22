using JavnoNadmetanje.Models.DokumentService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JavnoNadmetanje.ServiceCalls
{
    public class DokumentService : IDokumentService
    {
        private readonly IConfiguration configuration;

        public DokumentService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<ResponseDokumentDto> GetDokumentById(Guid dokumentId, string accessToken)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{ configuration["Services:DokumentService"] }api/Dokument/" + dokumentId);
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(content))
                    {
                        return default;
                    }
                    return JsonConvert.DeserializeObject<ResponseDokumentDto>(content);
                }
                return default;
            }
            catch
            {
                return default;
            }
        }
    }
}
