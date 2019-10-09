using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Utils.BaseService.Monitor.Model;
using TaskManager.Utils.Common;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;


namespace TaskManager.Utils.BaseService.Monitor.SystemRuntime.BatchQueues
{
    public class TimeWatchLogApiBatchQueue : BaseBatchQueue<tb_timewatchlog_api_model>
    {
        public override int MaxQueueCount { get { return 10000; } }

        public override int MaxSleepTime { get { return 5000 * 60; } }//5分钟

        public override int SleepTime { get { return 5000; } }

        public override string BatchTable { get { return "tb_timewatchlog_api"; } }

         protected override void BatchCommit()
        {
            if (!string.IsNullOrEmpty(TaskManager.Utils.Common.ConfigHelper.TimeWatchConnectionString))
            {
                var timewatchinfoTable = DataTableHelper.ConvertToDataTable<tb_timewatchlog_api_model>(TempQueue);
                var dict = new Dictionary<string, string>
                            {
                                //{"sqlservercreatetime", "sqlservercreatetime"},
                                {"logcreatetime","logcreatetime"},
                                {"time", "time"},
                                {"projectname", "projectname"},
                                {"url", "url"},
                                {"tag", "tag"},
                                {"msg", "msg"},
                                {"fromip", "fromip"}
                            };
                using (var c = Db.DbConfig.CreateConn(Db.DbType.SQLSERVER, TaskManager.Utils.Common.ConfigHelper.TimeWatchConnectionString))
                {
                    c.Open();
                    c.BeginTransaction();
                    try
                    {
                        c.SqlBulkCopy(timewatchinfoTable, BatchTable + DateTime.Now.ToString("yyyyMMdd"), "", new List<ProcedureParameter>(), dict, 0);
                        c.Commit();
                    }
                    catch (Exception exp)
                    {
                        c.Rollback();
                    }
                }
            }

        }
    }
}
