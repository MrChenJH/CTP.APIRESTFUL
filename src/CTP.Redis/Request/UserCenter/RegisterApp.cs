using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Request.UserCenter
{
    public class RegisterApp : Common
    {
        /// <summary>
        /// 应用标识
        /// </summary>
        public string AppId { get; set; }


        /// <summary>
        /// 应用密钥
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }


        /// <summary>
        /// 接受登录用户类
        /// </summary>
        public int AccessUser { get; set; }

        /// <summary>
        /// 主域名
        /// </summary>
        public string AppDomain { get; set; }

        /// <summary>
        /// 接入URL
        /// </summary>
        public string AccessUrl { get; set; }

        /// <summary>
        /// 应用来源
        /// </summary>
        public string AppSource { get; set; }

        /// <summary>
        /// 所属平台
        /// </summary>
        public string AppPlatform { get; set; }

        /// <summary>
        /// 接入方式
        /// </summary>
        public string AccessStyle { get; set; }

        /// <summary>
        /// 应用描述
        /// </summary>
        public string AppNode { get; set; }

        /// <summary>
        /// 应用状态
        /// </summary>
        public int AppStatus { get; set; }

        public string Createor { get; set; }
    }
}
