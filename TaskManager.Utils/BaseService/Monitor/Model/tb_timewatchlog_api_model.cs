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
	/*�����Զ����ɹ����Զ�����,��Ҫ������д�Լ��Ĵ��룬����ᱻ�Զ�����Ŷ - ����*/
        
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