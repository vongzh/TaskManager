using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Node.SystemRuntime;

namespace TaskManager.Node.Commands
{
    /// <summary>
    /// 卸载任务命令
    /// </summary>
    public class UninstallTaskCommand:BaseCommand
    {
        public override void Execute()
        {
            TaskProvider tp = new TaskProvider();
            tp.Uninstall(this.CommandInfo.taskid);
        }
    }
}
