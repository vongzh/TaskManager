using System;

using System.Collections.Generic;

namespace TaskManager.Utils.BaseService.Monitor.Model
{
    /// <summary>
    /// tb_timewatchlog Data Structure.
    /// </summary>
    [Serializable]
    public partial class tb_timewatchlog_model
    {
        /*�����Զ����ɹ����Զ�����,��Ҫ������д�Լ��Ĵ��룬����ᱻ�Զ�����Ŷ - ����*/

        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// ���ݿⱾ�ش���ʱ��
        /// </summary>
        public DateTime sqlservercreatetime { get; set; }

        /// <summary>
        /// ��־����ʱ��
        /// </summary>
        public DateTime logcreatetime { get; set; }

        /// <summary>
        /// ��ʱ
        /// </summary>
        public double time { get; set; }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string projectname { get; set; }

        /// <summary>
        /// ��ʱ��־���ͣ���ͨ��־=0��api�ӿ���־=1��sql��־=2
        /// </summary>
        public Byte logtype { get; set; }

        /// <summary>
        /// ��־��ʶ,sql������Ϊsql��ϣ,api������Ϊurl
        /// </summary>
        public int logtag { get; set; }

        /// <summary>
        /// ��ǰurl
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// ��ǰ��Ϣ
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// ��Դip(����ִ��ip)
        /// </summary>
        public string fromip { get; set; }

        /// <summary>
        /// sqlip��ַ
        /// </summary>
        public string sqlip { get; set; }

        /// <summary>
        /// ������¼�����Ϣ
        /// </summary>
        public string remark { get; set; }

    }
}