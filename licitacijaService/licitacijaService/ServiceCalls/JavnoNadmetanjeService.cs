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

    public class JavnoNadmetanjeService : IJavnoNadmetanjeService
    {
        private readonly IConfiguration configuration;

        public JavnoNadmetanjeService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<JavnoNadmetanjeConfirmationDTO>> GetJavnaNadmetanjaByLicitacijaId(Guid licitacijaId)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{ configuration["Services:JavnoNadmetanjeService"] }JavnaNadmetanjaLicitacija/" + licitacijaId);
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
                    return JsonConvert.DeserializeObject<List<JavnoNadmetanjeConfirmationDTO>>(content);
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
