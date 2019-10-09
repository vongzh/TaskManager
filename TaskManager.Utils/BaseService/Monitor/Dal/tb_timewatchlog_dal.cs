using System;

using System.Collections.Generic;
using System.Data;
using System.Text;
using TaskManager.Utils.Extensions;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Utils.BaseService.Monitor.Model;

namespace TaskManager.Utils.BaseService.Monitor.Dal
{
	public partial class tb_timewatchlog_dal
    {

        public virtual tb_timewatchlog_model CreateModel(DataRow dr)
        {
            var o = new tb_timewatchlog_model();

            //
            if (dr.Table.Columns.Contains("id"))
            {
                o.id = dr["id"].Toint();
            }
            //���ݿⱾ�ش���ʱ��
            if (dr.Table.Columns.Contains("sqlservercreatetime"))
            {
                o.sqlservercreatetime = dr["sqlservercreatetime"].ToDateTime();
            }
            //��־����ʱ��
            if (dr.Table.Columns.Contains("logcreatetime"))
            {
                o.logcreatetime = dr["logcreatetime"].ToDateTime();
            }
            //��ʱ
            if (dr.Table.Columns.Contains("time"))
            {
                o.time = dr["time"].Todouble();
            }
            //��Ŀ����
            if (dr.Table.Columns.Contains("projectname"))
            {
                o.projectname = dr["projectname"].Tostring();
            }
            //��ʱ��־���ͣ���ͨ��־=0��api�ӿ���־=1��sql��־=2
            if (dr.Table.Columns.Contains("logtype"))
            {
                o.logtype = dr["logtype"].ToByte();
            }
            //��־��ʶ,sql������Ϊsql��ϣ,api������Ϊurl
            if (dr.Table.Columns.Contains("logtag"))
            {
                o.logtag = dr["logtag"].Toint();
            }
            //��ǰurl
            if (dr.Table.Columns.Contains("url"))
            {
                o.url = dr["url"].Tostring();
            }
            //��ǰ��Ϣ
            if (dr.Table.Columns.Contains("msg"))
            {
                o.msg = dr["msg"].Tostring();
            }
            //��Դip(����ִ��ip)
            if (dr.Table.Columns.Contains("fromip"))
            {
                o.fromip = dr["fromip"].Tostring();
            }
            //sqlip��ַ
            if (dr.Table.Columns.Contains("sqlip"))
            {
                o.sqlip = dr["sqlip"].Tostring();
            }
            //������¼�����Ϣ
            if (dr.Table.Columns.Contains("remark"))
            {
                o.remark = dr["remark"].Tostring();
            }
            return o;
        }
    }
}