using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess;
using System.Data;
using System.Data.Common;

namespace TaskManager.Utils.Db
{
    class DbConnOracle : DbConn
    {
        private OracleConnection privateconn;
        private OracleTransaction ts = null;

        public DbConnOracle()
        {
            privateconn = new OracleConnection();
            _conn = privateconn;
            _dbtype = DbType.ORACLE;
        }

        public override void BeginTransaction()
        {
            ts = privateconn.BeginTransaction();
        }

        public override void Commit()
        {
            try
            {
                ts.Commit();
            }
            finally
            {
                ts = null;
            }
        }

        public override void Rollback()
        {
            try
            {
                ts.Rollback();
            }
            finally
            {
                ts = null;
            }
        }

        public override DbTransaction GetTransaction()
        {
            return ts;
        }

        /// <summary>参数类型转化</summary>
        /// <param name="Par"></param>
        /// <returns></returns>
        private OracleParameter ParameterTransform(ProcedureParameter ProcedurePar)
        {
            string pName = ':' + ProcedurePar.Name.TrimStart('@').TrimStart(':');
            /*车毅修改 支持无类型参数*/
            if (ProcedurePar.ParType == ProcParType.Default)
            {
                if (ProcedurePar.Value != null && ProcedurePar.Value.GetType() == typeof(Boolean))
                {
                    return new OracleParameter(pName, Convert.ToBoolean(ProcedurePar.Value) == true ? 1 : 0);
                }
                return new OracleParameter(pName, ProcedurePar.Value == null ? DBNull.Value : ProcedurePar.Value);
            }
            //for (int i = 0; i < ProcedurePar.Count; i++)
            //{
            OracleParameter p = new OracleParameter();
            p.ParameterName = pName;
            switch (ProcedurePar.ParType)
            {
                case ProcParType.Int16:
                    p.OracleDbType = OracleDbType.Int16;
                    break;
                case ProcParType.Int32:
                    p.OracleDbType = OracleDbType.Int32;
                    break;
                case ProcParType.Int64:
                    p.OracleDbType = OracleDbType.Int64;
                    break;
                case ProcParType.Single:
                    p.OracleDbType = OracleDbType.Single;
                    break;
                case ProcParType.Double:
                    p.OracleDbType = OracleDbType.Double;
                    break;
                case ProcParType.Decimal:
                    p.OracleDbType = OracleDbType.Decimal;
                    break;
                case ProcParType.Char:
                    p.OracleDbType = OracleDbType.Char;
                    break;
                case ProcParType.VarChar:
                    p.OracleDbType = OracleDbType.Varchar2;
                    break;
                case ProcParType.NVarchar:
                    p.OracleDbType = OracleDbType.NVarchar2;
                    break;
                case ProcParType.Image:
                    p.OracleDbType = OracleDbType.Blob;
                    break;
                case ProcParType.DateTime:
                    p.OracleDbType = OracleDbType.Date;
                    break;
                default:
                    throw new Exception("未知类型ProcParType：" + ProcedurePar.ParType.ToString());
            }
            p.Size = ProcedurePar.Size;
            p.Direction = ProcedurePar.Direction;
            switch (ProcedurePar.Direction)
            {
                case ParameterDirection.Input:
                case ParameterDirection.InputOutput:
                    p.Value = ProcedurePar.Value;
                    if (ProcedurePar.ParType == ProcParType.DateTime)
                    {
                        //var s = Convert.ToString(p.Value);
                        //if(!string.IsNullOrEmpty(s))
                        //{
                        //    if (!s.ToLower().StartsWith("to_date"))
                        //    {
                        //        p.Value = "to_date('" + ProcedurePar.Value + "','yyyy-mm-dd hh24:mi:ss')";
                        //    }
                        //}
                    }
                    break;
            }
            return p;
        }

        public override int ExecuteSql(string Sql, CommandType CmdType, List<ProcedureParameter> ProcedurePar)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = privateconn;
            cmd.Transaction = ts;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CmdType;
            cmd.CommandText = Sql;
            cmd.BindByName = true;
            if (ProcedurePar != null && ProcedurePar.Count > 0)
            {
                for (int i = 0; i < ProcedurePar.Count; i++)
                {
                    OracleParameter p = ParameterTransform(ProcedurePar[i]);
                    cmd.Parameters.Add(p);
                }
            }
            return cmd.ExecuteNonQuery();
        }

        public override object ExecuteScalar(string Sql, CommandType CmdType, List<ProcedureParameter> ProcedurePar)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = privateconn;
            cmd.Transaction = ts;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CmdType;
            cmd.CommandText = Sql;
            cmd.BindByName = true;
            if (ProcedurePar != null && ProcedurePar.Count > 0)
            {
                for (int i = 0; i < ProcedurePar.Count; i++)
                {
                    OracleParameter p = ParameterTransform(ProcedurePar[i]);
                    cmd.Parameters.Add(p);
                }
            }
            return cmd.ExecuteScalar();
        }

        //public override int ExecuteSql(string Sql, List<ImageParameter> ImagePar)
        //{
        //    OracleCommand cmd = new OracleCommand();
        //    cmd.Connection = privateconn;
        //    cmd.Transaction = ts;
        //    cmd.CommandTimeout = 0;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = Sql;
        //    for (int i = 0; i < ImagePar.Count; i++)
        //    {
        //        OracleParameter p = new OracleParameter(ImagePar[i].Name, OracleType.Blob, ImagePar[i].Value.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, ImagePar[i].Value);
        //        cmd.Parameters.Add(p);
        //    }
        //    return cmd.ExecuteNonQuery();
        //}

        public override int ExecuteProcedure(string ProcedureName, List<ProcedureParameter> ProcedurePar)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = privateconn;
            cmd.Transaction = ts;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = ProcedureName;
            cmd.BindByName = true;
            if (ProcedurePar != null && ProcedurePar.Count > 0)
            {
                for (int i = 0; i < ProcedurePar.Count; i++)
                {
                    OracleParameter p = ParameterTransform(ProcedurePar[i]);
                    cmd.Parameters.Add(p);
                }
            }
            int result = cmd.ExecuteNonQuery();
            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                switch (cmd.Parameters[i].Direction)
                {
                    case ParameterDirection.InputOutput:
                    case ParameterDirection.Output:
                    case ParameterDirection.ReturnValue:
                        ProcedurePar[i] = new ProcedureParameter(ProcedurePar[i].Name, ProcedurePar[i].ParType, ProcedurePar[i].Size, ProcedurePar[i].Direction, cmd.Parameters[i].Value);
                        break;
                }
            }
            return result;
        }

        public override DbDataReader SqlToDbReader(string Sql, List<ProcedureParameter> ProcedurePar)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = privateconn;
            cmd.Transaction = ts;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = Sql;
            cmd.BindByName = true;
            if (ProcedurePar != null && ProcedurePar.Count > 0)
            {
                for (int i = 0; i < ProcedurePar.Count; i++)
                {
                    OracleParameter p = ParameterTransform(ProcedurePar[i]);
                    cmd.Parameters.Add(p);
                }
            }
            return cmd.ExecuteReader();
        }

        public override int GetIdentity()
        {
            DataSet ds = new DataSet();
            SqlToDataSet(ds, "select sequence.currval from dual;", null);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return LibConvert.ObjToInt(ds.Tables[0].Rows[0][0]);
            }
        }

        public override bool FieldIsExist(string aTableName, string aFieldName)
        {
            string TempSql = "select top 0 * from " + aTableName;
            DataSet ds = new DataSet();
            SqlToDataSet(ds, TempSql, null);
            if (ds.Tables[0].Columns.IndexOf(aFieldName) < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override bool FieldIsExist(string aDbName, string aTableName, string aFieldName)
        {
            return FieldIsExist(aTableName, aFieldName);
        }




        public override DateTime GetServerDate()
        {
            DateTime Result = new DateTime(0);
            OracleCommand cmdOracle = new OracleCommand("Select sysdate as aDate from DUAL", privateconn);
            cmdOracle.Transaction = ts;
            OracleDataReader DrOracle = cmdOracle.ExecuteReader();
            if (DrOracle.Read())
                Result = DrOracle.GetDateTime(0);
            DrOracle.Close();
            return Result;
        }



        public override void SqlToDataSet(DataSet ds, string Sql, List<ProcedureParameter> ProcedurePar)
        {
            OracleDataAdapter da = new OracleDataAdapter(Sql, privateconn);
            da.SelectCommand.CommandTimeout = 0;
            da.SelectCommand.Transaction = ts;
            da.SelectCommand.BindByName = true;
            if (ProcedurePar != null && ProcedurePar.Count > 0)
            {
                for (int i = 0; i < ProcedurePar.Count; i++)
                {
                    OracleParameter p = ParameterTransform(ProcedurePar[i]);
                    da.SelectCommand.Parameters.Add(p);
                }
            }
            da.Fill(ds);
        }

        public override void SqlToDataSet(DataSet ds, string Sql, List<ProcedureParameter> ProcedurePar, string TableName)
        {
            OracleDataAdapter da = new OracleDataAdapter(Sql, privateconn);
            da.SelectCommand.CommandTimeout = 0;
            da.SelectCommand.Transaction = ts;
            da.SelectCommand.BindByName = true;
            if (ProcedurePar != null && ProcedurePar.Count > 0)
            {
                for (int i = 0; i < ProcedurePar.Count; i++)
                {
                    OracleParameter p = ParameterTransform(ProcedurePar[i]);
                    da.SelectCommand.Parameters.Add(p);
                }
            }
            da.Fill(ds, TableName);
        }

        public override bool TableIsExist(string aTableName)
        {
            DataSet Ds = new DataSet();
            List<ProcedureParameter> Par = new List<ProcedureParameter>()
            {
                new ProcedureParameter(){ Name="tablename", ParType= ProcParType.VarChar, Value=aTableName.ToUpper()}
            };
            SqlToDataSet(Ds, "select table_name from user_tables where table_name=:tablename", Par);
            if (Ds.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public override bool TableIsExist(string aDbName, string aTableName)
        {
            return TableIsExist(aTableName);
        }



        public override string GetMcsToSql(string AMultiChoiceStr, string aField, int aStyle, ref List<Db.ProcedureParameter> aPar)
        {
            if (aPar == null) aPar = new List<ProcedureParameter>();
            int j = 0, jyi = 0;
            List<string> SqlSz = new List<string>();
            string TmpStr = "";
            int tmpBzwz = 0;
            string sRet = "";
            for (int i = 1; i < LibString.GetLength(AMultiChoiceStr); i++)
            {
                if (LibString.RightStr(LibString.LeftStr(AMultiChoiceStr, i), 1) == ",")
                {
                    SqlSz.Add(LibString.RightStr(LibString.LeftStr(AMultiChoiceStr, i - 1), i - jyi));
                    j = j + 1;
                    jyi = i + 1;
                }
            }
            if (jyi - 1 != LibString.GetLength(AMultiChoiceStr))
            {
                SqlSz.Add(LibString.RightStr(LibString.LeftStr(AMultiChoiceStr, LibString.GetLength(AMultiChoiceStr)), LibString.GetLength(AMultiChoiceStr) - jyi + 1));
            }
            if (AMultiChoiceStr == "")
            {
                sRet = "(1=1)";
            }
            else
            {
                for (int i = 0; i < SqlSz.Count; i++)
                {
                    tmpBzwz = ((string)SqlSz[i]).IndexOf("|");
                    if (tmpBzwz == -1)
                    {
                        if (aStyle == 0)
                            TmpStr = aField + " Like '" + SqlSz[i] + "'";
                        else if (aStyle == 1)
                            TmpStr = aField + " Like '%" + SqlSz[i] + "'";
                        else if (aStyle == 2)
                            TmpStr = aField + " Like '" + SqlSz[i] + "%'";
                        else if (aStyle == 3)
                            TmpStr = aField + " Like '%" + SqlSz[i] + "%'";
                    }
                    else
                    {
                        TmpStr = "(substr(" + aField + ",1," + LibString.GetLength(LibString.LeftStr(SqlSz[i], tmpBzwz)).ToString() + ")>='" +
                                 LibString.LeftStr(SqlSz[i], tmpBzwz) + "' and substr(" + aField + ",1," +
                                 LibString.GetLength(LibString.RightStr(LibString.LeftStr(SqlSz[i], LibString.GetLength(SqlSz[i])), LibString.GetLength(SqlSz[i]) - tmpBzwz - 1)).ToString() + ")<='" +
                                 LibString.RightStr(LibString.LeftStr(SqlSz[i], LibString.GetLength(SqlSz[i])), LibString.GetLength(SqlSz[i]) - tmpBzwz - 1) + "')";
                    }
                    if (sRet == "")
                        sRet = TmpStr;
                    else
                        sRet = sRet + " or " + TmpStr;
                }
            }
            return "(" + sRet + ")";
        }
    }
}
