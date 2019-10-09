using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TaskManager.Utils.BaseService.Monitor.SystemRuntime
{
    public enum EnumTimeWatchLogType
    {
        [Description("无")]
        None = 0,
        [Description("普通")]
        Common=1,
        [Description("API")]
        ApiUrl=2,
        [Description("SQL")]
        SqlCmd=3,
    }

    public enum EnumLogType
    {
        [Description("普通错误")]
        CommonError=1,
        [Description("系统错误")]
        SystemError=2,
        [Description("普通")]
        CommonLog=3,
        [Description("系统")]
        SystemLog=4,
    }

    public enum EnumCommonLogType
    {
        CommonLog = 3,
        SystemLog = 4,
    }

    public enum EnumErrorLogType
    {
        [Description("普通错误")]
        CommonError = 1,
        [Description("系统错误")]
        SystemError = 2,
    }

    public enum DataBaseType
    {
        [Description("群集")]
        Cluster = 1,
        [Description("管理")]
        PlatformManage = 0,
        [Description("耗时")]
        Timewatch = 2,
        [Description("日志")]
        UnityLog = 3,
    }

    /// <summary>
    /// 系统用户角色
    /// </summary>
    public enum EnumUserRole
    {
        [Description("管理员")]
        Admin = 0,
        [Description("开发人员")]
        Developer = 1,
    }
}
