using System;

using System.Collections.Generic;
using System.Data;
using System.Text;
using TaskManager.Utils.Extensions;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.model;


namespace TaskManager.dal
{
	/*代码自动生成工具自动生成,不要在这里写自己的代码，否则会被自动覆盖哦 - 车毅*/
	public partial class tb_error_dal
    {
        public int Add(DbConn conn, tb_error_model model)
        {
            return SqlHelper.Visit(ps =>
            {
                ps.Add("@msg", model.msg);
                ps.Add("@errortype", model.errortype);
                ps.Add("@errorcreatetime", model.errorcreatetime);
                ps.Add("@taskid", model.taskid);
                ps.Add("@nodeid", model.nodeid);
                return conn.ExecuteSql(@"insert into tb_error(msg,errortype,errorcreatetime,taskid,nodeid)
										   values(@msg,@errortype,@errorcreatetime,@taskid,@nodeid)", ps.ToParameters()) ;
            });
        }
    }
}