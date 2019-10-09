using System;

using System.Collections.Generic;

namespace TaskManager.Utils.BaseService.Monitor.Model
{
    /// <summary>
    /// tb_log Data Structure.
    /// </summary>
    [Serializable]
    public partial class tb_log_model
    {
	/*代码自动生成工具自动生成,不要在这里写自己的代码，否则会被自动覆盖哦 - 车毅*/
        
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        
        /// <summary>
        /// 数据库创建时间
        /// </summary>
        public DateTime sqlservercreatetime { get; set; }
        
        /// <summary>
        /// 日志项目中创建时间
        /// </summary>
        public DateTime logcreatetime { get; set; }
        
        /// <summary>
        /// 日志类型:一般非正常错误,系统级严重错误,一般业务日志,系统日志
        /// </summary>
        public Byte logtype { get; set; }
        
        /// <summary>
        /// 项目名称
        /// </summary>
        public string projectname { get; set; }
        
        /// <summary>
        /// 日志唯一标示(简短的方法名或者url,便于归类)
        /// </summary>
        public string logtag { get; set; }
        
        /// <summary>
        /// 日志信息
        /// </summary>
        public string msg { get; set; }
        
    }
}