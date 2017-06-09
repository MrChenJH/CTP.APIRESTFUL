using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTP.Redis
{
    public class ErrorCode
    {
        /// <summary>
        /// Redis访问出错
        /// </summary>
        public const string ReadRedisErrorCode = "001";

        /// <summary>
        /// 值非法
        /// </summary>
        public const string IllegalValueErrorCode = "002";

        /// <summary>
        /// Keys值不存在
        /// </summary>
        public const string NotExistKeyErrorCode = "003";

        /// <summary>
        /// 系统异常
        /// </summary>
        public const string SystemErrorCode = "004";


        /// <summary>
        /// 代理异常
        /// </summary>
        public const string AgentFactoryErrorCode = "005";

    }
}
