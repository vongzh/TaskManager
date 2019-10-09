using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
namespace TaskManager.Utils.Db
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public static class DbConfig
    {

        /// <summary>取得配置信息</summary>
        /// <param name="Name">配置名称</param>
        /// <returns></returns>
        public static string GetConfig(string Name)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(Name))
            {
                return ConfigurationManager.AppSettings[Name];
            }
            else
            {
                return string.Empty;
            }
           // return ConfigManagerHelper.Get<string>(Name);
            //return LibConvert.NullToStr(ConfigurationManager.AppSettings[Name]);
        }

        /// <summary>数据库类型</summary>
        public static Db.DbType DbType
        {
            get
            {
                return (Db.DbType)LibConvert.StrToInt(GetConfig("DbType"));
            }
        }

        /// <summary>连接字符串</summary>
        public static string ConnectionString
        {
            get
            {
                /*兼容ConnectionString单条配置或者原来的多项配置方式*/
                if (!string.IsNullOrEmpty(GetConfig("DbServer")))
                {
                    string Temple = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Min Pool Size=10;Max Pool Size=200;";
                    return string.Format(Temple, GetConfig("DbServer"), GetConfig("DbName"), GetConfig("DbUser"), GetConfig("DbPass"));
                }
                else
                {
                    return GetConfig("ConnectionString");
                }
            }
        }

        /// <summary>创建数据库连接</summary>
        /// <returns></returns>
        public static Db.DbConn CreateConn()
        {
            return Db.DbConn.CreateConn(DbType, ConnectionString);
        }

        /// <summary>创建数据库连接</summary>
        /// <returns></returns>
        public static Db.DbConn CreateConn(DbType dbtype, string connectionString)
        {
            return Db.DbConn.CreateConn(dbtype, connectionString);
        }

        /// <summary>创建数据库连接</summary>
        /// <returns></returns>
        public static Db.DbConn CreateConn(string connectionString)
        {
            if (connectionString.StartsWith("mysql;", StringComparison.CurrentCultureIgnoreCase))
            {
                return Db.DbConn.CreateConn(DbType.MYSQL, connectionString.Remove(0, "mysql;".Length));
            }
            if (connectionString.ToUpper().IndexOf("DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)") >= 0)
            {
                return CreateConn(DbType.ORACLE_NEW, connectionString);

            }
            return Db.DbConn.CreateConn(DbType.SQLSERVER, connectionString);
        }
    }
}
