using System;

using System.Collections.Generic;
using System.Data;
using System.Text;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Utils.Extensions;
using TaskManager.Domain.Model;

namespace TaskManager.Domain.Dal
{
	/*�����Զ����ɹ����Զ�����,��Ҫ������д�Լ��Ĵ��룬����ᱻ�Զ�����Ŷ - ����*/
	public partial class tb_category_dal
    {
        public virtual bool Add(DbConn conn, tb_category_model model)
        {

            List<ProcedureParameter> Par = new List<ProcedureParameter>()
                {
					
					//������
					new ProcedureParameter("@categoryname",    model.categoryname),
					//���ഴ��ʱ��
					new ProcedureParameter("@categorycreatetime",    model.categorycreatetime)   
                };
            int rev = conn.ExecuteSql(@"insert into tb_category(categoryname,categorycreatetime)
										   values(@categoryname,@categorycreatetime)", Par);
            return rev == 1;

        }

        public virtual bool Edit(DbConn conn, tb_category_model model)
        {
            List<ProcedureParameter> Par = new List<ProcedureParameter>()
            {
                    
					//������
					new ProcedureParameter("@categoryname",    model.categoryname),
					//���ഴ��ʱ��
					new ProcedureParameter("@categorycreatetime",    model.categorycreatetime)
            };
			Par.Add(new ProcedureParameter("@id",  model.id));

            int rev = conn.ExecuteSql("update tb_category set categoryname=@categoryname,categorycreatetime=@categorycreatetime where id=@id", Par);
            return rev == 1;

        }

        public virtual bool Delete(DbConn conn, int id)
        {
            List<ProcedureParameter> Par = new List<ProcedureParameter>();
            Par.Add(new ProcedureParameter("@id",  id));

            string Sql = "delete from tb_category where id=@id";
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

        public virtual tb_category_model Get(DbConn conn, int id)
        {
            List<ProcedureParameter> Par = new List<ProcedureParameter>();
            Par.Add(new ProcedureParameter("@id", id));
            StringBuilder stringSql = new StringBuilder();
            stringSql.Append(@"select s.* from tb_category s where s.id=@id");
            DataSet ds = new DataSet();
            conn.SqlToDataSet(ds, stringSql.ToString(), Par);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
				return CreateModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }

		public virtual tb_category_model CreateModel(DataRow dr)
        {
            var o = new tb_category_model();
			
			//
			if(dr.Table.Columns.Contains("id"))
			{
				o.id = dr["id"].Toint();
			}
			//������
			if(dr.Table.Columns.Contains("categoryname"))
			{
				o.categoryname = dr["categoryname"].Tostring();
			}
			//���ഴ��ʱ��
			if(dr.Table.Columns.Contains("categorycreatetime"))
			{
				o.categorycreatetime = dr["categorycreatetime"].ToDateTime();
			}
			return o;
        }
    }
}