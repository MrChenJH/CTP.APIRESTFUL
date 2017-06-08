using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTP.Redis.Response;
using NLog;

namespace CTP.Redis
{
    public abstract class ABaseFactory
    {
        #region 属性

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message = string.Empty;

        /// <summary>
        /// 日记
        /// </summary>
        protected Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Redis 连接客户端
        /// </summary>
        public RedisClient Client = new RedisClient();

        #endregion

        #region  抽象方法

        public abstract string GetFactoryName(string ModleName);

        #endregion
    }
}
