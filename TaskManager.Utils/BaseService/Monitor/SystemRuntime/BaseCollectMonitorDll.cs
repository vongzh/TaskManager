using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskManager.Utils.Serialization;
using TaskManager.Utils.Extensions;
using System.Diagnostics;

namespace TaskManager.Utils.BaseService.Monitor.SystemRuntime
{
    /// <summary>
    /// 基础监控采集dll任务
    /// </summary>
    public abstract class BaseCollectMonitorDll : MarshalByRefObject, IDisposable
    {
        public string PlatformManageConnectString = "";
        public string ServerIP = "";

        public BaseCollectMonitorDll()
        {
            
        }

        /*忽略默认的对象租用行为，以便“在主机应用程序域运行时始终”将对象保存在内存中.   
          这种机制将对象锁定到内存中，防止对象被回收，但只能在主机应用程序运行   
          期间做到这样。*/
        public override object InitializeLifetimeService()
        {
            return null;
        }
        /// <summary>
        /// 线上环境运行入口
        /// </summary>
        public void TryStart()
        {
            try
            {
                Start();
            }
            catch (Exception exp)
            {
               //添加错误日志
            }
        }

        /// <summary>
        /// 与第三方约定的运行接口方面
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// 系统级稀有资源释放接口
        /// </summary>
        public virtual void Dispose() { }
    }
}
