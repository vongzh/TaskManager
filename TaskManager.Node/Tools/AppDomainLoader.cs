using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Node.Tools
{
    /// <summary>
    /// 应用程序域加载者
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AppDomainLoader<T> where T : class
    {
        /// <summary>
        /// 加载应用程序域，获取相应实例
        /// </summary>
        /// <param name="dllpath"></param>
        /// <param name="classpath"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public T Load(string dllpath, string classpath, out AppDomain domain)
        {
            AppDomainSetup setup = new AppDomainSetup();
            if (File.Exists(dllpath+".config"))
                setup.ConfigurationFile = dllpath + ".config";
            setup.ShadowCopyFiles = "true";
            setup.ApplicationBase = Path.GetDirectoryName(dllpath);
            domain = AppDomain.CreateDomain(Path.GetFileName(dllpath), null, setup);
            AppDomain.MonitoringIsEnabled = true;
            T obj = (T)domain.CreateInstanceFromAndUnwrap(dllpath, classpath);
            return obj;
        }
        /// <summary>
        /// 卸载应用程序域
        /// </summary>
        /// <param name="domain"></param>
        public void UnLoad(AppDomain domain)
        {
            AppDomain.Unload(domain);
            domain = null;
        }
    }
}
