using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskManager.Utils.Common;

namespace TaskManager.Node
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                if (args.Length == 1)
                {
                    GlobalConfig.NodeID = Convert.ToInt32(args[0]);
                }
                else if (args.Length == 2)
                {
                    GlobalConfig.NodeID = Convert.ToInt32(args[0]);
                    GlobalConfig.TaskDataBaseConnectString = Convert.ToString(args[1]);
                }
            }

            Application.Run(new NodeMain());
        }
    }
}
