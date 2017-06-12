using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Request.UserCenter
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class UOperLog
    {

        /// <summary>
        /// 注册编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 操作模块
        /// </summary>
        public string OperMoudle { get; set; }


        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperType { get; set; }


        /// <summary>
        /// 历史内容
        /// </summary>
        public string OldContent { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string OperContent { get; set; }


        /// <summary>
        /// 操作时间
        /// </summary>
        public string OperTime { get; set; }

    }
}
