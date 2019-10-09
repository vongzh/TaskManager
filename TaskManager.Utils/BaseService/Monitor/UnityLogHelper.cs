using System;
using TaskManager.Utils.BaseService.Monitor.Dal;
using TaskManager.Utils.BaseService.Monitor.SystemRuntime;
using TaskManager.Utils.BaseService.Monitor.SystemRuntime.BatchQueues;

using TaskManager.Utils.Extensions;
using TaskManager.Utils.Common;
using TaskManager.Utils.Log;

namespace TaskManager.Utils.BaseService.Monitor
{
    public class UnityLogHelper
    {
        static LogBatchQueue logbatchqueue;
        static UnityLogHelper()
        {
            logbatchqueue = new LogBatchQueue();
        }

        public static void AddCommonLog(SystemRuntime.CommonLogInfo log)
        {
            if (ConfigHelper.IsWriteCommonLog && ConfigHelper.IsWriteCommonLogToMonitorPlatform)
            {
                try
                {
                    logbatchqueue.Add(log);
                }
                catch (Exception exp)
                {
                    ErrorLog.Write("常用日志出错", exp);
                }
            }
        }

        public static void AddErrorLog(SystemRuntime.ErrorLogInfo log)
        {
            if (ConfigHelper.IsWriteErrorLog && ConfigHelper.IsWriteErrorLogToMonitorPlatform)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(Config.UnityLogConnectString))
                    {
                        SqlHelper.ExcuteSql(Config.UnityLogConnectString, (c) =>
                        {
                            tb_error_log_dal errorlogdal = new tb_error_log_dal();
                            errorlogdal.Add(c, log);
                        });
                        logbatchqueue.Add(new SystemRuntime.CommonLogInfo() { logcreatetime = log.logcreatetime, logtag = log.logtag, logtype = log.logtype, msg = log.msg.SubString2(900).NullToEmpty(), projectname = log.projectname });
                    }
                }
                catch (Exception)
                {
                    
                }
            }
        }
    }
}
