using System;
using System.Collections.Generic;
using System.Web;
using TaskManager.Utils.Api;
using TaskManager.Utils.Log;

namespace TaskManager.Utils.Common
{
    /// <summary>
    /// api帮助类
    /// </summary>
    public class ApiHelper
    {
        /// <summary>
        /// 获取api结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static ClientResult Get(string url, object param)
        {
            //可以记录api调用的网络耗时，便于优化以及一些错误日志的记录拦截
            //兼容后续版本的api通信机制

            try
            {
                TimeWatchLog watch = new TimeWatchLog();//网络耗时打印
                List<ParmField> parmList = new List<ParmField>();
                foreach (var p in param.GetType().GetProperties())
                {
                    var value = p.GetValue(param, null);
                    if (value != null)
                        value = value.ToString();
                    parmList.Add(new StringField(p.Name, value as string));
                }
                ClientResult r = HttpServer.InvokeApi(url, parmList);
                string msg = "";
                if (HttpContext.Current!= null&&HttpContext.Current.Request != null)
                    msg=HttpContext.Current.Request.RawUrl.ToString();
                watch.Write(new TimeWatchLogInfo(){ url=msg, logtag=msg.GetHashCode(), msg=msg, logtype= BaseService.Monitor.SystemRuntime.EnumTimeWatchLogType.ApiUrl});
                return r;
            }
            catch (Exception exp)
            {
                ErrorLog.Write("api调用出错:", exp);
                throw new Exception("api调用出错:" + exp.Message);

            }

        }
        /// <summary>
        /// 获取response对象
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static dynamic Response(ClientResult result)
        {
            return result.repObject["response"];
        }

        public static dynamic Data(ClientResult result)
        {
            return result.repObject["data"];
        }

        /// <summary>
        /// 获取时间戳  使用协调世界时UTC减去1970年1月1日
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetTimeStamp(DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

    }
}
