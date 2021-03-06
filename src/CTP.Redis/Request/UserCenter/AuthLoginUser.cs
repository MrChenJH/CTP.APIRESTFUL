﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Request.UserCenter
{
    /// <summary>
    ///授权登录用户
    /// </summary>
    public class AuthLoginUser : Common
    {
        /// <summary>
        /// open_id
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 注册编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 授权描述
        /// </summary>
        public string UserNode { get; set; }

        /// <summary>
        /// 用户来源
        /// </summary>
        public string UserSource { get; set; }

    }
}
