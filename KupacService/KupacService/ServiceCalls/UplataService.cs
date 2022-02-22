using KupacService.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KupacService.ServiceCalls
{
    public class UplataService : IUplataService
    {
        private readonly IConfiguration configuration;

        public UplataService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<UplataDto>> GetUplateByKupacId(Guid kupacId)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{ configuration["Services:UplataService"] }api/Uplate/" + kupacId);
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
                    return JsonConvert.DeserializeObject<List<UplataDto>>(content);
                }
                return default;

            } catch
            {
                return default;
            }
        }
    }
}
