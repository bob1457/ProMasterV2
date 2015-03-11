using System.Collections.Generic;
using ProMaster.Infrastructure.Logging;
using ProMaster.Infrastructure.Logging.ViewModels;


namespace ProMaster.DAL.Logging
{
    public interface ILogReportingRepository
    {
        IEnumerable<LoggingDashBoardViewModel> GetAllLogs();

        IEnumerable<Log4Net_Error> AllLogs();

        IEnumerable<LoggingDashBoardViewModel> GetLogsByLevel(string logLevel);

        IEnumerable<LoggingDashBoardViewModel> GetLogsByPeriod(string period);

        IEnumerable<LoggingDashBoardViewModel> GetLogsByLevelAndPeriod(string level, string period);


        Log4Net_Error GetLogDetails(int id);
    }
}
