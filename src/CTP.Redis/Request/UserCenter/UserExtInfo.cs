using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Request.UserCenter
{
    /// <summary>
    /// 用户扩展信息
    /// </summary>
    public class UserExtInfo : Common
    {
        /// <summary>
        /// 注册编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 所需部门
        /// </summary>
        public string UserDept { get; set; }

        /// <summary>
        /// 身份类型
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 身份类型
        /// </summary>
        public string CardNo { get; set; }
    }
}
