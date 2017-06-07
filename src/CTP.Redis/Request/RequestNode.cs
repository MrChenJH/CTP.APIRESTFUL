using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Request
{
    public class RequestNode : RequsetBase
    {
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName { get; set; }
    }
}
