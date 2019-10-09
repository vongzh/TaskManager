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
	/*�����Զ����ɹ����Զ�����,��Ҫ������д�Լ��Ĵ��룬����ᱻ�Զ�����Ŷ - ����*/
        
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        
        /// <summary>
        /// ���ݿⴴ��ʱ��
        /// </summary>
        public DateTime sqlservercreatetime { get; set; }
        
        /// <summary>
        /// ��־��Ŀ�д���ʱ��
        /// </summary>
        public DateTime logcreatetime { get; set; }
        
        /// <summary>
        /// ��־����:һ�����������,ϵͳ�����ش���,һ��ҵ����־,ϵͳ��־
        /// </summary>
        public Byte logtype { get; set; }
        
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string projectname { get; set; }
        
        /// <summary>
        /// ��־Ψһ��ʾ(��̵ķ���������url,���ڹ���)
        /// </summary>
        public string logtag { get; set; }
        
        /// <summary>
        /// ��־��Ϣ
        /// </summary>
        public string msg { get; set; }
        
    }
}