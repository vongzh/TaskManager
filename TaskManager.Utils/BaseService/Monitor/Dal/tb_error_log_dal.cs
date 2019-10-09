using System;

using System.Collections.Generic;
using System.Data;
using System.Text;
using TaskManager.Utils.Extensions;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Utils.BaseService.Monitor.Model;

namespace TaskManager.Utils.BaseService.Monitor.Dal
{
	public partial class tb_error_log_dal
    {
        public virtual bool Add(DbConn conn, tb_error_log_model model)
        {

            List<ProcedureParameter> Par = new List<ProcedureParameter>() 
                {
					
					//���ݿⴴ��ʱ��
					//new ProcedureParameter("@sqlservercreatetime",    model.sqlservercreatetime),
					//��־��Ŀ�д���ʱ��
					new ProcedureParameter("@logcreatetime",    model.logcreatetime),
					//��־����:һ�����������,ϵͳ�����ش���
					new ProcedureParameter("@logtype",    model.logtype),
					//��Ŀ����
					new ProcedureParameter("@projectname",    model.projectname),
					//��־Ψһ��ʾ(��̵ķ���������url,���ڹ���)
					new ProcedureParameter("@logtag",    model.logtag),
					//������Ϣ
					new ProcedureParameter("@msg",    model.msg),
					//��ջ����
					new ProcedureParameter("@tracestack",    model.tracestack),
					//������ע��Ϣ
					new ProcedureParameter("@remark",    model.remark),
					//��ؿ�����Ա
					new ProcedureParameter("@developer",    model.developer)   
                };
            int rev = conn.ExecuteSql(string.Format(@"insert into tb_error_log{0}(sqlservercreatetime,logcreatetime,logtype,projectname,logtag,msg,tracestack,remark,developer)
										   values(getdate(),@logcreatetime,@logtype,@projectname,@logtag,@msg,@tracestack,@remark,@developer)", TaskManager.Utils.BaseService.Monitor.SystemRuntime.DbShardingHelper.MonthRule(DateTime.Now)), Par);
            return rev == 1;

        }

		public virtual tb_error_log_model CreateModel(DataRow dr)
        {
            var o = new tb_error_log_model();
			
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
			//��־����:һ�����������,ϵͳ�����ش���
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
			//������Ϣ
			if(dr.Table.Columns.Contains("msg"))
			{
				o.msg = dr["msg"].Tostring();
			}
			//��ջ����
			if(dr.Table.Columns.Contains("tracestack"))
			{
				o.tracestack = dr["tracestack"].Tostring();
			}
			//������ע��Ϣ
			if(dr.Table.Columns.Contains("remark"))
			{
				o.remark = dr["remark"].Tostring();
			}
			//��ؿ�����Ա
			if(dr.Table.Columns.Contains("developer"))
			{
				o.developer = dr["developer"].Tostring();
			}
			return o;
        }
    }
}