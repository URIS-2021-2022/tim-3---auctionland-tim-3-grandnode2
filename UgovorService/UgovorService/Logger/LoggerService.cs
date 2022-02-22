using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UgovorService.Logger
{
    public class LoggerService
    {
        private readonly HttpClient httpClient;
        public LoggerService()
        {
            httpClient = new HttpClient();
        }

        public async Task PostLogger(string opis)
        {
            try
            {
                string loggerPath = "http://localhost:9903/api/Logger";
                LoggerVO loggerVO = new LoggerVO();
                loggerVO.Datum = DateTime.Now;
                loggerVO.OpisAktivnosti = opis;
                var logger = JsonSerializer.Serialize(loggerVO);
                string contentType = "application/json";
                var bodyRequest = new StringContent(logger, Encoding.UTF8, contentType);
                await httpClient.PostAsync(requestUri: loggerPath, bodyRequest);
            }
            catch
            {
                // catch
            }

        }
    }
}
