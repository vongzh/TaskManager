﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Node;
using TaskManager.Node.SystemMonitor;
using TaskManager.Node.Tools;
using TaskManager.Utils.Api;
using TaskManager.Utils.Common;


namespace TaskManager.WinService
{
    public class NodeService : ServiceBase
    {
        protected override void OnStart(string[] args)
        {
            try
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains("NodeID"))
                {
                    GlobalConfig.NodeID = Convert.ToInt32(ConfigurationManager.AppSettings["NodeID"]);
                }
                if (string.IsNullOrWhiteSpace(GlobalConfig.TaskDataBaseConnectString) || GlobalConfig.NodeID <= 0)
                {
                    string url = GlobalConfig.TaskManagerWebUrl.TrimEnd('/') + "/OpenApi/" + "GetNodeConfigInfo/";
                    ClientResult r = ApiHelper.Get(url, new { });
                    if (r.success == false)
                    {
                        throw new Exception("请求" + url + "失败,请检查配置中“任务调度平台站点url”配置项");
                    }

                    dynamic appconfiginfo = ApiHelper.Data(r);
                    string connectstring = appconfiginfo.TaskDataBaseConnectString;
                    appconfiginfo.TaskDataBaseConnectString = StringDESHelper.DecryptDES(connectstring, "vongzh");

                    if (string.IsNullOrWhiteSpace(GlobalConfig.TaskDataBaseConnectString))
                        GlobalConfig.TaskDataBaseConnectString = appconfiginfo.TaskDataBaseConnectString;
                    if (GlobalConfig.NodeID <= 0)
                        GlobalConfig.NodeID = appconfiginfo.NodeID;
                }

                IOHelper.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + GlobalConfig.TaskSharedDllsDir + @"\");
                CommandQueueProcessor.Run();

                //注册后台监控
                GlobalConfig.Monitors.Add(new TaskRecoverMonitor());
                GlobalConfig.Monitors.Add(new TaskPerformanceMonitor());
                GlobalConfig.Monitors.Add(new NodeHeartBeatMonitor());
                GlobalConfig.Monitors.Add(new TaskStopMonitor());

                LogHelper.AddNodeLog("节点windows服务启动成功");
            }
            catch (Exception exp)
            {
                LogHelper.AddNodeError("节点windows服务启动失败", exp);
            }
        }

        protected override void OnStop()
        {
            LogHelper.AddNodeLog("节点windows服务停止");
        }
    }
}
