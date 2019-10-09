using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
//using TaskManager.Utils.BaseService.ConfigManager;
//using TaskManager.Utils.BaseService.ConfigManager;
//using TaskManager.Utils.BaseService.ConfigManager.SystemRuntime;

namespace TaskManager.Utils.Common
{
    /// <summary>
    /// XXF配置文件
    /// 通过配置中心读取XXF的日志
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 是否记录访问认证中心的耗时（得到Token信息和验证Token）
        /// </summary>
        public static bool WriteVisitCertCenterTimeWatchLog { get { return Convert.ToBoolean(Get("WriteVisitCertCenterTimeWatchLog", "false")); } }
        /// <summary>
        /// 是否写错误日志
        /// </summary>
        public static bool IsWriteErrorLog { get { return Convert.ToBoolean(Get("IsWriteErrorLog", "true")); } }
        /// <summary>
        /// 是否拦截访问日志
        /// </summary>
        public static bool IsWriteVisitLog { get { return Convert.ToBoolean(Get("IsWriteVisitLog", "false")); } }
        /// <summary>
        /// 错误日志是否写入监控平台
        /// </summary>
        public static bool IsWriteErrorLogToMonitorPlatform { get { return Convert.ToBoolean(Get("IsWriteErrorLogToMonitorPlatform", "false")); } }
        /// <summary>
        /// 错误日志是否写入本地文件
        /// </summary>
        public static bool IsWriteErrorLogToLocalFile { get { return Convert.ToBoolean(Get("IsWriteErrorLogToLocalFile", "true")); } }
        /// <summary>
        /// 是否写常用日志
        /// </summary>
        public static bool IsWriteCommonLog { get { return Convert.ToBoolean(Get("IsWriteCommonLog", "true")); } }
        /// <summary>
        /// 常用日志是否写入监控平台
        /// </summary>
        public static bool IsWriteCommonLogToMonitorPlatform { get { return Convert.ToBoolean(Get("IsWriteCommonLogToMonitorPlatform", "false")); } }
        /// <summary>
        /// 常用日志是否写入本地文件
        /// </summary>
        public static bool IsWriteCommonLogToLocalFile { get { return Convert.ToBoolean(Get("IsWriteCommonLogToLocalFile", "true")); } }
        /// <summary>
        /// 是否写耗时日志
        /// </summary>
        public static bool IsWriteTimeWatchLog { get { return Convert.ToBoolean(Get("IsWriteTimeWatchLog", "false")); } }
        /// <summary>
        /// 耗时日志是否写入监控平台
        /// </summary>
        public static bool IsWriteTimeWatchLogToMonitorPlatform { get { return Convert.ToBoolean(Get("IsWriteTimeWatchLogToMonitorPlatform", "false")); } }
        /// <summary>
        /// 耗时日志是否写入本地文件
        /// </summary>
        public static bool IsWriteTimeWatchLogToLocalFile { get { return Convert.ToBoolean(Get("IsWriteTimeWatchLogToLocalFile", "false")); } }
        /// <summary>
        /// 耗时监控数据库连接
        /// </summary>
        public static string TimeWatchConnectionString { get { return Get("TimeWatchConnectionString", ""); } }
        /// <summary>
        /// 监控平台数据库连接
        /// </summary>
        public static string MonitorPlatformConnectionString { get { return Get("MonitorPlatformConnectionString", ""); } }//server=192.168.17.200;Initial Catalog=bs_MonitorPlatform;User ID=sa;Password=Xx~!@#;

        /// <summary>
        /// 点啊点主库数据库连接
        /// </summary>
        public static string MainConnectString { get { return Get("MainConnectString", ""); } }//server=192.168.17.200;Initial Catalog=dyd_new_main;User ID=sa;Password=Xx~!@#;

        /// <summary>
        /// 点啊点配置数据库连接
        /// </summary>
        //public static string ConfigConnectString { get { return _ConfigConnectString == null ? Get("ConfigConnectString", "") : _ConfigConnectString; } set { _ConfigConnectString = value; } }//server=192.168.17.200;Initial Catalog=dyd_new_main;User ID=sa;Password=Xx~!@#;
        public static string ConfigConnectString { get { return Get("ConfigConnectString", ""); } set { _ConfigConnectString = value; } }//server=192.168.17.200;Initial Catalog=dyd_new_main;User ID=sa;Password=Xx~!@#;
        private static string _ConfigConnectString;//用于兼容“任务调度中心”的使用

        /// <summary>
        /// 点啊点统一配置中心数据库连接
        /// </summary>
        public static string ConfigManagerConnectString = Get("ConfigManagerConnectString", "");//server=192.168.17.200;Initial Catalog=dyd_new_main;User ID=sa;Password=Xx~!@#;


        /// <summary>
        /// 是否启用配置分库连接--默认不启用
        /// </summary>
        public static string IsEnabledDepotsConnectByConfig { get { return Get("IsEnabledMainConnectByConfig", "false"); } }

        /// <summary>
        /// 当前项目名称
        /// </summary>
        public static string ProjectName = Get("ProjectName", "未命名项目");
        /// <summary>
        /// 当前项目默认开发人员
        /// </summary>
        public static string ProjectDeveloper { get { return Get("ProjectDeveloper", ""); } }


        /// <summary>
        /// 集群性能监控库连接
        /// </summary>
        public static string ClusterConnectString { get { return Get("ClusterConnectString", ""); } }

        /// <summary>
        /// 集群性能监控库连接
        /// </summary>
        public static string PlatformManageConnectString { get { return Get("PlatformManageConnectString", ""); } }

        /// <summary>
        /// 耗时库连接
        /// </summary>
        public static string TimeWatchConnectString { get { return Get("TimeWatchConnectString", ""); } }

        /// <summary>
        /// 集群性能监控库连接
        /// </summary>
        public static string UnityLogConnectString { get { return Get("UnityLogConnectString", ""); } }

        /// <summary>
        /// 创建月表OR日表SQL路径
        /// </summary>
        public static string TableCreateSqlTxtUrl { get { return Get("DayTableCreateSqlTxtUrl", ""); } }

        /// <summary>
        /// 创建DLL执行创建的类型（Day：日表,Month：月表）
        /// </summary>
        public static string TableCreateType { get { return Get("TableCreateType", ""); } }
        /// <summary>
        /// MQ发送失败时,消息重发备用数据库连接
        /// </summary>
        public static string MQErrorConnectString { get { return Get("MQErrorConnectString", ""); } }
        /// <summary>
        /// MQ发送失败时,消息重发备用数据库连接
        /// </summary>
        public static int MQMaxTablePartitionNum { get { return Convert.ToInt32(Get("MQMaxTablePartitionNum", "1")); } }

        public static string Get(string key, string defaultvalue = "")
        {


            if (key == "ConfigManagerConnectString" || key == "ProjectName" || ProjectName == "未命名项目" || string.IsNullOrWhiteSpace(ProjectName) || string.IsNullOrWhiteSpace(ConfigManagerConnectString))
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains(key))
                    return System.Configuration.ConfigurationManager.AppSettings[key];
            }
            //else
            //{
            //    try
            //    {
            //        var value = ConfigManagerHelper.Get<string>(key);
            //        if (value != null)
            //            return value;
            //    }
            //    catch
            //    {
            //        return defaultvalue;
            //    }

            //}
            return defaultvalue;
        }

        /// <summary>
        /// 从配置中心获取配置
        /// </summary>
        /// <param name="configkey"></param>
        /// <returns></returns>
        private static string GetFromConfigManager(string configkey)
        {
            string value = null;
            try
            {

                //if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains(configkey))
                //{
                    value = ConfigurationManager.AppSettings[configkey];
               // }
                //else
                //{
                //    //if (ConfigManagerHelper.GetInstance(configkey) != null)
                //    //{
                //    //    if (AppDomainContext.Context == null || AppDomainContext.Context.ConfigInfoOfKeyDic == null)
                //    //        return null;
                //    //    var config = AppDomainContext.Context.ConfigInfoOfKeyDic.GetConfig(configkey);
                //    //    if (config == null)
                //    //        return null;
                //    //    value = config.Value();

                //    //}

                 
                //}
            }
            catch (Exception)
            {
            }

            return value;
        }
    }
}
