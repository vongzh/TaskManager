using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Model;
using System.Threading.Tasks;
using TaskManager.Domain.Dal;

using System.Net;
using TaskManager.Utils.Common;

namespace TaskManager.Node.SystemMonitor
{
    /// <summary>
    /// 节点心跳监控者
    /// 用于心跳通知数据库当前节点状态
    /// </summary>
    public class NodeHeartBeatMonitor : BaseMonitor
    {
        public override int Interval
        {
            get
            {
                return 1000 * 1 * 5;
            }
        }
        protected override void Run()
        {
            SqlHelper.ExcuteSql(GlobalConfig.TaskDataBaseConnectString, (c) =>
            {
                var sqldatetime = c.GetServerDate();
                tb_node_dal nodedal = new tb_node_dal();
                var node = nodedal.GetOneNode(c, GlobalConfig.NodeID);
                if (node != null)
                {
                    node.nodeip = Dns.GetHostName();
                    node.nodelastupdatetime = sqldatetime;
                }
                else
                {
                    node = new tb_node_model()
                    {
                        ifcheckstate = true,
                        nodelastupdatetime = sqldatetime,
                        nodecreatetime = sqldatetime,
                        nodeip = Dns.GetHostName(),
                        id = GlobalConfig.NodeID,
                        nodename = "新增节点",
                    };
                }
                nodedal.AddOrUpdate(c, node);
            });
        }
    }
}
