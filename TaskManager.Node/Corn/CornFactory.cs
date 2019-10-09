using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Core.CustomCorn;
using TaskManager.Node.SystemRuntime;
using Quartz;

namespace TaskManager.Node.Corn
{
    public class CornFactory
    {
        public static ITrigger CreateTigger(NodeTaskRuntimeInfo taskruntimeinfo)
        {
            if (taskruntimeinfo.TaskModel.taskcron.Contains("[") || taskruntimeinfo.TaskModel.taskcron.Contains("]"))
            {
                var customcorn = CustomCornFactory.GetCustomCorn(taskruntimeinfo.TaskModel.taskcron);
                customcorn.Parse();
                if (customcorn is SimpleCorn || customcorn is RunOnceCorn)
                {
                    var simplecorn = customcorn as SimpleCorn;

                    // 定义调度触发规则，比如每1秒运行一次，共运行8次
                    TriggerBuilder simpleTriggerBuilder = TriggerBuilder.Create()
                                                .WithIdentity(taskruntimeinfo.TaskModel.id.ToString(), taskruntimeinfo.TaskModel.categoryid.ToString());
                    if (simplecorn.ConInfo.StartTime != null)
                        simpleTriggerBuilder.StartAt(simplecorn.ConInfo.StartTime.Value.ToUniversalTime());
                    if (simplecorn.ConInfo.EndTime != null)
                        simpleTriggerBuilder.EndAt(simplecorn.ConInfo.EndTime.Value.ToUniversalTime());

                    SimpleScheduleBuilder simpleScheduleBuilder = SimpleScheduleBuilder.Create();
                    if (simplecorn.ConInfo.RepeatInterval != null)
                        simpleScheduleBuilder.WithIntervalInSeconds(TimeSpan.FromSeconds(simplecorn.ConInfo.RepeatInterval.Value).Seconds);
                    else
                        simpleScheduleBuilder.WithIntervalInSeconds(TimeSpan.FromSeconds(1).Seconds);

                    if (simplecorn.ConInfo.RepeatCount != null)
                        simpleScheduleBuilder.WithRepeatCount(simplecorn.ConInfo.RepeatCount.Value - 1);//因为任务默认执行一次，所以减一次
                    else
                        simpleScheduleBuilder.WithRepeatCount(int.MaxValue);


                    return simpleTriggerBuilder.WithSchedule(simpleScheduleBuilder).Build();
                }
                return null;
            }
            else
            {
                TriggerBuilder triggerBuilder = TriggerBuilder.Create()
                                                .WithIdentity(taskruntimeinfo.TaskModel.id.ToString(), taskruntimeinfo.TaskModel.categoryid.ToString())
                                                .WithCronSchedule(taskruntimeinfo.TaskModel.taskcron);
                return triggerBuilder.Build();
            }
        }
    }
}
