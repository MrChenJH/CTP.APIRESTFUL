using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Request.UserCenter
{

    /// <summary>
    /// 用户配置
    /// </summary>
    public class UserConfig
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int AutoNo { get; set; }

        /// <summary>
        /// 配置主键
        /// </summary>
        public string ConfigKey { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        public string ConfigValue { get; set; }

        /// <summary>
        /// 配置标识
        /// </summary>
        public string ConfigSign { get; set; }
    }
}
