using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TaskManager.Utils.Extensions;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;

using TaskManager.Utils.BaseService.Monitor.Model;
using TaskManager.Utils.Common;

namespace TaskManager.Utils.BaseService.Monitor.Dal
{
	public partial class tb_database_config_dal
    {
        public List<tb_database_config_model> GetModelList(DbConn conn)
        {
            return SqlHelper.Visit(ps =>
            {
                string cmd = "select * from tb_database_config";
                DataSet ds = new DataSet();
                conn.SqlToDataSet(ds, cmd, ps.ToParameters());
                List<tb_database_config_model> rs = new List<tb_database_config_model>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                        rs.Add(CreateModel(dr));
                }
                return rs;
            });
        }

        public virtual tb_database_config_model CreateModel(DataRow dr)
        {
            var o = new tb_database_config_model();

            //
            if (dr.Table.Columns.Contains("id"))
            {
                o.id = dr["id"].Toint();
            }
            //数据库本地昵称
            if (dr.Table.Columns.Contains("dblocalname"))
            {
                o.dblocalname = dr["dblocalname"].Tostring();
            }
            //数据库服务器地址
            if (dr.Table.Columns.Contains("dbserver"))
            {
                o.dbserver = dr["dbserver"].Tostring();
            }
            //数据库名称
            if (dr.Table.Columns.Contains("dbname"))
            {
                o.dbname = dr["dbname"].Tostring();
            }
            //数据库用户名
            if (dr.Table.Columns.Contains("dbuser"))
            {
                o.dbuser = dr["dbuser"].Tostring();
            }
            //数据库密码
            if (dr.Table.Columns.Contains("dbpass"))
            {
                o.dbpass = dr["dbpass"].Tostring();
            }
            //数据库类型
            if (dr.Table.Columns.Contains("dbtype"))
            {
                o.dbtype = dr["dbtype"].ToByte();
            }
            return o;
        }
    }
}