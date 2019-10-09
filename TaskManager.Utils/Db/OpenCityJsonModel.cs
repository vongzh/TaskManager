using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager.Utils.Db
{
    public class OpenCityJsonModel
    {

        /// <summary>
        /// 传入城市编码
        /// </summary>
        public string InCityCode { get; set; }

        /// <summary>
        /// 返回城市编码
        /// </summary>
        public string ToCityCode { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string keyword { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
