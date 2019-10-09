using System;

using System.Collections.Generic;
using System.Data;
using System.Text;
using TaskManager.Utils.Extensions;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Domain.Model;

namespace TaskManager.Domain.Dal
{
	/*代码自动生成工具自动生成,不要在这里写自己的代码，否则会被自动覆盖哦 - 车毅*/
	public partial class tb_tempdata_dal
    {
        public virtual bool Add(DbConn conn, tb_tempdata_model model)
        {

            List<ProcedureParameter> Par = new List<ProcedureParameter>()
                {
					
					//
					new ProcedureParameter("@taskid",    model.taskid),
					//
					new ProcedureParameter("@tempdatajson",    model.tempdatajson),
					//
					new ProcedureParameter("@tempdatalastupdatetime",    model.tempdatalastupdatetime)   
                };
            int rev = conn.ExecuteSql(@"insert into tb_tempdata(taskid,tempdatajson,tempdatalastupdatetime)
										   values(@taskid,@tempdatajson,@tempdatalastupdatetime)", Par);
            return rev == 1;

        }

        public virtual bool Edit(DbConn conn, tb_tempdata_model model)
        {
            List<ProcedureParameter> Par = new List<ProcedureParameter>()
            {
                    
					//
					new ProcedureParameter("@taskid",    model.taskid),
					//
					new ProcedureParameter("@tempdatajson",    model.tempdatajson),
					//
					new ProcedureParameter("@tempdatalastupdatetime",    model.tempdatalastupdatetime)
            };
			Par.Add(new ProcedureParameter("@id",  model.id));

            int rev = conn.ExecuteSql("update tb_tempdata set taskid=@taskid,tempdatajson=@tempdatajson,tempdatalastupdatetime=@tempdatalastupdatetime where id=@id", Par);
            return rev == 1;

        }

        public virtual bool Delete(DbConn conn, int id)
        {
            List<ProcedureParameter> Par = new List<ProcedureParameter>();
            Par.Add(new ProcedureParameter("@id",  id));

            string Sql = "delete from tb_tempdata where id=@id";
            int rev = conn.ExecuteSql(Sql, Par);
            if (rev == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public virtual tb_tempdata_model Get(DbConn conn, int id)
        {
            List<ProcedureParameter> Par = new List<ProcedureParameter>();
            Par.Add(new ProcedureParameter("@id", id));
            StringBuilder stringSql = new StringBuilder();
            stringSql.Append(@"select s.* from tb_tempdata s where s.id=@id");
            DataSet ds = new DataSet();
            conn.SqlToDataSet(ds, stringSql.ToString(), Par);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
				return CreateModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }

		public virtual tb_tempdata_model CreateModel(DataRow dr)
        {
            var o = new tb_tempdata_model();
			
			//
			if(dr.Table.Columns.Contains("id"))
			{
				o.id = dr["id"].Toint();
			}
			//
			if(dr.Table.Columns.Contains("taskid"))
			{
				o.taskid = dr["taskid"].Toint();
			}
			//
			if(dr.Table.Columns.Contains("tempdatajson"))
			{
				o.tempdatajson = dr["tempdatajson"].Tostring();
			}
			//
			if(dr.Table.Columns.Contains("tempdatalastupdatetime"))
			{
				o.tempdatalastupdatetime = dr["tempdatalastupdatetime"].ToDateTime();
			}
			return o;
        }
    }
}