using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager;
using TaskManager.SystemRuntime;

namespace TaskManager.Demo
{
    public class DemoDisposeTask2 : BaseDllTask
    {
        public DemoDisposeTask2() : base()
        {
            this.SafeDisposeOperator = new TaskSafeDisposeOperator(1);
        }

        public override void Run()
        {
            this.OpenOperator.Log("开始");
            while (true)
            {
                System.Threading.Thread.Sleep(5000);
                this.OpenOperator.Log("停顿5秒");
                if (this.SafeDisposeOperator.DisposedState == TaskDisposedState.Disposing)
                    break;
            }
            this.SafeDisposeOperator.DisposedState = TaskDisposedState.Finished;
            this.OpenOperator.Log("退出");
        }

        public override void TestRun()
        {
            base.TestRun();
        }

        public override void Dispose()
        {
            base.Dispose();

        }
    }
}
