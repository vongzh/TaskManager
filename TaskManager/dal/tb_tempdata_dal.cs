using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;


namespace TaskManager.dal
{
    public class tb_tempdata_dal
    {
        public virtual int SaveTempData(DbConn conn,int taskid,string json)
        {
            return SqlHelper.Visit(ps =>
            {
                string cmd = "update tb_tempdata set tempdatajson=@tempdatajson where taskid=@taskid";
                ps.Add("taskid", taskid);
                ps.Add("tempdatajson",json);
                return conn.ExecuteSql(cmd, ps.ToParameters());
            });
        }

        public virtual string GetTempData(DbConn conn, int taskid)
        {
            return SqlHelper.Visit(ps =>
            {
                StringBuilder stringSql = new StringBuilder();
                stringSql.Append(@"select s.* from tb_tempdata s where s.taskid=@taskid");
                ps.Add("taskid", taskid);
                DataSet ds = new DataSet();
                conn.SqlToDataSet(ds, stringSql.ToString(), ps.ToParameters());
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToString( ds.Tables[0].Rows[0]["tempdatajson"]);
                }
                return null;
            });
        }
    }
}
