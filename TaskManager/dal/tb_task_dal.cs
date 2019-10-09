using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;


namespace TaskManager.dal
{
    public class tb_task_dal
    {

        public int UpdateLastStartTime(DbConn conn, int id, DateTime time)
        {
            return SqlHelper.Visit(ps =>
            {
                string cmd = "update tb_task set tasklaststarttime=@tasklaststarttime where id=@id";
                ps.Add("id", id);
                ps.Add("tasklaststarttime", time);
                return conn.ExecuteSql(cmd, ps.ToParameters());
            });
        }

        public int UpdateLastEndTime(DbConn conn, int id, DateTime time)
        {
            return SqlHelper.Visit(ps =>
            {
                string cmd = "update tb_task set tasklastendtime=@tasklastendtime where id=@id";
                ps.Add("id", id);
                ps.Add("tasklastendtime", time);
                return conn.ExecuteSql(cmd, ps.ToParameters());
            });
        }

        public int UpdateTaskError(DbConn conn, int id, DateTime time)
        {
            return SqlHelper.Visit(ps =>
            {
                string cmd = "update tb_task set taskerrorcount=taskerrorcount+1,tasklasterrortime=@tasklasterrortime where id=@id";
                ps.Add("id", id);
                ps.Add("tasklasterrortime", time);
                return conn.ExecuteSql(cmd, ps.ToParameters());
            });
        }

        public int UpdateTaskSuccess(DbConn conn, int id)
        {
            return SqlHelper.Visit(ps =>
            {
                string cmd = "update tb_task set taskerrorcount=0,taskruncount=taskruncount+1 where id=@id";
                ps.Add("id", id);
                return conn.ExecuteSql(cmd, ps.ToParameters());
            });
        }
    }
}
