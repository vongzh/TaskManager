using System;
using Quartz;
using TaskManager.Node.Tools;
using System.Threading.Tasks;


namespace TaskManager.Node.SystemRuntime
{
    /// <summary>
    /// 通用任务的回调job
    /// </summary>
    public class TaskJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                int taskid = Convert.ToInt32(context.JobDetail.Key.Name);
                var taskruntimeinfo = TaskPoolManager.CreateInstance().Get(taskid.ToString());
                if (taskruntimeinfo == null || taskruntimeinfo.DllTask == null)
                {
                    LogHelper.AddTaskError("当前任务信息为空引用", taskid, new Exception());
                    return Task.FromResult(false);
                }
                taskruntimeinfo.TaskLock.Invoke(() =>
                {
                    try
                    {
                        taskruntimeinfo.DllTask.TryRun();
                    }
                    catch (Exception exp)
                    {
                        LogHelper.AddTaskError($"任务{ taskid }回调时执行失败", taskid, exp);
                    }
                });
                return Task.FromResult(true);
            }
            catch (Exception exp)
            {
                LogHelper.AddNodeError("任务回调时严重系统级错误", exp);
                return Task.FromResult(false);
            }
        }
    }
}
