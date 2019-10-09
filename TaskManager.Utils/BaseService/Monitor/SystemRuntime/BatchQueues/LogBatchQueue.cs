using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Utils.BaseService.Monitor.Dal;
using TaskManager.Utils.BaseService.Monitor.Model;
using TaskManager.Utils.Common;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;


namespace TaskManager.Utils.BaseService.Monitor.SystemRuntime.BatchQueues
{
    public class LogBatchQueue : BaseBatchQueue<tb_log_model>
    {
        public override int MaxQueueCount { get { return 10000; } }

        public override int MaxSleepTime { get { return 5000 * 60; } }//5分钟

        public override int SleepTime { get { return 5000; } }

        public override string BatchTable { get { return "tb_log"; } }

        protected override void BatchCommit()
        {
            if (!string.IsNullOrEmpty(Config.UnityLogConnectString))
            {
                if (TempQueue.Count > 500)
                {
                    var dict = new Dictionary<string, string>
                            {
                                {"logcreatetime","logcreatetime"},
                                {"logtype", "logtype"},
                                {"projectname", "projectname"},
                                {"logtag", "logtag"},
                                {"msg", "msg"}
                            };
                    var timewatchinfoTable = DataTableHelper.ConvertToDataTable<tb_log_model>(TempQueue);
                    using (var c = Db.DbConfig.CreateConn(Db.DbType.SQLSERVER, Config.UnityLogConnectString))
                    {
                        c.Open();
                        c.BeginTransaction();
                        try
                        {
                            c.SqlBulkCopy(timewatchinfoTable, BatchTable + DateTime.Now.ToString("yyyyMM"), "", new List<ProcedureParameter>(), dict, 0);
                            c.Commit();
                        }
                        catch (Exception exp)
                        {
                            c.Rollback();
                        }

                    }
                }
                else
                {
                    foreach (var t in TempQueue)
                    {
                        try
                        {
                            SqlHelper.ExcuteSql(Config.UnityLogConnectString, (c) =>
                            {
                                tb_log_dal logdal = new tb_log_dal();
                                logdal.Add(c, t);
                            });
                        }
                        catch { }
                    }
                }
            }
        }
    }
}
