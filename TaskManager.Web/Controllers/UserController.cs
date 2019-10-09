using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Domain.Model;
using TaskManager.Domain.Dal;
using TaskManager.Web;
using TaskManager.Core;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Utils.Extensions;

namespace TaskManager.Web.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        //
        // GET: /Developers/

        public ActionResult Index()
        {
            return this.Visit(EnumUserRole.Admin, () =>
             {
                 using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                 {
                     conn.Open();
                     List<tb_user_model> Model = new tb_user_dal().GetAllUsers(conn);
                     return View(Model);
                 }
             });
        }

        public ActionResult Add(int? userid)
        {
            return this.Visit(EnumUserRole.Admin, () =>
            {
                if (userid == null)
                    return View();

                using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                {
                    conn.Open();
                    tb_user_dal dal = new tb_user_dal();

                    var model = dal.Get(conn, userid.Value);
                    return View(model);
                }
            });
        }

        [HttpPost]
        public ActionResult Add(tb_user_model model)
        {
            return this.Visit(EnumUserRole.Admin, () =>
            {
                if (model.id <= 0 || string.IsNullOrWhiteSpace(model.userstaffno) || string.IsNullOrWhiteSpace(model.username))
                    return RedirectToAction("index");

                model.useremail = model.useremail.NullToEmpty();
                model.usercreatetime = DateTime.Now;
                model.usertel = model.usertel.NullToEmpty();
                if (string.IsNullOrEmpty(model.userpsw))
                    model.userpsw = "000";

                using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                {
                    conn.Open();
                    tb_user_dal dal = new tb_user_dal();
                    if (model.id == 0)
                        dal.Add(conn, model);
                    else
                        dal.Edit(conn, model);
                }
                return RedirectToAction("index");
            });
        }

        public JsonResult Delete(int id)
        {
            return this.Visit(EnumUserRole.Admin, () =>
            {
                try
                {
                    tb_user_dal dal = new tb_user_dal();
                    using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                    {
                        conn.Open();
                        bool state = dal.DeleteOneNode(conn, id);
                        return Json(new { code = 1, state = state });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { code = -1, msg = ex.Message });
                }
            });
        }
    }
}
