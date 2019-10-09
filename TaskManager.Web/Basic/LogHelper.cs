using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Web;
using TaskManager.Domain.Dal;
using TaskManager.Domain.Model;

using TaskManager.Utils.Extensions;
using TaskManager.Utils.Log;
using TaskManager.Utils.Common;

namespace TaskManager.Web
{
    /// <summary>
    /// 节点内部日志操作类
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="model"></param>
        public static void AddLog(tb_log_model model)
        {
            try
            {
                SqlHelper.ExcuteSql(Config.TaskConnectString, (c) =>
                {
                    tb_log_dal logdal = new tb_log_dal();
                    model.msg = model.msg.SubString2(1000);
                    logdal.Add2(c, model);
                });
            }
            catch (Exception exp)
            {
                ErrorLog.Write("添加日志至数据库出错", exp);
            }
        }
        /// <summary>
        /// 添加错误日志
        /// </summary>
        /// <param name="model"></param>
        public static void AddError(tb_error_model model)
        {
            try
            {
                AddLog(new tb_log_model { logcreatetime = model.errorcreatetime, logtype = model.errortype, msg = model.msg, taskid = model.taskid });
                SqlHelper.ExcuteSql(Config.TaskConnectString, (c) =>
                {
                    tb_error_dal errordal = new tb_error_dal();
                    model.msg = model.msg.SubString2(1000);
                    errordal.Add2(c, model);
                });
            }
            catch (Exception exp)
            {
                ErrorLog.Write("添加错误日志至数据库出错", exp);
            }
        }
    }
}