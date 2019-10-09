using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager.Utils.Common
{
    public static class ArrayHelper
    {
        /// <summary>
        /// 转换成int数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static ICollection<int> ConvertIntArray(string str, char separator)
        {
            if (string.IsNullOrEmpty(str)) return null;
            ICollection<string> array = str.Split(separator);
            ICollection<int> result = new List<int>();
            if (array != null && array.Count > 0)
            {
                int temp = 0;
                foreach (var item in array)
                {
                    if (Int32.TryParse(item, out temp))
                    {
                        result.Add(temp);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 转换成long数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static ICollection<long> ConvertLongArray(string str, char separator)
        {
            if (string.IsNullOrEmpty(str)) return null;
            ICollection<string> array = str.Split(separator);
            ICollection<long> result = new List<long>();
            if (array != null && array.Count > 0)
            {
                long temp = 0;
                foreach (var item in array)
                {
                    if (long.TryParse(item, out temp))
                    {
                        result.Add(temp);
                    }
                }
            }
            return result;
        }
        public static ICollection<long> ArrayDifference(ICollection<long> arr1, ICollection<long> arr2)
        {
            if (arr1 == null || arr1.Count <= 0) return arr2;
            if (arr2 == null || arr2.Count <= 0) return arr1;

            List<long> result = new List<long>();
            IList<long> l1 = Except(arr1, arr2).ToList();
            IList<long> l2 = ArrayDifference(arr1, arr2).ToList();
            if (l1 != null && l1.Count > 0)
            {
                result.AddRange(l1);
            }
            if (l2 != null && l2.Count > 0)
            {
                result.AddRange(l2);
            }
            return result;
        }
        public static ICollection<long> Except(ICollection<long> arr1, ICollection<long> arr2)
        {
            if (arr1 == null || arr1.Count <= 0) return arr2;
            if (arr2 == null || arr2.Count <= 0) return arr1;

            ICollection<long> result = new List<long>();
            ICollection<long> max = new List<long>();
            ICollection<long> min = new List<long>();
            if (arr1.Count > arr2.Count)
            {
                max = arr1;
                min = arr2;
            }
            else
            {
                max = arr2;
                min = arr1;
            }
            result = max.Except(min).ToList();
            return result;
        }
        public static ICollection<long> RepeatArray(ICollection<long> array1, ICollection<long> array2)
        {
            ICollection<long> result = new List<long>();

            foreach (var item in array1)
            {
                IEnumerable<long> temp = array2.Where(q => q == item);
                if (temp != null && temp.Count() > 1)
                {
                    result.Add(item);
                }
            }
            foreach (var item in array2)
            {
                IEnumerable<long> temp = array1.Where(q => q == item);
                if (temp != null && temp.Count() > 1)
                {
                    if (!result.Contains(item))
                        result.Add(item);
                }
            }
            return result;
        }
        /// <summary>
        /// 字典比较
        /// </summary>
        /// <param name="dic1"></param>
        /// <param name="dic2"></param>
        /// <returns></returns>
        public static ICollection<long> DicCompare(IDictionary<long, decimal> dic1, IDictionary<long, decimal> dic2)
        {
            if ((dic1 == null || dic1.Count <= 0) && (dic2 == null || dic2.Count <= 0)) return null;
            if ((dic1 != null && dic1.Count > 0) && (dic2 == null || dic2.Count <= 0)) return dic1.Keys.ToList();
            if ((dic2 != null && dic2.Count > 0) && (dic1 == null || dic1.Count <= 0)) return dic2.Keys.ToList();

            List<long> result = new List<long>();
            IDictionary<long, decimal> max = new Dictionary<long, decimal>();
            IDictionary<long, decimal> min = new Dictionary<long, decimal>();

            if (dic1.Count > dic2.Count)
            {
                max = dic1;
                min = dic2;
            }
            else
            {
                max = dic2;
                min = dic1;
            }

            foreach (var item in max.Keys)
            {
                if (min.ContainsKey(item))
                {
                    if (max[item] != min[item])
                    {
                        result.Add(item);
                    }
                }
                else
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
