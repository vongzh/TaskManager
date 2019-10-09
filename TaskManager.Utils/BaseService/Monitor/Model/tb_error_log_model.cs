using System;

using System.Collections.Generic;

namespace TaskManager.Utils.BaseService.Monitor.Model
{
    /// <summary>
    /// tb_error_log Data Structure.
    /// </summary>
    [Serializable]
    public partial class tb_error_log_model
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
        /// 日志类型:一般非正常错误,系统级严重错误
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
        /// 错误信息
        /// </summary>
        public string msg { get; set; }
        
        /// <summary>
        /// 堆栈跟踪
        /// </summary>
        public string tracestack { get; set; }
        
        /// <summary>
        /// 其他备注信息
        /// </summary>
        public string remark { get; set; }
        
        /// <summary>
        /// 相关开发人员
        /// </summary>
        public string developer { get; set; }
        
    }
}