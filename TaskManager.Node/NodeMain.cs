﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskManager.Core;
using TaskManager.Core.Net;
using TaskManager.Utils.Api;
using TaskManager.Utils.Common;

using TaskManager.Utils.Serialization;

namespace TaskManager.Node
{
    public partial class NodeMain : Form
    {
        public NodeMain()
        {
            //此处隐藏节点窗体
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;

            InitializeComponent();
        }

        private void NodeMain_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(GlobalConfig.TaskDataBaseConnectString) || GlobalConfig.NodeID <= 0)
                {
                    string url = GlobalConfig.TaskManagerWebUrl.TrimEnd('/') + "/OpenApi/" + "GetNodeConfigInfo/";
                    ClientResult r = ApiHelper.Get(url, new
                    {

                    });
                    if (r.success == false)
                    {
                        throw new Exception($"请求{url}失败,请检查配置中“任务调度平台站点url”配置项");
                    }

                    var appconfiginfo = ApiHelper.Data(r);
                    string connectstring =appconfiginfo.TaskDataBaseConnectString;
                    appconfiginfo.TaskDataBaseConnectString = StringDESHelper.DecryptDES(connectstring, "vongzh");

                    if (string.IsNullOrWhiteSpace(GlobalConfig.TaskDataBaseConnectString))
                        GlobalConfig.TaskDataBaseConnectString = appconfiginfo.TaskDataBaseConnectString;
                    if (GlobalConfig.NodeID <= 0)
                        GlobalConfig.NodeID = appconfiginfo.NodeID;
                }

                IOHelper.CreateDirectory(GlobalConfig.TaskSharedDllsDir + @"\");
                CommandQueueProcessor.Run();

                //注册后台监控
                GlobalConfig.Monitors.Add(new SystemMonitor.TaskRecoverMonitor());
                GlobalConfig.Monitors.Add(new SystemMonitor.TaskPerformanceMonitor());
                GlobalConfig.Monitors.Add(new SystemMonitor.NodeHeartBeatMonitor());
                GlobalConfig.Monitors.Add(new SystemMonitor.TaskStopMonitor());
                this.Text = this.Text + GlobalConfig.NodeID;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message + ",进程即将退出!");
                Application.Exit();
            }
        }
    }
}
