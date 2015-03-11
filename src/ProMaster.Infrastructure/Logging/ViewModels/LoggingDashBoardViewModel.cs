using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProMaster.Infrastructure.Logging.ViewModels
{
    public class LoggingDashBoardViewModel
    {
        public int LogId { get; set; }
        public string LoggerProviderName { get; set; }
        public string LogLevel { get; set; }
        public string LogMessage { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogThread { get; set; }
        public string LogException { get; set; }
        public IEnumerable<LoggingDashBoardViewModel> LogList { get; set; }
        public int NumberOfPages { get; set; }

    }
}
