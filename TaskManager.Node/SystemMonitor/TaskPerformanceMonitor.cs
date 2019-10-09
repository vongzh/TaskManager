using System;
using System.IO;
using TaskManager.Domain.Dal;
using TaskManager.Node.SystemRuntime;
using TaskManager.Node.Tools;
using TaskManager.Utils.Common;


namespace TaskManager.Node.SystemMonitor
{
    /// <summary>
    /// 任务性能监控者
    /// 用于检测当前任务运行的性能情况，通知到数据库
    /// </summary>
    public class TaskPerformanceMonitor : BaseMonitor
    {
        public override int Interval
        {
            get
            {
                return 1000 * 30;
            }
        }

        protected override void Run()
        {
            foreach (var taskruntimeinfo in TaskPoolManager.CreateInstance().GetList())
            {
                try
                {
                    if (taskruntimeinfo == null)
                        continue;

                    string fileinstallpath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + GlobalConfig.TaskDllDir + @"\" + taskruntimeinfo.TaskModel.id;
                    double dirsizeM = -1;
                    if (Directory.Exists(fileinstallpath))
                    {
                        long dirsize = IOHelper.DirSize(new DirectoryInfo(fileinstallpath));
                        dirsizeM = (double)dirsize / 1024 / 1024;
                    }
                    try
                    {
                        if (taskruntimeinfo.Domain != null)
                        {
                            SqlHelper.ExcuteSql(GlobalConfig.TaskDataBaseConnectString, (c) =>
                            {
                                tb_performance_dal nodedal = new tb_performance_dal();
                                nodedal.AddOrUpdate(c, new Domain.Model.tb_performance_model()
                                {
                                    cpu = taskruntimeinfo.Domain.MonitoringTotalProcessorTime.TotalSeconds,
                                    memory = (double)taskruntimeinfo.Domain.MonitoringSurvivedMemorySize / 1024 / 1024,
                                    installdirsize = dirsizeM,
                                    taskid = taskruntimeinfo.TaskModel.id,
                                    lastupdatetime = DateTime.Now,
                                    nodeid = GlobalConfig.NodeID
                                });
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.AddTaskError("任务性能监控时出错", taskruntimeinfo.TaskModel.id, ex);
                    }
                }
                catch (Exception exp)
                {
                    LogHelper.AddNodeError("任务性能监控者出错", exp);
                }
            }
        }
    }
}
