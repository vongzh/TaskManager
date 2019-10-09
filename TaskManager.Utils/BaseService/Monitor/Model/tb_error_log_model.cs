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
        /// ��־����:һ�����������,ϵͳ�����ش���
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
        /// ������Ϣ
        /// </summary>
        public string msg { get; set; }
        
        /// <summary>
        /// ��ջ����
        /// </summary>
        public string tracestack { get; set; }
        
        /// <summary>
        /// ������ע��Ϣ
        /// </summary>
        public string remark { get; set; }
        
        /// <summary>
        /// ��ؿ�����Ա
        /// </summary>
        public string developer { get; set; }
        
    }
}