using System;

using System.Collections.Generic;
using System.Data;
using System.Text;
using TaskManager.Utils.Extensions;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Utils.BaseService.Monitor.Model;

namespace TaskManager.Utils.BaseService.Monitor.Dal
{
	public partial class tb_timewatchlog_dal
    {

        public virtual tb_timewatchlog_model CreateModel(DataRow dr)
        {
            var o = new tb_timewatchlog_model();

            //
            if (dr.Table.Columns.Contains("id"))
            {
                o.id = dr["id"].Toint();
            }
            //数据库本地创建时间
            if (dr.Table.Columns.Contains("sqlservercreatetime"))
            {
                o.sqlservercreatetime = dr["sqlservercreatetime"].ToDateTime();
            }
            //日志创建时间
            if (dr.Table.Columns.Contains("logcreatetime"))
            {
                o.logcreatetime = dr["logcreatetime"].ToDateTime();
            }
            //耗时
            if (dr.Table.Columns.Contains("time"))
            {
                o.time = dr["time"].Todouble();
            }
            //项目名称
            if (dr.Table.Columns.Contains("projectname"))
            {
                o.projectname = dr["projectname"].Tostring();
            }
            //耗时日志类型：普通日志=0，api接口日志=1，sql日志=2
            if (dr.Table.Columns.Contains("logtype"))
            {
                o.logtype = dr["logtype"].ToByte();
            }
            //日志标识,sql类型则为sql哈希,api类型则为url
            if (dr.Table.Columns.Contains("logtag"))
            {
                o.logtag = dr["logtag"].Toint();
            }
            //当前url
            if (dr.Table.Columns.Contains("url"))
            {
                o.url = dr["url"].Tostring();
            }
            //当前信息
            if (dr.Table.Columns.Contains("msg"))
            {
                o.msg = dr["msg"].Tostring();
            }
            //来源ip(代码执行ip)
            if (dr.Table.Columns.Contains("fromip"))
            {
                o.fromip = dr["fromip"].Tostring();
            }
            //sqlip地址
            if (dr.Table.Columns.Contains("sqlip"))
            {
                o.sqlip = dr["sqlip"].Tostring();
            }
            //其他记录标记信息
            if (dr.Table.Columns.Contains("remark"))
            {
                o.remark = dr["remark"].Tostring();
            }
            return o;
        }
    }
}