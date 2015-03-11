using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;

namespace ProMaster.Infrastructure.Logging
{
    public class Log4NetLogger : ILogger
    {
        private ILog _logger;

        public Log4NetLogger()
        {
            _logger = LogManager.GetLogger(this.GetType());
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception ex)
        {
            Error(LogUtility.BuildExceptionMessage(ex));
        }

        public void Error(string message, Exception ex)
        {
            _logger.Error(message, ex);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception ex)
        {
            Fatal(LogUtility.BuildExceptionMessage(ex));
        }
    }
}
