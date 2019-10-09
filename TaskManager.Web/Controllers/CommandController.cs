using TaskManager.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Core;
using TaskManager.Domain.Dal;
using TaskManager.Domain.Model;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Utils.Common;
using Webdiyer.WebControls.Mvc;

namespace TaskManager.Web.Controllers
{
    [Authorize]
    public class CommandController : BaseController
    {
        //
        // GET: /Command/

        public ActionResult Index(int taskid = -1, int commandstate = -1, int nodeid = -1, int pagesize = 10, int pageindex = 1)
        {
            return this.Visit(EnumUserRole.Admin, () =>
            {
                ViewBag.taskid = taskid; ViewBag.commandstate = commandstate; ViewBag.nodeid = nodeid; ViewBag.pagesize = pagesize; ViewBag.pageindex = pageindex;
                tb_command_dal dal = new tb_command_dal();
                PagedList<tb_command_model_Ex> pageList = null;
                int count = 0;
                using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                {
                    conn.Open();
                    List<tb_command_model_Ex> List = dal.GetList(conn, commandstate, taskid, nodeid, pagesize, pageindex, out count);
                    List<tb_task_model> Task = new tb_task_dal().GetListAll(conn);
                    List<tb_node_model> Node = new tb_node_dal().GetListAll(conn);
                    ViewBag.Node = Node;
                    ViewBag.Task = Task;
                    pageList = new PagedList<tb_command_model_Ex>(List, pageindex, pagesize, count);
                }
                return View(pageList);
            });
        }

        public ActionResult Add()
        {
            return this.Visit(EnumUserRole.Admin, () =>
            {
                using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                {
                    conn.Open();
                    List<tb_task_model> Task = new tb_task_dal().GetListAll(conn);
                    List<tb_node_model> Node = new tb_node_dal().GetListAll(conn);
                    ViewBag.Node = Node;
                    ViewBag.Task = Task;
                    return View();
                }
            });
        }

        [HttpPost]
        public ActionResult Add(tb_command_model model)
        {
            return this.Visit(EnumUserRole.Admin, () =>
            {
                tb_command_dal dal = new tb_command_dal();
                using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                {
                    conn.Open();
                    model.commandcreatetime = DateTime.Now;
                    dal.Add(conn, model);
                }
                return RedirectToAction("index");
            });
        }

        public ActionResult Update(int id)
        {
            return this.Visit(EnumUserRole.Admin, () =>
            {
                using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                {
                    conn.Open();
                    tb_command_dal dal = new tb_command_dal();
                    tb_command_model_Ex model = dal.GetOneCommand(conn, id);
                    List<tb_node_model> Node = new tb_node_dal().GetListAll(conn);
                    ViewBag.Node = Node;
                    return View(model);
                }
            });
        }

        [HttpPost]
        public ActionResult Update(tb_command_model model)
        {
            return this.Visit(EnumUserRole.Admin, () =>
            {
                tb_command_dal dal = new tb_command_dal();
                using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                {
                    conn.Open();
                    model.commandcreatetime = DateTime.Now;
                    dal.UpdateCommand(conn, model);
                }
                return RedirectToAction("index");
            });
        }

        public JsonResult Delete(int id)
        {
            return this.Visit(EnumUserRole.Admin, () =>
            {
                using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                {
                    conn.Open();
                    tb_command_dal dal = new tb_command_dal();
                    dal.Delete(conn, id);
                    return Json(new { code = 1, msg = "Success" });
                }
            });
        }
    }
}
