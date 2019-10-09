using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TaskManager.Utils.Extensions;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Utils.BaseService.Monitor.Model;

namespace TaskManager.Utils.BaseService.Monitor.Dal
{
	/*代码自动生成工具自动生成,不要在这里写自己的代码，否则会被自动覆盖哦 - 车毅*/
	public partial class tb_timewatchlog_api_dal
    {
       

		public virtual tb_timewatchlog_api_model CreateModel(DataRow dr)
        {
            var o = new tb_timewatchlog_api_model();
			
			//
			if(dr.Table.Columns.Contains("id"))
			{
				o.id = dr["id"].Toint();
			}
			//
			if(dr.Table.Columns.Contains("sqlservercreatetime"))
			{
				o.sqlservercreatetime = dr["sqlservercreatetime"].ToDateTime();
			}
			//
			if(dr.Table.Columns.Contains("logcreatetime"))
			{
				o.logcreatetime = dr["logcreatetime"].ToDateTime();
			}
			//
			if(dr.Table.Columns.Contains("time"))
			{
				o.time = dr["time"].Todouble();
			}
			//
			if(dr.Table.Columns.Contains("projectname"))
			{
				o.projectname = dr["projectname"].Tostring();
			}
			//
			if(dr.Table.Columns.Contains("url"))
			{
				o.url = dr["url"].Tostring();
			}
			//
			if(dr.Table.Columns.Contains("msg"))
			{
				o.msg = dr["msg"].Tostring();
			}
			//
			if(dr.Table.Columns.Contains("fromip"))
			{
				o.fromip = dr["fromip"].Tostring();
			}
			//
			if(dr.Table.Columns.Contains("tag"))
			{
				o.tag = dr["tag"].Tostring();
			}
			return o;
        }
    }
}