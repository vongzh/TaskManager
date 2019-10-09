using System;

using System.Collections.Generic;

namespace TaskManager.Utils.BaseService.Monitor.Model
{
    /// <summary>
    /// tb_database_config Data Structure.
    /// </summary>
    [Serializable]
    public partial class tb_database_config_model
    {
	/*�����Զ����ɹ����Զ�����,��Ҫ������д�Լ��Ĵ��룬����ᱻ�Զ�����Ŷ - ����*/
        
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        
        /// <summary>
        /// ���ݿⱾ���ǳ�
        /// </summary>
        public string dblocalname { get; set; }
        
        /// <summary>
        /// ���ݿ��������ַ
        /// </summary>
        public string dbserver { get; set; }
        
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public string dbname { get; set; }
        
        /// <summary>
        /// ���ݿ��û���
        /// </summary>
        public string dbuser { get; set; }
        
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public string dbpass { get; set; }
        
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public Byte dbtype { get; set; }
        
    }
}