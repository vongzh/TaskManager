using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TaskManager.Core
{
    /// <summary>
    /// 枚举操作工具类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumUtils<T> where T : struct
    {
        /// <summary>
        /// 获得指定枚举的描述信息
        /// </summary>
        /// <param name="enumInstance"></param>
        /// <returns></returns>
        public static string GetText(T enumInstance)
        {
            Type t = typeof(T);
            FieldInfo[] fieldInfoList = t.GetFields();

            var query = from q in fieldInfoList
                        where q.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false).Length > 0
                        && string.Equals(q.Name, enumInstance.ToString(), StringComparison.CurrentCultureIgnoreCase)
                        select ((System.ComponentModel.DescriptionAttribute)q.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)[0]).Description;
            if (query != null)
            {
                return query.First<string>();
            }
            return null;
            /*
            string strReturn = string.Empty;


            foreach (FieldInfo tField in fieldInfoList)
            {
                if (!tField.IsSpecialName && tField.Name.ToLower() == enumInstance.ToString().ToLower())
                {
                    DescribtionAtrtribute[] enumAttributelist = (DescribtionAtrtribute[])tField.GetCustomAttributes(typeof(DescribtionAtrtribute), false);
                    if (enumAttributelist != null && enumAttributelist.Length > 0)
                    {
                        strReturn = enumAttributelist[0].Description;
                        break;
                    }
                }
            }
            return strReturn;
             * */
        }

        /// <summary>
        /// 通过枚举，获得其枚举值
        /// </summary>
        /// <param name="enumInstance"></param>
        /// <returns></returns>
        public static string GetValue(T enumInstance)
        {
            return enumInstance.ToString();
            /*
            Type t = typeof(T);
            FieldInfo[] fieldInfoList = t.GetFields();
            string strReturn = string.Empty;
            foreach (FieldInfo tField in fieldInfoList)
            {
                if (!tField.IsSpecialName && tField.Name.ToLower() == enumInstance.ToString().ToLower())
                {
                    strReturn = tField.Name;
                    break;
                }
            }
            return strReturn;*/
        }

        /// <summary>
        /// 将字符串转换为枚举
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static T GetEnum(string Value)
        {
            Type t = typeof(T);
            FieldInfo field = t.GetField(Value);
            T e = default(T);
            System.Enum.TryParse<T>(Value, out e);
            return e;
        }


        /*public static void BindListControl(ListControl listControl)
        {
            listControl.DisplayMember = "Text";
            listControl.ValueMember = "Value";
            listControl.DataSource = GetAllEnums();
        }*/
    }
}
