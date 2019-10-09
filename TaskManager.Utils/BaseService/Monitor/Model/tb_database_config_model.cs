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
	/*代码自动生成工具自动生成,不要在这里写自己的代码，否则会被自动覆盖哦 - 车毅*/
        
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        
        /// <summary>
        /// 数据库本地昵称
        /// </summary>
        public string dblocalname { get; set; }
        
        /// <summary>
        /// 数据库服务器地址
        /// </summary>
        public string dbserver { get; set; }
        
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string dbname { get; set; }
        
        /// <summary>
        /// 数据库用户名
        /// </summary>
        public string dbuser { get; set; }
        
        /// <summary>
        /// 数据库密码
        /// </summary>
        public string dbpass { get; set; }
        
        /// <summary>
        /// 数据库类型
        /// </summary>
        public Byte dbtype { get; set; }
        
    }
}