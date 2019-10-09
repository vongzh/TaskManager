using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Utils.Common
{
    /// <summary>
    /// 默认约定的实体转字典存储结果 车毅
    /// </summary>
    public class DictionaryResult : Dictionary<string, Object>
    {

    }

    /// <summary>
    /// dataTable 转 json输出字典
    /// 用于简化api结果输出，使之不用通过自定义model的方式输出 车毅
    /// </summary>
    public static class DataTableHelper
    {
        /// <summary>
        /// 字典列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<DictionaryResult> ToDictionaryList(DataTable dt)
        {
            List<DictionaryResult> rs = new List<DictionaryResult>();
            foreach (DataRow dr in dt.Rows)
            {
                DictionaryResult r = new DictionaryResult();
                foreach (DataColumn c in dt.Columns)
                {
                    //KeyValuePair<string, Object> k = new KeyValuePair<string, object>();
                    r.Add(c.ColumnName, dr[c]);//r.Add(c.ColumnName.ToLower(), dr[c]);
                }
                rs.Add(r);
            }
            return rs;
        }

        /// <summary>
        /// 单条字典结果
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DictionaryResult ToDictionary(DataTable dt)
        {
            if (dt.Rows.Count == 1)
                return ToDictionaryList(dt)[0];
            else if (dt.Rows.Count > 1)
                throw new Exception("多于一条结果");
            else
                return null;
        }
        /// <summary>
        /// List转datatable (未测)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertToModel<T>(this DataTable dt) where T : class, new()
        {
            // 定义集合 
            List<T> ts = new List<T>();
            // 获得此模型的类型 
            Type type = typeof(T);
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性 
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    // 检查DataTable是否包含此列 
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter 
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }

        /// <summary>
        /// List转datatable (未测)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            return tb;
        }

        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
    }
}