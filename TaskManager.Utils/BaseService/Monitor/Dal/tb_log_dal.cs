using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TaskManager.Utils.Extensions;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Utils.BaseService.Monitor.Model;

namespace TaskManager.Utils.BaseService.Monitor.Dal
{
	public partial class tb_log_dal
    {
        public virtual bool Add(DbConn conn, tb_log_model model)
        {

            List<ProcedureParameter> Par = new List<ProcedureParameter>()
                {
					
					//���ݿⴴ��ʱ��
					//new ProcedureParameter("@sqlservercreatetime",    model.sqlservercreatetime),
					//��־��Ŀ�д���ʱ��
					new ProcedureParameter("@logcreatetime",    model.logcreatetime),
					//��־����:һ�����������,ϵͳ�����ش���,һ��ҵ����־,ϵͳ��־
					new ProcedureParameter("@logtype",    model.logtype),
					//��Ŀ����
					new ProcedureParameter("@projectname",    model.projectname),
					//��־Ψһ��ʾ(��̵ķ���������url,���ڹ���)
					new ProcedureParameter("@logtag",    model.logtag),
					//��־��Ϣ
					new ProcedureParameter("@msg",    model.msg)   
                };
            int rev = conn.ExecuteSql(string.Format(@"insert into tb_log{0}(sqlservercreatetime,logcreatetime,logtype,projectname,logtag,msg)
										   values(getdate(),@logcreatetime,@logtype,@projectname,@logtag,@msg)", TaskManager.Utils.BaseService.Monitor.SystemRuntime.DbShardingHelper.MonthRule(DateTime.Now)), Par);
            return rev == 1;

        }
		public virtual tb_log_model CreateModel(DataRow dr)
        {
            var o = new tb_log_model();
			
			//
			if(dr.Table.Columns.Contains("id"))
			{
				o.id = dr["id"].Toint();
			}
			//���ݿⴴ��ʱ��
			if(dr.Table.Columns.Contains("sqlservercreatetime"))
			{
				o.sqlservercreatetime = dr["sqlservercreatetime"].ToDateTime();
			}
			//��־��Ŀ�д���ʱ��
			if(dr.Table.Columns.Contains("logcreatetime"))
			{
				o.logcreatetime = dr["logcreatetime"].ToDateTime();
			}
			//��־����:һ�����������,ϵͳ�����ش���,һ��ҵ����־,ϵͳ��־
			if(dr.Table.Columns.Contains("logtype"))
			{
				o.logtype = dr["logtype"].ToByte();
			}
			//��Ŀ����
			if(dr.Table.Columns.Contains("projectname"))
			{
				o.projectname = dr["projectname"].Tostring();
			}
			//��־Ψһ��ʾ(��̵ķ���������url,���ڹ���)
			if(dr.Table.Columns.Contains("logtag"))
			{
				o.logtag = dr["logtag"].Tostring();
			}
			//��־��Ϣ
			if(dr.Table.Columns.Contains("msg"))
			{
				o.msg = dr["msg"].Tostring();
			}
			return o;
        }
    }
}