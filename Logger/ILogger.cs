using System;

namespace Logger
{
    public interface ILogger
    {
        void WriteLog(LogLevel level, string message);
    }
}
