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
        /*代码自动生成工具自动生成,不要在这里写自己的代码，否则会被自动覆盖哦 - 车毅*/

        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 数据库本地创建时间
        /// </summary>
        public DateTime sqlservercreatetime { get; set; }

        /// <summary>
        /// 日志创建时间
        /// </summary>
        public DateTime logcreatetime { get; set; }

        /// <summary>
        /// 耗时
        /// </summary>
        public double time { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string projectname { get; set; }

        /// <summary>
        /// 耗时日志类型：普通日志=0，api接口日志=1，sql日志=2
        /// </summary>
        public Byte logtype { get; set; }

        /// <summary>
        /// 日志标识,sql类型则为sql哈希,api类型则为url
        /// </summary>
        public int logtag { get; set; }

        /// <summary>
        /// 当前url
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 当前信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 来源ip(代码执行ip)
        /// </summary>
        public string fromip { get; set; }

        /// <summary>
        /// sqlip地址
        /// </summary>
        public string sqlip { get; set; }

        /// <summary>
        /// 其他记录标记信息
        /// </summary>
        public string remark { get; set; }

    }
}