using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Utils.Tool
{  
    /// <summary>
    /// 34位编码提供类
    /// </summary>
    public class Convert34Provider
    {
        public string[] _34Data = {"0","1","2","3","4","5","6","7","8","9","a","b",
                                      "c","d","e","f","g","h","j","k","l","m","n",
                                      "p","q","r","s","t","u","v","w","x","y","z" };
        public string result = "";
        /// <summary>
        /// 调用则执行转34位编码
        /// </summary>
        /// <param name="i"></param>
        public void ConvertDiaTo34(long i)
        {
            long n = i / 34;
            long m = i % 34;
            result = _34Data[m] + result;
            if (n > 0)
            {
                ConvertDiaTo34(n);
            }
        }
        /// <summary>
        /// 获取34位编码字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (result.Length < 4)
            {
                return result.PadLeft(4, '0');
            }
            else
            {
                int n = result.Length % 4;
                int m = result.Length / 4;
                string s = result.Substring(0, n).PadLeft(4, '0');
                for (int i = 0; i < m; i++)
                {
                    s = s + " " + result.Substring(n + i * 4, 4);
                }
                return s;
            }
        }
    }
    //public class Convert34Provider
    //{
    //    public string To(int num)
    //    {
    //        if (num < 0)
    //        {
    //            return "";
    //        }
    //        //10进制数所对应的34进制数
    //        char[] rule = new char[] {'0','1','2','3','4','5','6','7','8','9',
    //                                  'a','b','c','d','e','f','g','h','j','k',
    //                                  'l','m','n','p','q','r','s','t','u','v',
    //                                  'w','x','y','z',};

    //        //保存除34后的余数
    //        List<int> list = new List<int>();
    //        while (num >= 34)
    //        {
    //            int a = num % 34;
    //            num /= 34;
    //            list.Add(a);
    //        }
    //        list.Add(num);

    //        StringBuilder sb = new StringBuilder();
    //        //结果要从后往前排
    //        for (int i = list.Count - 1; i >= 0; i--)
    //        {
    //            sb.Append(rule[list[i]]);
    //        }
    //        return sb.ToString();
    //    }

    //    public int From(string convert)
    //    {
    //        if (string.IsNullOrEmpty(convert))
    //            return -1;
    //        var cs = convert.ToCharArray();
    //        char[] rule = new char[] {'0','1','2','3','4','5','6','7','8','9',
    //                                  'a','b','c','d','e','f','g','h','j','k',
    //                                  'l','m','n','p','q','r','s','t','u','v',
    //                                  'w','x','y','z',};
    //        Dictionary<char, int> dics = new Dictionary<char, int>();
    //        int i=0;
    //        foreach (var o in rule)
    //        {
    //            dics.Add(o, i);
    //            i++;
    //        }
    //        int all = 1;
    //        foreach (var c in cs)
    //        {
    //            all = all * dics[c];
    //        }
    //        return all;
    //    }
    //}


}
