using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SystemRuntime;

namespace TaskManager.MonitorTasks
{
    public class Program
    {
        [STAThread]
        static void Main()
        {
            TaskManageErrorSendTask task = new TaskManageErrorSendTask();
            task.SystemRuntimeInfo = new TaskSystemRuntimeInfo() { TaskConnectString = "server=.;Initial Catalog=TaskManager;User ID=sa;Password=123456" };
            task.TestRun();
        }
    }
}
