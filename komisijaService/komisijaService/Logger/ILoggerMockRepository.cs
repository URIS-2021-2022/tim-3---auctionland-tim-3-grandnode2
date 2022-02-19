using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace komisijaService.Logger
{
    public interface ILoggerMockRepository
    {
        void Log(LogLevel logLevel, string requestId, string previousRequestId, string message, Exception exception);
    }
}
