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

namespace TaskManager.Web.Controllers
{
    [Authorize]
    public class CategoryController : BaseController
    {
        //
        // GET: /Category/

        public ActionResult Index(string keyword)
        {
            return this.Visit(EnumUserRole.Admin, () =>
            {
                using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                {
                    conn.Open();
                    tb_category_dal dal = new tb_category_dal();
                    List<tb_category_model> model = dal.GetList(conn, keyword);
                    return View(model);
                }
            });
        }

        public ActionResult Add()
        {
            return this.Visit(EnumUserRole.Admin, () =>
            {
                return View();
            });
        }

        [HttpPost]
        public ActionResult Add(tb_category_model model)
        {
            return this.Visit(EnumUserRole.Admin, () =>
            {
                using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                {
                    conn.Open();
                    tb_category_dal dal = new tb_category_dal();
                    dal.Add(conn, model.categoryname);
                    return RedirectToAction("index");
                }
            });
        }

        public JsonResult Update(tb_category_model model)
        {
            return this.Visit(EnumUserRole.Admin, () =>
            {
                using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                {
                    conn.Open();
                    tb_category_dal dal = new tb_category_dal();
                    dal.Update(conn, model);
                    return Json(new { code = 1, msg = "Success" });
                }
            });
        }

        public JsonResult Delete(int id)
        {
            return this.Visit(EnumUserRole.Admin, () =>
            {
                try
                {
                    tb_category_dal dal = new tb_category_dal();
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
