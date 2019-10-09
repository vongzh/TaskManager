﻿using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using TaskManager.Web.BackgroundTask;

namespace TaskManager.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private static BackgroundTasks _backgroundtask;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            _backgroundtask = BackgroundTasks.Create();
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            Response.StatusCode = 404;
        }
    }
}