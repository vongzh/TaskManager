using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Node.SystemRuntime;

namespace TaskManager.Node.Commands
{
    /// <summary>
    /// 开启任务命令
    /// </summary>
    public class StartTaskCommand:BaseCommand
    {
        public override void Execute()
        {
            TaskProvider tp = new TaskProvider();
            tp.Start(this.CommandInfo.taskid);
        }
    }
}
