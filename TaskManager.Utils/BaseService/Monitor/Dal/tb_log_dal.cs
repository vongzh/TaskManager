using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TaskManager.Utils.Extensions;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Utils.BaseService.Monitor.Model;

namespace TaskManager.Utils.BaseService.Monitor.Dal
{
	public partial class tb_log_dal
    {
        public virtual bool Add(DbConn conn, tb_log_model model)
        {

            List<ProcedureParameter> Par = new List<ProcedureParameter>()
                {
					
					//数据库创建时间
					//new ProcedureParameter("@sqlservercreatetime",    model.sqlservercreatetime),
					//日志项目中创建时间
					new ProcedureParameter("@logcreatetime",    model.logcreatetime),
					//日志类型:一般非正常错误,系统级严重错误,一般业务日志,系统日志
					new ProcedureParameter("@logtype",    model.logtype),
					//项目名称
					new ProcedureParameter("@projectname",    model.projectname),
					//日志唯一标示(简短的方法名或者url,便于归类)
					new ProcedureParameter("@logtag",    model.logtag),
					//日志信息
					new ProcedureParameter("@msg",    model.msg)   
                };
            int rev = conn.ExecuteSql(string.Format(@"insert into tb_log{0}(sqlservercreatetime,logcreatetime,logtype,projectname,logtag,msg)
										   values(getdate(),@logcreatetime,@logtype,@projectname,@logtag,@msg)", TaskManager.Utils.BaseService.Monitor.SystemRuntime.DbShardingHelper.MonthRule(DateTime.Now)), Par);
            return rev == 1;

        }
		public virtual tb_log_model CreateModel(DataRow dr)
        {
            var o = new tb_log_model();
			
			//
			if(dr.Table.Columns.Contains("id"))
			{
				o.id = dr["id"].Toint();
			}
			//数据库创建时间
			if(dr.Table.Columns.Contains("sqlservercreatetime"))
			{
				o.sqlservercreatetime = dr["sqlservercreatetime"].ToDateTime();
			}
			//日志项目中创建时间
			if(dr.Table.Columns.Contains("logcreatetime"))
			{
				o.logcreatetime = dr["logcreatetime"].ToDateTime();
			}
			//日志类型:一般非正常错误,系统级严重错误,一般业务日志,系统日志
			if(dr.Table.Columns.Contains("logtype"))
			{
				o.logtype = dr["logtype"].ToByte();
			}
			//项目名称
			if(dr.Table.Columns.Contains("projectname"))
			{
				o.projectname = dr["projectname"].Tostring();
			}
			//日志唯一标示(简短的方法名或者url,便于归类)
			if(dr.Table.Columns.Contains("logtag"))
			{
				o.logtag = dr["logtag"].Tostring();
			}
			//日志信息
			if(dr.Table.Columns.Contains("msg"))
			{
				o.msg = dr["msg"].Tostring();
			}
			return o;
        }
    }
}