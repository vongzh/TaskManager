using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TaskManager.Utils.Extensions
{
    /// <summary>
    /// 字符串处理方法
    /// </summary>
    public static class StringMehtodHelper
    {
        /// <summary>
        /// 部分字符串获取
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxlen"></param>
        /// <returns></returns>
        public static string SubString3(this string str, int maxlen)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            if (str.Length <= maxlen)
                return str;
            return str.Substring(0, maxlen)+"...";
        }
        /// <summary>
        /// 部分字符串获取
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxlen"></param>
        /// <returns></returns>
        public static string SubString2(this string str,int maxlen)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            if (str.Length <= maxlen)
                return str;
            return str.Substring(0, maxlen);
        }
        /// <summary>
        /// 如果string空引用转空内容
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NullToEmpty(this string str)
        {
            if (str == null)
                return "";
            return str;
        }

        /// <summary>
        /// 去除html标签
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveHtml(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string text1 = "<.*?>";
                Regex regex1 = new Regex(text1);
                str = regex1.Replace(str, "");
                str = str.Replace("&nbsp;", " ");
            }
            return str;
        }

        /// <summary>
        /// 去除2个以上的空格
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveMoreSpace(this string content)
        {
            if (string.IsNullOrEmpty(content)) return "";
            Regex r = new Regex(@"\s{2,}", RegexOptions.Multiline);
            return r.Replace(content, " ");
        }

        /// <summary>
        /// 剪切字符串
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="intLen"></param>
        /// <returns></returns>
        public static string CutString(this string strInput, int intLen)
        {
            if (String.IsNullOrEmpty(strInput))
                return strInput;
            strInput = strInput.Trim();
            byte[] buffer1 = Encoding.Default.GetBytes(strInput);
            if (buffer1.Length > intLen)
            {
                string text1 = "";
                for (int num1 = 0; num1 < strInput.Length; num1++)
                {
                    byte[] buffer2 = Encoding.Default.GetBytes(text1 + strInput.Substring(num1, 1));
                    if (buffer2.Length > intLen)
                    {
                        break;
                    }
                    text1 = text1 + strInput.Substring(num1, 1);
                }
                return (text1 + "...");
            }
            return strInput;
        }

        /// <summary>
        /// json特殊字符处理
        /// </summary>
        public static string EscapeJson(this string s)
        {
            if (s == null || s.Length == 0)
                return s;

            s = s.Replace("\\", "\\\\");
            s = s.Replace("\"", "\\\"");
            s = s.Replace("\r", "\\r");
            s = s.Replace("\n", "\\n");

            return s;
        }

        /// <summary>
        /// StartWith忽略大小写
        /// </summary>
        public static bool StartWithIgnoreCase(this string s, string begin)
        {
            if (s == null || s.Length == 0)
                return false;

            return s.StartsWith(begin, true, System.Globalization.CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 移除字符串
        /// </summary>
        public static string RemoveStart(this string s, int len)
        {
            if (s == null || s.Length == 0)
                return s;
            return s.Remove(0, len); ;
        }
    }
}
