using System;

using System.Collections.Generic;

namespace TaskManager.Utils.BaseService.Monitor.Model
{
    /// <summary>
    /// tb_timewatchlog_api Data Structure.
    /// </summary>
    [Serializable]
    public partial class tb_timewatchlog_api_model
    {
	/*代码自动生成工具自动生成,不要在这里写自己的代码，否则会被自动覆盖哦 - 车毅*/
        
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime sqlservercreatetime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime logcreatetime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public double time { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string projectname { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string fromip { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string tag { get; set; }
        
    }
}