using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TaskManager.Utils.Extensions
{
    /// <summary>
    /// DB object对象的扩展方法
    /// </summary>
    public static class DBObjectMethodHelper
    { 
        public static bool isint(this string o)
        {
            int count;
            if (int.TryParse(o, out count))
            {
                return true;
            }
            return false;
        }

        public static bool Tobool(this object o)
        {
            if (o == null || Convert.IsDBNull(o))
                return false;
            return Convert.ToBoolean(o);
        }
        public static short Toshort(this object o)
        {
            if (o == null || Convert.IsDBNull(o))
                return 0;
            return Convert.ToInt16(o);
        }
        public static int Toint(this object o)
        {
            if (o == null || Convert.IsDBNull(o))
                return 0;
            return Convert.ToInt32(o);
        }
        public static double Todouble(this object o)
        {
            if (o == null || Convert.IsDBNull(o))
                return 0;
            return Convert.ToDouble(o);
        }
        public static decimal Todecimal(this object o)
        {
            if (o == null || Convert.IsDBNull(o))
                return 0;
            return Convert.ToDecimal(o);
        }
        public static long Tolong(this object o)
        {
            if (o == null || Convert.IsDBNull(o))
                return 0;
            return Convert.ToInt64(o);
        }
        public static string Tostring(this object o)
        {
            if (o == null || Convert.IsDBNull(o))
                return null;
            return Convert.ToString(o);
        }
        public static DateTime ToDateTime(this object o)
        {
            if (o == null || Convert.IsDBNull(o))
                return new DateTime();
            return Convert.ToDateTime(o);
        }
        public static Byte ToByte(this object o)
        {
            if (o == null || Convert.IsDBNull(o))
                return 0;
            return Convert.ToByte(o);
        }
        public static byte[] ToBytes(this object o)
        {
            if (o == null || Convert.IsDBNull(o))
                return null;
            return (byte[])(o);
        }
    }
}
