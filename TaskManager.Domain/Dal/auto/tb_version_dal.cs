using System;

using System.Collections.Generic;
using System.Data;
using System.Text;
using TaskManager.Utils.Extensions;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Domain.Model;

namespace TaskManager.Domain.Dal
{
	/*�����Զ����ɹ����Զ�����,��Ҫ������д�Լ��Ĵ��룬����ᱻ�Զ�����Ŷ - ����*/
	public partial class tb_version_dal
    {
        public virtual bool Add(DbConn conn, tb_version_model model)
        {

            List<ProcedureParameter> Par = new List<ProcedureParameter>()
                {
					
					//
					new ProcedureParameter("@taskid",    model.taskid),
					//
					new ProcedureParameter("@version",    model.version),
					//
					new ProcedureParameter("@versioncreatetime",    model.versioncreatetime),
					//ѹ���ļ��������ļ�
					new ProcedureParameter("@zipfile",    model.zipfile),
					//
					new ProcedureParameter("@zipfilename",    model.zipfilename)   
                };
            int rev = conn.ExecuteSql(@"insert into tb_version(taskid,version,versioncreatetime,zipfile,zipfilename)
										   values(@taskid,@version,@versioncreatetime,@zipfile,@zipfilename)", Par);
            return rev == 1;

        }

        public virtual bool Edit(DbConn conn, tb_version_model model)
        {
            List<ProcedureParameter> Par = new List<ProcedureParameter>()
            {
                    
					//
					new ProcedureParameter("@taskid",    model.taskid),
					//
					new ProcedureParameter("@version",    model.version),
					//
					new ProcedureParameter("@versioncreatetime",    model.versioncreatetime),
					//ѹ���ļ��������ļ�
					new ProcedureParameter("@zipfile",    model.zipfile),
					//
					new ProcedureParameter("@zipfilename",    model.zipfilename)
            };
			Par.Add(new ProcedureParameter("@id",  model.id));

            int rev = conn.ExecuteSql("update tb_version set taskid=@taskid,version=@version,versioncreatetime=@versioncreatetime,zipfile=@zipfile,zipfilename=@zipfilename where id=@id", Par);
            return rev == 1;

        }

        public virtual bool Delete(DbConn conn, int id)
        {
            List<ProcedureParameter> Par = new List<ProcedureParameter>();
            Par.Add(new ProcedureParameter("@id",  id));

            string Sql = "delete from tb_version where id=@id";
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

        public virtual tb_version_model Get(DbConn conn, int id)
        {
            List<ProcedureParameter> Par = new List<ProcedureParameter>();
            Par.Add(new ProcedureParameter("@id", id));
            StringBuilder stringSql = new StringBuilder();
            stringSql.Append(@"select s.* from tb_version s where s.id=@id");
            DataSet ds = new DataSet();
            conn.SqlToDataSet(ds, stringSql.ToString(), Par);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
				return CreateModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }

		public virtual tb_version_model CreateModel(DataRow dr)
        {
            var o = new tb_version_model();
			
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
			if(dr.Table.Columns.Contains("version"))
			{
				o.version = dr["version"].Toint();
			}
			//
			if(dr.Table.Columns.Contains("versioncreatetime"))
			{
				o.versioncreatetime = dr["versioncreatetime"].ToDateTime();
			}
			//ѹ���ļ��������ļ�
			if(dr.Table.Columns.Contains("zipfile"))
			{
				o.zipfile = dr["zipfile"].ToBytes();
			}
			//
			if(dr.Table.Columns.Contains("zipfilename"))
			{
				o.zipfilename = dr["zipfilename"].Tostring();
			}
			return o;
        }
    }
}