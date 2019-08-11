using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace Logger
{
    public class Serilogger : ILogger
    {
        private long logID;
        public Serilogger()
        {
            logID = DateTime.Now.Ticks;

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .WriteTo.File("logs\\ConsultaNovedadesMailApi_.log", rollingInterval: RollingInterval.Day, shared: true)
               .CreateLogger();
        }

        public void WriteLog(LogLevel level, string message)
        {
            string logMessage = "[" + logID + "]" + " : " + message;

            switch (level)
            {
                case LogLevel.DEBUG:
                    Log.Debug(logMessage);
                    break;
                case LogLevel.INFO:
                    Log.Information(logMessage);
                    break;
                case LogLevel.ERROR:
                    Log.Error(logMessage);
                    break;
                default:
                    break;
            }
        }
    }
}
