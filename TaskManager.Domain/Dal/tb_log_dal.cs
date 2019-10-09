using System;

using System.Collections.Generic;
using System.Data;
using System.Text;
using TaskManager.Utils.Extensions;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Domain.Model;


namespace TaskManager.Domain.Dal
{
	public partial class tb_log_dal
    {
        public int Add2(DbConn conn, tb_log_model model)
        {
            return SqlHelper.Visit(ps =>
            {
                ps.Add("@msg", model.msg);
                ps.Add("@logtype", model.logtype);
                ps.Add("@logcreatetime", model.logcreatetime);
                ps.Add("@taskid", model.taskid);
                ps.Add("@nodeid", model.nodeid);
                return conn.ExecuteSql(@"insert into tb_log(msg,logtype,logcreatetime,taskid,nodeid)
										   values(@msg,@logtype,@logcreatetime,@taskid,@nodeid)", ps.ToParameters());
            });
        }

        public List<tb_loginfo_model> GetList(DbConn conn, string keyword, int id, string cstime, string cetime, int logtype, int taskid, int nodeid, int pagesize, int pageindex, out int count)
        {
            List<tb_loginfo_model> model = new List<tb_loginfo_model>();
            int _count = 0;
            DataSet dsList = SqlHelper.Visit<DataSet>(ps =>
            {
                string sqlwhere = "";
                string sql = "select ROW_NUMBER() over(order by E.id desc) as rownum,E.*,ISNULL(T.taskcreateuserid,0) taskcreateuserid, u.username as taskusername,T.taskname,n.nodename as tasknodename from tb_log E left join tb_task T on E.taskid=T.id left join tb_user u on t.taskcreateuserid=u.id left join tb_node n on n.id=E.nodeid where 1=1 ";
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    ps.Add("keyword", keyword);
                    sqlwhere = " and msg like '%'+@keyword+'%' ";
                }
                if (taskid != -1)
                {
                    ps.Add("taskid", taskid);
                    sqlwhere = " and E.taskid=@taskid";
                }
                if (nodeid != -1)
                {
                    ps.Add("nodeid", nodeid);
                    sqlwhere = " and E.nodeid=@nodeid";
                }
                if (id != -1)
                {
                    ps.Add("id", id);
                    sqlwhere = " and E.id=@id";
                }
                DateTime d = DateTime.Now;
                if (DateTime.TryParse(cstime, out d))
                {
                    ps.Add("CStime", Convert.ToDateTime(cstime));
                    sqlwhere += " and E.logcreatetime>=@CStime";
                }
                if (DateTime.TryParse(cetime, out d))
                {
                    ps.Add("CEtime", Convert.ToDateTime(cetime));
                    sqlwhere += " and E.logcreatetime<=@CEtime";
                }
                if (logtype != -1)
                {
                    ps.Add("logtype", logtype);
                    sqlwhere += " and E.logtype=@logtype";
                }
                _count = Convert.ToInt32(conn.ExecuteScalar("select count(1) from tb_log E where 1=1 " + sqlwhere, ps.ToParameters()));
                DataSet ds = new DataSet();
                string sqlSel = "select * from (" + sql + sqlwhere + ") A where rownum between " + ((pageindex - 1) * pagesize + 1) + " and " + pagesize * pageindex;
                conn.SqlToDataSet(ds, sqlSel, ps.ToParameters());
                return ds;
            });
            foreach (DataRow dr in dsList.Tables[0].Rows)
            {
                tb_loginfo_model m = new tb_loginfo_model();
                m.log_model = CreateModel(dr);
                m.taskusername = Convert.ToString(dr["taskusername"]);
                m.taskname = Convert.ToString(dr["taskname"]);
                model.Add(m);
            }
            count = _count;
            return model;
        }
    }
}