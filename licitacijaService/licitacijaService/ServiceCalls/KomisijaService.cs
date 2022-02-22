using licitacijaService.DTOs.Mock;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace licitacijaService.ServiceCalls
{
    public class KomisijaService : IKomisijaService
    {
        private readonly IConfiguration configuration;

        public KomisijaService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<KomisijaConfirmationDto>> GetKomisijaByOznaka(string oznakaKomisije)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{ configuration["Services:KomisijaService"] }api/komisije?oznakaKomisije=" + oznakaKomisije);
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(content))
                    {
                        return default;
                    }
                    return JsonConvert.DeserializeObject<List<KomisijaConfirmationDto>>(content);
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
