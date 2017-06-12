using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Request.UserCenter
{
    /// <summary>
    /// 登陆日志
    /// </summary>
    public class LoginLog
    {
        /// <summary>
        /// 注册编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 登陆类型
        /// </summary>
        public int LoginType { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string LoginIp { get; set; }

        /// <summary>
        /// 日志时间
        /// </summary>
        public string LogTime { get; set; }


        /// <summary>
        /// 登陆内容
        /// </summary>
        public string LoginContent { get; set; }
    }
}
