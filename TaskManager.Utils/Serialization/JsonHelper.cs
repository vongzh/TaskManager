using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace TaskManager.Utils.Serialization
{
    /// <summary>
    /// jason 序列化方式
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// jason序列化
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public string Serializer(object o)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(o);
        }
        /// <summary>
        /// jason反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public T Deserialize<T>(string s)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<T>(s);
        }
        /// <summary>
        /// jason反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public object Deserialize(string s,Type type)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize(s,type);
        }
    }
}
