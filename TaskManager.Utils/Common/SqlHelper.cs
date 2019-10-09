using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;

namespace TaskManager.Utils.Common
{
    /// <summary>
    /// sql帮助类
    /// 用于sql的一些处理方式的封装 车毅
    /// 举例：sql安全检查，危险字符过滤，命令拼接，统一分页（多表，单表），where条件拼接
    /// sql耗时检测等
    /// </summary> 
    public class SqlHelper
    {
        public enum SqlDbType
        {
            /// <summary>
            /// Oracle数据库 0
            /// </summary>
            [System.ComponentModel.Description("Oracle数据库")]
            Oracle = 0,
            /// <summary>
            /// SQLServer数据库 1
            /// </summary>
            [System.ComponentModel.Description("SQLServer数据库")]
            SQLServer = 1,
        }
        /// <summary>
        /// ,分隔的in 
        /// 举例 name in ('a','b','c'); 
        /// </summary>
        /// <param name="splits"></param>
        /// <returns></returns>
        public static string CmdIn(List<Db.ProcedureParameter> par, string splits, SqlDbType sqlType = SqlDbType.SQLServer)
        {
            string[] ss = splits.Trim(',').Split(',');
            string r = "";
            int index = 0;
            if (sqlType == SqlDbType.Oracle)
            {
                foreach (var s in ss)
                {
                    string param = string.Format(":inparam{0}", index);
                    r += param + ",";
                    par.Add(new Db.ProcedureParameter(param, s));
                    index++;
                }
            }
            else
            {
                foreach (var s in ss)
                {
                    string param = string.Format("@inparam{0}", index);
                    r += param + ",";
                    par.Add(new Db.ProcedureParameter(param, s));
                    index++;
                }
            }
            return r.Trim(',');
        }

        /// <summary>
        /// ,分隔的in 
        /// 举例 name in ('a','b','c'); 
        /// </summary>
        /// <param name="splits"></param>
        /// <returns></returns>
        public static string CmdIn<T>(List<Db.ProcedureParameter> par, List<T> splits, SqlDbType sqlType = SqlDbType.SQLServer)
        {

            string r = "";
            int index = 0;
            if (sqlType == SqlDbType.Oracle)
            {
                foreach (var s in splits)
                {
                    string param = string.Format(":inparam{0}", index);
                    r += param + ",";
                    par.Add(new Db.ProcedureParameter(param, s));
                    index++;
                }
            }
            else
            {
                foreach (var s in splits)
                {
                    string param = string.Format("@inparam{0}", index);
                    r += param + ",";
                    par.Add(new Db.ProcedureParameter(param, s));
                    index++;
                }
            }
            return r.Trim(',');
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="par"></param>
        /// <param name="splits"></param>
        /// <returns></returns>
        public static string CmdInFromList<T>(List<Db.ProcedureParameter> par, List<T> splits, string inparam, SqlDbType sqlType = SqlDbType.SQLServer)
        {
            int index = 0;
            StringBuilder sb = new StringBuilder();
            if (sqlType == SqlDbType.Oracle)
            {
                foreach (var s in splits)
                {
                    string param = string.Format(":{0}{1}", inparam, index);
                    sb.AppendFormat(param + ",");
                    par.Add(new Db.ProcedureParameter(param, s));
                    index++;
                }
            }
            else
            {
                foreach (var s in splits)
                {
                    string param = string.Format("@{0}{1}", inparam, index);
                    sb.AppendFormat(param + ",");
                    par.Add(new Db.ProcedureParameter(param, s));
                    index++;
                }
            }
            return sb.ToString().Trim(',');
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="par"></param>
        /// <param name="splits"></param>
        /// <returns></returns>
        public static string CmdInFromListForSimplePar<T>(SimpleProcedureParameter ps, List<T> splits, string inparam, SqlDbType sqlType = SqlDbType.SQLServer)
        {
            int index = 0;
            StringBuilder sb = new StringBuilder();
            if (sqlType == SqlDbType.Oracle)
            {
                foreach (var s in splits)
                {
                    string param = string.Format(":{0}{1}", inparam, index);
                    sb.AppendFormat(param + ",");
                    ps.Add(param, s);
                    index++;
                }
            }
            else
            {
                foreach (var s in splits)
                {
                    string param = string.Format("@{0}{1}", inparam, index);
                    sb.AppendFormat(param + ",");
                    ps.Add(param, s);
                    index++;
                }
            }
            return sb.ToString().Trim(',');
        }

        public static void SetPagingForPar(int pageindex, int pagesize, List<ProcedureParameter> par, SqlDbType sqlType = SqlDbType.SQLServer)
        {
            int startIndex = (pageindex - 1) * pagesize + 1;
            int endIndex = pagesize * pageindex;
            if (sqlType == SqlDbType.Oracle)
            {
                par.Add(new ProcedureParameter(":startIndex", startIndex));
                par.Add(new ProcedureParameter(":endIndex", endIndex));
            }
            else
            {
                par.Add(new ProcedureParameter("@startIndex", startIndex));
                par.Add(new ProcedureParameter("@endIndex", endIndex));
            }
        }

        public static void SetPagingForSimplePar(int pageindex, int pagesize, SimpleProcedureParameter ps, SqlDbType sqlType = SqlDbType.SQLServer)
        {
            int startIndex = (pageindex - 1) * pagesize + 1;
            int endIndex = pagesize * pageindex;
            if (sqlType == SqlDbType.Oracle)
            {
                ps.Add(":startIndex", startIndex);
                ps.Add(":endIndex", endIndex);
            }
            else
            {
                ps.Add("@startIndex", startIndex);
                ps.Add("@endIndex", endIndex);
            }
        }

        #region 分页
        /// <summary>
        /// 分页1 wsd_page_1： 根据唯一字段唯一值按大小排序，如ID
        /// 备注：数据库需要安装Sys_Page1存储过程
        /// 采用min,max算法分页,性能最优,可能有部分数据（相同值的排序字段处于分页时）丢失，有适用范围，详情百度
        /// http://www.jiaonan.tv/html/blog/1/26473.htm
        /// </summary>
        /// <param name="tb">表名</param>
        /// <param name="collist">要查询出的字段列表,*表示全部字段</param>
        /// <param name="condition">查询条件 ,不带where</param>
        /// <param name="col">排序列 例：ID</param>
        /// <param name="coltype">列的类型,0-数字类型,1-字符类型</param>
        /// <param name="orderby">--排序,FALSE-顺序,TRUE-倒序</param>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="page">当前页</param>
        /// <param name="records">总记录数：为0则计算总记录数</param>
        /// <returns>分页记录</returns>
        public DataSet Pager1(DbConn conn, string tb, string collist, string condition, string col, int coltype, bool orderby, int pagesize, int page, ref int records)
        {
            DataSet Datalist = new DataSet();
            //SqlParameter[] parms;

            //parms = new SqlParameter[]
            //{ 
            //    new SqlParameter("@tb",SqlDbType.VarChar,200),
            //    new SqlParameter("@collist",SqlDbType.VarChar,800),
            //    new SqlParameter("@condition",SqlDbType.VarChar,800),
            //    new SqlParameter("@col",SqlDbType.VarChar,50),
            //    new SqlParameter("@coltype",SqlDbType.SmallInt,2),
            //    new SqlParameter("@orderby",SqlDbType.Bit,1),
            //    new SqlParameter("@pagesize",SqlDbType.Int,4),
            //    new SqlParameter("@page",SqlDbType.Int,4),
            //    new SqlParameter("@records",SqlDbType.Int,4)
            //};
            //parms[0].Value = tb;
            //parms[1].Value = collist;
            //parms[2].Value = condition;
            //parms[3].Value = col;
            //parms[4].Value = coltype;
            //parms[5].Value = orderby;
            //parms[6].Value = pagesize;
            //parms[7].Value = page;
            //parms[8].Value = records;
            //parms[8].Direction = ParameterDirection.InputOutput;
            var Par = new List<Db.ProcedureParameter>();
            Par.Add(new ProcedureParameter("@tb", ProcParType.VarChar, 200, tb));
            Par.Add(new ProcedureParameter("@collist", ProcParType.VarChar, 800, collist));
            Par.Add(new ProcedureParameter("@condition", ProcParType.VarChar, 800, condition));
            Par.Add(new ProcedureParameter("@col", ProcParType.VarChar, 50, col));
            Par.Add(new ProcedureParameter("@coltype", ProcParType.Int16, 2, coltype));
            Par.Add(new ProcedureParameter("@orderby", ProcParType.Int16, 2, orderby));
            Par.Add(new ProcedureParameter("@pagesize", ProcParType.Int32, 4, pagesize));
            Par.Add(new ProcedureParameter("@page", ProcParType.Int32, 4, page));
            Par.Add(new ProcedureParameter("@records", ProcParType.Int32, 4, ParameterDirection.ReturnValue, records));
            //Datalist =  db DBUtility.SqlHelper.ExecuteDataset(sqlConnectionString, CommandType.StoredProcedure, "Sys_Page1", parms);
            conn.SqlToDataSet(Datalist, "Sys_Page1", Par);
            records = Convert.ToInt32(Par[8].Value.ToString());
            return Datalist;

        }




        /// <summary>
        ///  分页2 wsd_page_2：单表任意排序
        ///  备注：数据库需要安装Sys_Page2存储过程
        ///  采用not in的算法方式分页，性能不是最佳 http://www.jiaonan.tv/html/blog/1/26473.htm
        /// </summary>
        /// <param name="tb">表名  例: news</param>
        /// <param name="collist">要查询出的字段列表,*表示全部字段</param>
        /// <param name="where">查询条件，不带where 例：classid = 2</param>
        /// <param name="orderby">排序条件 例：order by tuijian desc,id desc</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="page">当前页码</param>
        /// <param name="records">总记录数：为0则重新计算</param>
        /// <returns>分页记录</returns>
        public DataSet Pager2(DbConn conn, string tb, string collist, string where, string orderby, int pagesize, int page, ref int records)
        {
            DataSet Datalist = new DataSet();
            //SqlParameter[] parms;
            //parms = new SqlParameter[]
            //{ 
            //    new SqlParameter("@tb",SqlDbType.VarChar,500),
            //    new SqlParameter("@collist",SqlDbType.VarChar,800),
            //    new SqlParameter("@where",SqlDbType.VarChar,800),
            //    new SqlParameter("@orderby",SqlDbType.VarChar,800),
            //    new SqlParameter("@pagesize",SqlDbType.Int,4),
            //    new SqlParameter("@page",SqlDbType.Int,4),
            //    new SqlParameter("@records",SqlDbType.Int,4)
            //};
            //parms[0].Value = tb;
            //parms[1].Value = collist;
            //parms[2].Value = where;
            //parms[3].Value = orderby;
            //parms[4].Value = pagesize;
            //parms[5].Value = page;
            //parms[6].Value = records;
            //parms[6].Direction = ParameterDirection.InputOutput;
            var Par = new List<Db.ProcedureParameter>();
            Par.Add(new ProcedureParameter("@tb", ProcParType.VarChar, 500, tb));
            Par.Add(new ProcedureParameter("@collist", ProcParType.VarChar, 800, collist));
            Par.Add(new ProcedureParameter("@where", ProcParType.VarChar, 800, where));
            Par.Add(new ProcedureParameter("@orderby", ProcParType.VarChar, 800, orderby));
            Par.Add(new ProcedureParameter("@pagesize", ProcParType.Int32, 4, pagesize));
            Par.Add(new ProcedureParameter("@page", ProcParType.Int32, 4, page));
            Par.Add(new ProcedureParameter("@records", ProcParType.Int32, 4, ParameterDirection.ReturnValue, records));
            conn.SqlToDataSet(Datalist, "Sys_Page2", Par);
            records = Convert.ToInt32(Par[6].Value.ToString());
            //Datalist = DBUtility.SqlHelper.ExecuteDataset(sqlConnectionString, CommandType.StoredProcedure, "Sys_Page2", parms);
            //records = Convert.ToInt32(parms[6].Value.ToString());
            return Datalist;

        }




        /// <summary>
        /// 分页3： 单表/多表通用分页存储过程 wsd_page_3
        /// 备注：数据库需要安装Sys_Page3存储过程
        /// 好像是颠倒算法，效率还行。http://www.jiaonan.tv/html/blog/1/26473.htm
        /// </summary>
        /// <param name="tb">表名 例： table1 inner join table2 on table1.xx=table2.xx </param>
        /// <param name="collist">需要获取字段 例: tabl1.xx,table2.*,注意，需要把排序列都选上</param>
        /// <param name="where">条件,不带where</param>
        /// <param name="orderby">最内层orderby(需要带上表前缀，注意asc 必须写上) 例: order by table1.xxx desc,table2.ad asc "</param>
        /// <param name="orderbyo">最外城orderby xxx.desc,ad asc</param>        
        /// <param name="pagesize">每页条数</param>
        /// <param name="page">页数</param>
        /// <param name="records">记录条数</param>
        /// <returns></returns>

        public DataSet Pager3(DbConn conn, string tb, string collist, string where, string orderby, string orderbyo, int pagesize, int page, ref int records)
        {
            DataSet Datalist = new DataSet();
            //SqlParameter[] parms;
            //parms = new SqlParameter[]
            //{ 
            //    new SqlParameter("@tb",SqlDbType.VarChar,800),
            //    new SqlParameter("@collist",SqlDbType.VarChar,800),
            //    new SqlParameter("@where",SqlDbType.VarChar,800),
            //    new SqlParameter("@orderby",SqlDbType.VarChar,800),
            //    new SqlParameter("@orderbyo",SqlDbType.VarChar,800),
            //    new SqlParameter("@pagesize",SqlDbType.Int,4),
            //    new SqlParameter("@page",SqlDbType.Int,4),
            //    new SqlParameter("@records",SqlDbType.Int,4)
            //};
            //parms[0].Value = tb;
            //parms[1].Value = collist;
            //parms[2].Value = where;
            //parms[3].Value = orderby;
            //parms[4].Value = orderbyo;
            //parms[5].Value = pagesize;
            //parms[6].Value = page;
            //parms[7].Value = records;
            //parms[7].Direction = ParameterDirection.InputOutput;
            var Par = new List<Db.ProcedureParameter>();
            Par.Add(new ProcedureParameter("@tb", ProcParType.VarChar, 500, tb));
            Par.Add(new ProcedureParameter("@collist", ProcParType.VarChar, 800, collist));
            Par.Add(new ProcedureParameter("@where", ProcParType.VarChar, 800, where));
            Par.Add(new ProcedureParameter("@orderby", ProcParType.VarChar, 800, orderby));
            Par.Add(new ProcedureParameter("@orderbyo", ProcParType.VarChar, 800, orderbyo));
            Par.Add(new ProcedureParameter("@pagesize", ProcParType.Int32, 4, pagesize));
            Par.Add(new ProcedureParameter("@page", ProcParType.Int32, 4, page));
            Par.Add(new ProcedureParameter("@records", ProcParType.Int32, 4, ParameterDirection.ReturnValue, records));
            //Datalist = DBUtility.SqlHelper.ExecuteDataset(sqlConnectionString, CommandType.StoredProcedure, "Sys_Page3", parms);
            //records = Convert.ToInt32(parms[7].Value.ToString());
            conn.SqlToDataSet(Datalist, "Sys_Page3", Par);
            records = Convert.ToInt32(Par[7].Value.ToString());
            return Datalist;

        }
        #endregion

        /// <summary>
        /// sql访问拦截器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Visit<T>(Func<SimpleProcedureParameter, T> action)
        {
            try
            {
                var dicparams = new SimpleProcedureParameter();
                var r = action.Invoke(dicparams);
                return r;
            }
            catch (Exception exp)
            {
                //ErrorLog.Write("dal层sql调用出错:", exp);
                throw exp;

            }

        }

        /// <summary>
        /// sql访问拦截器 非泛型版本
        /// 默认DataTable 返回 List{DictionaryResult} Model对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public static List<DictionaryResult> Visit(Func<SimpleProcedureParameter, DataTable> action)
        {
            var r = SqlHelper.Visit<DataTable>(action);

            return DataTableHelper.ToDictionaryList(r as DataTable);
        }

        public static void ExcuteSql(string connectstring, Action<DbConn> action)
        {
            using (DbConn conn = DbConfig.CreateConn(connectstring))
            {
                conn.Open();
                action(conn);
            }
        }

        public static void ExcuteSql(string connectstring, bool iswatchtimelog, Action<DbConn> action)
        {
            using (DbConn conn = DbConfig.CreateConn(connectstring))
            {
                conn.Open();
                conn.IsWatchTime = iswatchtimelog;
                action(conn);
            }
        }
    }
}
