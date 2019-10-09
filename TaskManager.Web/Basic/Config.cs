using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TaskManager.Web
{ 
    public class Config
    {
        public static string TaskConnectString = ConfigurationManager.AppSettings["TaskConnectString"].ToString();
    }
}