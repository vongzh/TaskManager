using System;

using System.Collections.Generic;
using System.Data;
using System.Text;
using TaskManager.Utils.Extensions;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Domain.Model;


namespace TaskManager.Domain.Dal
{
    public partial class tb_tempdata_dal
    {
        public int UpdateByTaskID(DbConn conn, tb_tempdata_model model)
        {
            return SqlHelper.Visit<int>(ps =>
            {
                ps.Add("@taskid", model.taskid);
                ps.Add("@tempdatajson", model.tempdatajson);
                ps.Add("@tempdatalastupdatetime", model.tempdatalastupdatetime);
                ps.Add("@id", model.id);

                int rev = conn.ExecuteSql("update tb_tempdata set tempdatajson=@tempdatajson,tempdatalastupdatetime=@tempdatalastupdatetime where taskid=@taskid", ps.ToParameters());
                return rev;
            });
        }

        public tb_tempdata_model GetByTaskID(DbConn conn, int taskid)
        {
            return SqlHelper.Visit(ps =>
            {
                ps.Add("taskid", taskid);
                string sql = "select * from tb_tempdata where taskid=@taskid";
                DataSet ds = new DataSet();
                conn.SqlToDataSet(ds, sql, ps.ToParameters());
                tb_tempdata_model model = CreateModel(ds.Tables[0].Rows[0]);
                return model;
            });
        }
    }
}