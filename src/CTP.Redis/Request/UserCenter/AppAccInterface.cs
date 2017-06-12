using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Request.UserCenter
{

    /// <summary>
    /// 应用摄权接口
    /// </summary>
    public class AppAccInterface
    {
        /// <summary>
        /// 应用标识
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 接口名
        /// </summary>
        public string IName { get; set; }

        /// <summary>
        /// 是否开放
        /// </summary>
        public string IsOpen { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }


    }
}
