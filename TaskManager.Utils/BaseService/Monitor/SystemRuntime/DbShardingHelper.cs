using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Utils.BaseService.Monitor.Model;

namespace TaskManager.Utils.BaseService.Monitor.SystemRuntime
{
    /// <summary>
    /// 数据库分表分库帮助类
    /// </summary>
    public class DbShardingHelper
    {
        public static string DayRule(DateTime time)
        {
            return time.ToString("yyyyMMdd");
        }

        public static string MonthRule(DateTime time)
        {
            return time.ToString("yyyyMM");
        }
        public static string GetDataBase<T>(List<T> tb_database_config_models, DataBaseType type)
        {
           return GetDataBase( tb_database_config_models.Select(c=>(dynamic)c).ToList(), type);
        }
        public static string GetDataBase(List<dynamic> tb_database_config_models, DataBaseType type) 
        {
            //List<dynamic> models = tb_database_config_models;
            //if (tb_database_config_models.GetType() == typeof(List<>))
            //{
            //    throw new Exception("传入models 类型为List<tb_database_config_model>");
            //}
            if (tb_database_config_models == null || tb_database_config_models.Count == 0)
                throw new Exception("GetDataBase找不到数据库连接");
            var o = tb_database_config_models.Where(c => c.dbtype == (int)type).FirstOrDefault();
            if (o == null)
                throw new Exception("GetDataBase找不到数据库连接");
            return GetDataBase(o);
        }

        public static string GetDataBase(dynamic tb_database_config_model)
        {
            return "server={dbserver};Initial Catalog={dbname};User ID={dbuser};Password={dbpass};".Replace("{dbserver}", tb_database_config_model.dbserver).Replace("{dbname}", tb_database_config_model.dbname)
                            .Replace("{dbuser}", tb_database_config_model.dbuser).Replace("{dbpass}", tb_database_config_model.dbpass);
        }
    }
}
