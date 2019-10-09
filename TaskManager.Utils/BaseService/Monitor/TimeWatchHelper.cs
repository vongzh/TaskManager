using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Utils.BaseService.Monitor.Model;
using TaskManager.Utils.BaseService.Monitor.SystemRuntime;
using TaskManager.Utils.BaseService.Monitor.SystemRuntime.BatchQueues;

namespace TaskManager.Utils.BaseService.Monitor
{
    public class TimeWatchHelper
    {
        static TimeWatchLogBatchQueue timewatchlogbatchqueue;
        static TimeWatchLogApiBatchQueue timewatchlogapibatchqueue;

        static TimeWatchHelper()
        {
            timewatchlogbatchqueue = new TimeWatchLogBatchQueue();
            timewatchlogapibatchqueue = new TimeWatchLogApiBatchQueue();
        }

        public static void AddTimeWatchLog(TimeWatchLogInfo log)
        {
            if (TaskManager.Utils.Common.ConfigHelper.IsWriteTimeWatchLog && TaskManager.Utils.Common.ConfigHelper.IsWriteTimeWatchLogToMonitorPlatform)
            { 
                try
                {
                    timewatchlogbatchqueue.Add(log);
                }
                catch (Exception exp)
                {
                    TaskManager.Utils.Log.ErrorLog.Write("耗时日志出错",exp);
                }
            }
        }

        public static void AddTimeWatchApiLog(TimeWatchLogApiInfo log)
        {
            if (TaskManager.Utils.Common.ConfigHelper.IsWriteTimeWatchLog && TaskManager.Utils.Common.ConfigHelper.IsWriteTimeWatchLogToMonitorPlatform)
            {
                try
                {
                    timewatchlogapibatchqueue.Add(log);
                    //timewatchlogbatchqueue.Add(new TimeWatchLogInfo()
                    //{
                    //    fromip = log.fromip,
                    //    logcreatetime = log.logcreatetime,
                    //    logtag = log.url.GetHashCode(),
                    //    url=log.url,
                    //    time = log.time,
                    //    sqlip = "",
                    //    remark = log.tag,
                    //    projectname = log.projectname,
                    //    msg = log.msg,
                    //    logtype = (int)EnumTimeWatchLogType.ApiUrl
                    //});
                }
                catch (Exception exp)
                {
                    TaskManager.Utils.Log.ErrorLog.Write("耗时日志api出错", exp);
                }
            }
        }
    }
}
