using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Core;
using System.Web.Security;
using TaskManager.Domain.Model;
using Newtonsoft.Json;

namespace TaskManager.Web
{
    public class BaseController : Controller
    {
        /// <summary>
        /// web 访问控制器
        /// 错误拦截
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ActionResult Visit(EnumUserRole role, Func<ActionResult> action)
        {
            return this.Visit<ActionResult>(role, action);
        }

        /// <summary>
        /// web 访问控制器
        /// 错误拦截
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public T Visit<T>(EnumUserRole userrole, Func<T> action)
        {
            try
            {
                int currentRole = (int)userrole;
                int role = (int)CurrentUser.userrole;
                int id = (int)CurrentUser.id;
                if (currentRole == -1 || currentRole == role)
                {
                    ViewBag.Role = role;
                    ViewBag.Number = id;
                    return action.Invoke();
                }
                else
                {
                    throw new Exception("无权访问！");
                }
            }
            catch (Exception exp)
            {
                //异常返回
                throw exp;
            }
        }

        public tb_user_model CurrentUser
        {
            get
            {
                var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                return JsonConvert.DeserializeObject<tb_user_model>(ticket.UserData);
            }
        }
    }
}