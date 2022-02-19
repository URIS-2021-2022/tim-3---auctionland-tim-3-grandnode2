using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.Logger
{
    public interface ILoggerMockReposiotry
    {
        void Log(LogLevel logLevel, string requestId, string previousRequestId, string message, Exception exception);
    }
}
