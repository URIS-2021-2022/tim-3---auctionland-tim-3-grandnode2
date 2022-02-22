using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UgovorService.Models;

namespace UgovorService.ServiceCalls
{
    public class LiceService : ILiceService
    {
        private readonly IConfiguration _configuration;

        public LiceService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<LiceDto> GetLiceById(Guid liceId)
        {
             try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{ _configuration["Services:KupacService"] }/api/Kupci/" + liceId); //Treba srediti
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
                    return JsonConvert.DeserializeObject<LiceDto>(content);
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
