using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Core;
using TaskManager.Domain.Dal;
using TaskManager.Domain.Model;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;
using TaskManager.Web;

namespace TaskManager.Web.Controllers
{
    public class PerformanceController : BaseController
    {
        //
        // GET: /Performance/

        public ActionResult Index(string nodeid, string taskid, string orderby)
        {
            return this.Visit(EnumUserRole.None, () =>
            {
                ViewBag.nodeid = nodeid;
                ViewBag.taskid = taskid;
                ViewBag.orderby = orderby;
                tb_performance_dal dal = new tb_performance_dal();
                
                using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                {
                    conn.Open();
                    ViewBag.taskmodels = dal.GetAllWithTask(conn,nodeid,taskid,orderby,DateTime.Now.AddMinutes(-10));
                   
                }
                return View();
            });
        }
        public ActionResult NodeIndex()
        {
            return this.Visit(EnumUserRole.None, () =>
            {
                tb_performance_dal dal = new tb_performance_dal();
                using (DbConn conn = DbConfig.CreateConn(Config.TaskConnectString))
                {
                    ViewBag.nodemodels = dal.GetAllWithNode(conn, "p.nodeid desc", DateTime.Now.AddMinutes(-10));
                }
                return View();
            });
        }
    }
}
