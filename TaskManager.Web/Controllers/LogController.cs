using TaskManager.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Domain.Model;
using TaskManager.Domain.Dal;
using TaskManager.Core;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Utils.Common;
using Webdiyer.WebControls.Mvc;

namespace TaskManager.Web.Controllers
{
    [Authorize]
    public class LogController : BaseController
    {
        //
        // GET: /Log/
        public ActionResult ErrorLog(string keyword, string CStime, string CEtime, int id = -1, int errortype = -1, int taskid = -1, int nodeid = -1, int pagesize = 10, int pageindex = 1)
        {
            return this.Visit(EnumUserRole.None, () =>
            {
                int count = 0;
                using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                {
                    ViewBag.keyword = keyword; ViewBag.CStime = CStime; ViewBag.CEtime = CEtime; ViewBag.id = id; ViewBag.errortype = errortype; ViewBag.taskid = taskid;
                    ViewBag.nodeid = nodeid; ViewBag.pagesize = pagesize; ViewBag.pageindex = pageindex;
                    conn.Open();
                    tb_error_dal dal = new tb_error_dal();
                    List<tb_errorinfo_model> model = dal.GetList(conn, keyword, id, CStime, CEtime, errortype, taskid, nodeid, pagesize, pageindex, out count);
                    PagedList<tb_errorinfo_model> pageList = new PagedList<tb_errorinfo_model>(model, pageindex, pagesize, count);
                    List<tb_task_model> Task = new tb_task_dal().GetListAll(conn);
                    List<tb_node_model> Node = new tb_node_dal().GetListAll(conn);
                    ViewBag.Node = Node;
                    ViewBag.Task = Task;
                    return View(pageList);
                }
            });
        }

        public ActionResult Log(string keyword, string CStime, string CEtime, int id = -1, int logtype = -1, int taskid = -1, int nodeid = -1, int pagesize = 10, int pageindex = 1)
        {
            return this.Visit(EnumUserRole.None, () =>
            {
                ViewBag.keyword = keyword; ViewBag.CStime = CStime; ViewBag.CEtime = CEtime; ViewBag.id = id; ViewBag.logtype = logtype; ViewBag.taskid = taskid;
                ViewBag.nodeid = nodeid; ViewBag.pagesize = pagesize; ViewBag.pageindex = pageindex;
                int count = 0;
                using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                {
                    conn.Open();
                    tb_log_dal dal = new tb_log_dal();
                    List<tb_loginfo_model> model = dal.GetList(conn, keyword, id, CStime, CEtime, logtype, taskid, nodeid, pagesize, pageindex, out count);
                    PagedList<tb_loginfo_model> pageList = new PagedList<tb_loginfo_model>(model, pageindex, pagesize, count);
                    List<tb_task_model> Task = new tb_task_dal().GetListAll(conn);
                    List<tb_node_model> Node = new tb_node_dal().GetListAll(conn);
                    ViewBag.Node = Node;
                    ViewBag.Task = Task;
                    return View(pageList);
                }
            });
        }
    }
}