using TaskManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;


namespace TaskManager.Domain.Dal
{
    public partial class tb_category_dal
    {
        public virtual bool Add(DbConn conn, string categoryname)
        {

            List<ProcedureParameter> Par = new List<ProcedureParameter>()
                {
					new ProcedureParameter("@categoryname",    categoryname),
                };
            int rev = conn.ExecuteSql(@"insert into tb_category(categoryname,categorycreatetime)
										   values(@categoryname,getdate())", Par);
            return rev == 1;
        }

        public bool Update(DbConn conn, tb_category_model model)
        {
            List<ProcedureParameter> Par = new List<ProcedureParameter>()
                {
					new ProcedureParameter("@categoryname",    model.categoryname),
                    new ProcedureParameter("@id",    model.id),
                };
            int rev = conn.ExecuteSql(@"update tb_category set categoryname=@categoryname where id=@id", Par);
            return rev == 1;
        }

        public List<tb_category_model> GetList(DbConn conn,string keyword)
        {
            return SqlHelper.Visit(ps =>
            {
                string sql = "select id,categoryname,categorycreatetime from tb_category ";
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    ps.Add("keyword", keyword);
                    sql += "where categoryname like '%'+@keyword+'%'";
                }
                DataSet ds = new DataSet();
                conn.SqlToDataSet(ds, sql, ps.ToParameters());
                List<tb_category_model> Model = new List<tb_category_model>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    tb_category_model m = CreateModel(dr);
                    Model.Add(m);
                }
                return Model;
            });
        }

        public bool DeleteOneNode(DbConn conn, int id)
        {
            return SqlHelper.Visit<bool>(ps =>
            {
                ps.Add("id", id);
                string sql = "delete from tb_category where (select count(1) from tb_task where categoryid=@id)=0 and id=@id";
                int i = conn.ExecuteSql(sql, ps.ToParameters());
                return i > 0;
            });
        }
    }
}
