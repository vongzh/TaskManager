using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager.Utils.Tool
{
    /// <summary>
    /// 24进制乱序
    /// </summary>
    public class Convert24Provider
    {
        //10进制数所对应的24进制数
        char[] rule = new char[] {
                                     'w','z','n','p','c','e','h','b',
                                      'l','x','m','y','q','r','k','s','t','u','v',
                                       'a','f','g','j','d',
                                      };
        /// <summary>
        /// 数字转24进制
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string To(long num)
        {
            if (num < 0)
            {
                return "";
            }


            //保存除24后的余数
            List<long> list = new List<long>();
            while (num >= rule.Length)
            {
                long a = num % rule.Length;
                num /= rule.Length;
                list.Add(a);
            }
            list.Add(num);

            StringBuilder sb = new StringBuilder();
            //结果要从后往前排
            for (int i = list.Count - 1; i >= 0; i--)
            {
                sb.Append(rule[list[i]]);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 24进制转数字
        /// </summary>
        /// <param name="convert"></param>
        /// <returns></returns>
        public long From(string convert)
        {
            if (string.IsNullOrEmpty(convert))
                return -1;
            //var cs = convert.ToCharArray();

            Dictionary<char, int> dics = new Dictionary<char, int>();
            int j = 0;
            foreach (var o in rule)
            {
                dics.Add(o, j);
                j++;
            }

            int length = convert.Length;
            long result = 0;
            for (int i = 0; i < length; i++)
            {
                long val = (long)Math.Pow(rule.Length, (length - i - 1));
                char c = convert[i];
                long tmp = dics[c];
                result += tmp * val;
            }
            return result;
        }


    }
}
