using System.Web.Mvc;
using TaskManager.Web;
using TaskManager.Core;
using TaskManager.Core.Net;

namespace TaskManager.Web.Controllers
{
    public class OpenApiController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetNodeConfigInfo()
        {
            NodeAppConfigInfo nodeinfo = new NodeAppConfigInfo();
            nodeinfo.NodeID = Common.GetAvailableNode();
            nodeinfo.TaskDataBaseConnectString =  StringDESHelper.EncryptDES(Config.TaskConnectString,"vongzh");
            return Json( new  { code = 1, msg = "", data = nodeinfo, total = 0 } , JsonRequestBehavior.AllowGet);
        }

        public string Ping()
        {
            return "ok";
        }
    }
}
