using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager.Utils.Common
{
    /// <summary>
    /// 时间类函数
    /// </summary>
    public static class TimeHelper
    {
        /// <summary>
        /// 时间是否超出指定的小时数
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static bool IsExceedTime(int hours, string endTime)
        {
            if (endTime == null) return false;
            if (hours <= 0) return false;

            DateTime time = DateTime.Parse("1970-01-01");
            if (DateTime.TryParse(endTime, out time))
            {
                DateTime now = DateTime.Now.Date;
                double difference = (now - time).TotalHours;
                if (difference > hours)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 获取两段时间的间隔时间
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static ICollection<string> IntervalTimeSpan(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime) return null;

            IList<string> result = new List<string>();
            DateTime dt1 = Convert.ToDateTime(startTime.ToString("yyyy-MM-01"));
            DateTime dt2 = Convert.ToDateTime(endTime.ToString("yyyy-MM-01"));
            while (true)
            {
                if (dt1 > dt2) break;
                result.Add(dt1.ToString("yyyy-MM-01"));
                dt1 = dt1.AddMonths(1);
            }
            return result;
        }
        public static ICollection<DateTime> IntervalTime(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime) return null;

            IList<DateTime> result = new List<DateTime>();
            DateTime dt1 = Convert.ToDateTime(startTime.ToString("yyyy-MM-01"));
            DateTime dt2 = Convert.ToDateTime(endTime.ToString("yyyy-MM-01"));
            while (true)
            {
                if (dt1 > dt2) break;
                result.Add(DateTime.Parse(dt1.ToString("yyyy-MM-01")));
                dt1 = dt1.AddMonths(1);
            }
            return result;
        }
        public static ICollection<DateTime> IntervalTimeDay(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime) return null;

            IList<DateTime> result = new List<DateTime>();
            DateTime dt1 = Convert.ToDateTime(startTime.ToString("yyyy-MM-dd"));
            DateTime dt2 = Convert.ToDateTime(endTime.ToString("yyyy-MM-dd"));
            while (true)
            {
                if (dt1 > dt2) break;
                result.Add(DateTime.Parse(dt1.ToString("yyyy-MM-dd")));
                dt1 = dt1.AddDays(1);
            }
            return result;
        }
        public static ICollection<DateTime> IntervalTimeDesc(DateTime startTime, DateTime endTime)
        {
            ICollection<DateTime> array = IntervalTime(startTime, endTime);
            if (array != null && array.Count > 0)
            {
                return array.Reverse().ToList();
            }
            return null;
        }
        public static ICollection<DateTime> IntervalTimeNext(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime) return null;

            IList<DateTime> result = new List<DateTime>();
            DateTime dt1 = Convert.ToDateTime(startTime.AddMonths(1).ToString("yyyy-MM-01"));
            DateTime dt2 = Convert.ToDateTime(endTime.ToString("yyyy-MM-01"));
            while (true)
            {
                if (dt1 > dt2) break;
                result.Add(DateTime.Parse(dt1.ToString("yyyy-MM-01")));
                dt1 = dt1.AddMonths(1);
            }
            return result;
        }
        public static ICollection<string> IntervalTimeMonthTableNext(DateTime startTime, DateTime endTime)
        {
            ICollection<DateTime> array = IntervalTimeNext(startTime, endTime);
            IList<string> result = new List<string>();
            if (array != null && array.Count > 0)
            {
                foreach (var item in array)
                {
                    result.Add(item.ToString("yyyyMM"));
                }
            }
            return result;
        }
        /// <summary>
        /// 转化成分月形式的表
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static ICollection<string> IntervalTimeMonthTable(DateTime startTime, DateTime endTime)
        {
            ICollection<DateTime> array = IntervalTime(startTime, endTime);
            IList<string> result = new List<string>();
            if (array != null && array.Count > 0)
            {
                foreach (var item in array)
                {
                    result.Add(item.ToString("yyyyMM"));
                }
            }
            return result;
        }
        /// <summary>
        /// 获取月份的时间段
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static IDictionary<DateTime, DateTime> IntervalTimeMonth(DateTime startTime, DateTime endTime)
        {
            if (startTime > endTime) return null;
            IDictionary<DateTime, DateTime> dic = new Dictionary<DateTime, DateTime>();

            DateTime dt1 = Convert.ToDateTime(startTime.ToString("yyyy-MM-dd"));
            DateTime dt2 = Convert.ToDateTime(endTime.ToString("yyyy-MM-dd"));
            while (true)
            {
                if (dt1 > dt2) break;
                DateTime lastDayOfMonth = LastDayOfMonth(dt1);
                if (endTime.Month == dt1.Month)
                {
                    lastDayOfMonth = endTime;
                }
                if (!dic.ContainsKey(dt1)) dic.Add(dt1, lastDayOfMonth);
                dt1 = FirstDayOfMonth(dt1.AddMonths(1));
            }
            return dic;
        }
        /// <summary>
        /// 取得某月的最后一天
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);
        }
        /// <summary>
        /// 获取指定月份第一天
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, 1);
        }
    }
}
