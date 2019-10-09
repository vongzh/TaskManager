using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Domain.Model;
using TaskManager.Domain.Dal;
using Newtonsoft.Json;
using TaskManager.Web;

namespace TaskManager.Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        //登录
        [HttpGet]
        public ActionResult Login(string appid, string sign, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //登录
        [HttpPost]
        public ActionResult Login(string appid, string sign, string returnUrl, string username, string password, string validate)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("Login", "登录名或密码不能为空。");
                return View();
            }

            using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
            {
                conn.Open();
                tb_user_model model = new tb_user_dal().GetUser(conn, username, password);
                if (model != null)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        version: 1,
                        name: model.username,
                        issueDate: DateTime.Now,
                        expiration: DateTime.Now.Add(FormsAuthentication.Timeout),
                        isPersistent: true,
                        userData: JsonConvert.SerializeObject(model)
                    );
                    string enticket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie auth = new HttpCookie(FormsAuthentication.FormsCookieName, enticket);
                    Response.AppendCookie(auth);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Task");
                    }
                }
            }

            ModelState.AddModelError("Login", "登陆出错,请咨询管理员。");
            return View();
        }

        //登出
        public ActionResult Logout(string returnurl)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}
