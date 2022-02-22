using KatastarskaOpstina.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KatastarskaOpstina.ServiceCalls
{
    public class ParcelaService : IParcelaService
    {
        private readonly IConfiguration configuration;

        public ParcelaService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<ParcelaDto>> GetParceleByKatastarskaOpstinaID(Guid katastarskaOpstinaID)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{ configuration["Services:ParcelaService"] }api/parcele?katastarskaOpstinaId=" + katastarskaOpstinaID);
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
                    return JsonConvert.DeserializeObject<List<ParcelaDto>>(content);
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
