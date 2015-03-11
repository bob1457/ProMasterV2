using System;
using System.Collections.Generic;
using System.Linq;
using ProMaster.Infrastructure.Logging.ViewModels;
using ProMaster.DAL.Logging;
using ProMaster.Infrastructure.Logging;

namespace ProMaster.Repository.Logging
{
    public class LogReportingReposioty : ILogReportingRepository
    {
        ProMasterLogReportingDataEntities entities = new ProMasterLogReportingDataEntities();

        public IEnumerable<LoggingDashBoardViewModel> GetAllLogs()
        {
            //throw new NotImplementedException();
            var result = from logs in entities.Log4Net_Error
                         select new LoggingDashBoardViewModel
                         {
                             LogId = logs.Id,
                             LogDateTime = logs.Date,
                             LogThread = logs.Thread,
                             LogLevel = logs.Level,
                             LogException = logs.Exception,
                             LogMessage = logs.Message                
            
                         };
            return result;
        }

        public IEnumerable<LoggingDashBoardViewModel> GetLogsByLevel(string logLevel)
        {
            //throw new NotImplementedException();
            var result = from logs in entities.Log4Net_Error
                         where logs.Level == logLevel
                         select new LoggingDashBoardViewModel
                         {
                             LogId = logs.Id,
                             LogDateTime = logs.Date,
                             LogThread = logs.Thread,
                             LogLevel = logs.Level,
                             LogException = logs.Exception,
                             LogMessage = logs.Message

                         };
            return result;
        }


        public IEnumerable<Log4Net_Error> AllLogs()
        {
            //throw new NotImplementedException();
            return entities.Log4Net_Error;
        }


        public Log4Net_Error GetLogDetails(int id)
        {
            //throw new NotImplementedException();
            //var result = from log in entities.Log4Net_Error

            return entities.Log4Net_Error.Where(l => l.Id == id).FirstOrDefault();
                         
        }


        public IEnumerable<LoggingDashBoardViewModel> GetLogsByPeriod(string period)
        {
            //throw new NotImplementedException();
            var date = DateTime.Now;
            
            switch (period)
            {                
                case "Last 7 Days":
                    date = DateTime.Now.AddDays(-7).Date;
                    break;
                case "Last 14 Days":
                    date = DateTime.Now.AddDays(-14).Date;
                    break;
                case "Last 30 Days":
                    date = DateTime.Now.AddDays(-30).Date;
                    break;
                case "Last 180 Days":
                    date = DateTime.Now.AddDays(-180).Date;
                    break;
            }
            
            var result = from logs in entities.Log4Net_Error
                         where logs.Date > date
                         select new LoggingDashBoardViewModel
                         {
                             LogId = logs.Id,
                             LogDateTime = logs.Date,
                             LogThread = logs.Thread,
                             LogLevel = logs.Level,
                             LogException = logs.Exception,
                             LogMessage = logs.Message
                         };
            return result;

        }


        public IEnumerable<LoggingDashBoardViewModel> GetLogsByLevelAndPeriod(string level, string period)
        {
            //throw new NotImplementedException();

            var date = DateTime.Now;
            
            switch (period)
            {                
                case "Last 7 Days":
                    date = DateTime.Now.AddDays(-7).Date;
                    break;
                case "Last 14 Days":
                    date = DateTime.Now.AddDays(-14).Date;
                    break;
                case "Last 30 Days":
                    date = DateTime.Now.AddDays(-30).Date;
                    break;
                case "Last 180 Days":
                    date = DateTime.Now.AddDays(-180).Date;
                    break;
            }


            var result = from logs in entities.Log4Net_Error
                         where logs.Level == level && logs.Date > date
                         select new LoggingDashBoardViewModel
                         {
                             LogId = logs.Id,
                             LogDateTime = logs.Date,
                             LogThread = logs.Thread,
                             LogLevel = logs.Level,
                             LogException = logs.Exception,
                             LogMessage = logs.Message

                         };
            return result;

        }








    }
}
