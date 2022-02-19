using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace licitacijaService.Logger
{
    public class LoggerMockReposiotry : ILoggerMockReposiotry
    {
        public void Log(LogLevel logLevel, string requestId, string previousRequestId, string message, Exception exception)
        {
            Task.Run(() =>
            {
                Thread.Sleep(500);
            });
        }
    }
}
