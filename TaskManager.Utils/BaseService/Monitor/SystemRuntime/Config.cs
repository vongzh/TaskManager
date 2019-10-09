using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Utils.BaseService.Monitor.Dal;
using TaskManager.Utils.Common;

namespace TaskManager.Utils.BaseService.Monitor.SystemRuntime
{
    public class Config
    {
        static Config()
        {
            try
            {
                SqlHelper.ExcuteSql(TaskManager.Utils.Common.ConfigHelper.MonitorPlatformConnectionString, (c) => {
                    tb_database_config_dal configdal = new tb_database_config_dal();
                    UnityLogConnectString = DbShardingHelper.GetDataBase(configdal.GetModelList(c), DataBaseType.UnityLog);
                });
            }
            catch 
            {
                
            }
        }
        public static string UnityLogConnectString = "";

        public static int TimeWatchLogBatchCommitCount { get { return Convert.ToInt32(TaskManager.Utils.Common.ConfigHelper.Get("TimeWatchLogBatchCommitCount", "10000")); } }
    }
}
