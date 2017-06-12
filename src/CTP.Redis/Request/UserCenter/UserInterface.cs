using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Request.UserCenter
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public class UserInterface : Common
    {
        /// <summary>
        /// 接口名
        /// </summary>
        public string IName { get; set; }

        /// <summary>
        /// 接口描述
        /// </summary>
        public string INote { get; set; }

        /// <summary>
        /// 接口状态
        /// </summary>
        public string IStatus { get; set; }

        /// <summary>
        ///支持平台
        /// </summary>
        public string IPlatform { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
    }
}
