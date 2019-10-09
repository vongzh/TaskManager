using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.model;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;


namespace TaskManager.dal
{
    public class tb_log_dal
    {
        public int Add(DbConn conn, tb_log_model model)
        {
            return SqlHelper.Visit(ps =>
            {
					ps.Add("@msg",    model.msg);
					ps.Add("@logtype",    model.logtype);
					ps.Add("@logcreatetime",    model.logcreatetime);
                    ps.Add("@taskid", model.taskid);
                    ps.Add("@nodeid", model.nodeid);
                return conn.ExecuteSql(@"insert into tb_log(msg,logtype,logcreatetime,taskid,nodeid)
										   values(@msg,@logtype,@logcreatetime,@taskid,@nodeid)", ps.ToParameters());
            });
        }
    }
}
