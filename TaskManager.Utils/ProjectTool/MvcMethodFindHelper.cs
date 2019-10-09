using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TaskManager.Utils.Extensions;
using TaskManager.Utils.Serialization;

namespace TaskManager.Utils.ProjectTool
{
    /// <summary>
    /// mvc contoller 方法查找,并执行
    /// </summary>
    public class MvcMethodFindHelper
    {
        /// <summary>
        /// 查找当前mvc contoller方法，并执行
        /// 仅支持默认约定的mvc模式
        /// </summary>
        /// <param name="controllerobject"></param>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static MvcMethodInfo Find(object controllerobject, string url, Dictionary<string, object> param)
        {
            try
            {
                url = url.Replace("http://", "").Trim('/');
                string controller = url.Split('/')[0];
                string action = url.Split('/')[1];
                string namespacestr = controllerobject.GetType().Namespace;
                //仅支持dal层的数据查找（相同类名）
                var obj = Assembly.GetAssembly(controllerobject.GetType()).CreateInstance(namespacestr + "." + controller + "Controller", true);
                if (obj != null)
                {
                    var methodfind = obj.GetType().GetMethod(action, BindingFlags.Instance | BindingFlags.Public
                        | BindingFlags.IgnoreCase);
                    return new MvcMethodInfo() { Controller = obj, Method = methodfind, ControllerName = controller, ActionName = action };

                }
                return null;
            }
            catch (Exception exp)
            {
                throw new Exception("mvc找不到对应url方法" + url.NullToEmpty() + ":" + exp.Message);
            }
        }
        /// <summary>
        /// 模拟调用方法
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="method"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object Call(object controller, MethodInfo method, Dictionary<string, object> param)
        {
            try
            {
                //方法填充参数
                var ps = new List<object>();
                foreach (System.Reflection.ParameterInfo p in method.GetParameters())
                {
                    //填充名称相同的参数
                    bool findparam = false;
                    foreach (var p2 in param)
                    {
                        if (p.Name.ToLower() == p2.Key.ToLower())
                        {
                            findparam = true;
                            if (p2.Value is System.Collections.ArrayList)
                            {
                                var j = new JsonHelper();
                                ps.Add(j.Deserialize(j.Serializer(p2.Value), p.ParameterType));//重新序列化以适应新的参数类型
                            }
                            else
                                ps.Add(p2.Value);

                            //if (p.ParameterType == typeof(string[]))//数据类型
                            //{
                            //    List<string> list = new List<string>();
                            //    if (p2.Value.GetType() == typeof(System.Collections.ArrayList))
                            //    {
                            //        if (((System.Collections.ArrayList)p2.Value).Count > 0)
                            //        {
                            //            //string str = string.Join(",", (string[])((System.Collections.ArrayList)p2.Value).ToArray(typeof(string)));
                            //            var se = ((System.Collections.ArrayList)p2.Value).GetEnumerator();
                            //            while (se.MoveNext())
                            //            {
                            //                list.Add(se.Current.ToString());
                            //            }
                            //        }
                            //    }
                            //    else
                            //    {
                            //        string[] _items = p2.Value.ToString().TrimStart(new char[] { '[' }).TrimEnd(']').Replace("\r\n", "").Replace(" ", "").Replace("\"", "").TrimEnd(',').Split(new char[] { ',' });
                            //        foreach (var item in _items)
                            //        {
                            //            list.Add(item);
                            //        }
                            //    }
                            //    string[] _temparr = new string[list.Count];
                            //    var num = 0;
                            //    foreach (string tempitem in list)
                            //    {
                            //        _temparr[num] = tempitem;
                            //        num++;
                            //    }
                            //    ps.Add(_temparr);
                            //}
                            //else
                            //{
                            //    ps.Add(p2.Value);
                            //}
                            continue;
                        }
                    }
                    if (findparam == false)
                    {
                        dynamic p1 = p;//System.Reflection.RuntimeParameterInfo

                        //判断是否为nullable泛型类
                        if (p1.ParameterType.IsGenericType && p1.ParameterType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                        {
                            ps.Add(null);//可选参数
                            ////如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                            //System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(p1.ParameterType);
                            ////将convertsionType转换为nullable对的基础基元类型
                            //p1.ParameterType = nullableConverter.UnderlyingType;
                            //return Convert.ChangeType(p1.Value, p1.ParameterType);
                        }
                        //有默认参数值
                        else// if (p1.HasDefaultValue == true)
                        {
                            ps.Add(p.DefaultValue);//可选参数
                            //ps.Add(p.RawDefaultValue);
                        }
                        ////类型默认参数值
                        //else
                        //{
                        //    var ptype = p.GetType();
                        //    ps.Add(ptype.IsValueType ? Activator.CreateInstance(ptype) : null);
                        //}

                    }
                }
                var r = method.Invoke(controller, ps.ToArray());
                return r;
            }
            catch (Exception exp)
            {
                throw new Exception("mvc模拟执行方法失败:" + exp.Message);
            }
        }
        /// <summary>
        /// 自定义mvc方法信息
        /// </summary>
        public class MvcMethodInfo
        {
            public object Controller { get; set; }
            public MethodInfo Method { get; set; }
            public string ControllerName { get; set; }
            public string ActionName { get; set; }
        }
    }
}
