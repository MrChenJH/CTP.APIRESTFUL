using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTP.Redis.Response;
using NLog;

namespace CTP.Redis
{
    public abstract class ABase
    {
        #region 属性
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message = string.Empty;


        /// <summary>
        /// 日记
        /// </summary>
        protected static Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Redis 连接客户端
        /// </summary>
        public RedisClient Client = new RedisClient();

        #endregion

        #region  抽象方法

        /// <summary>
        /// 查询拓展
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        public abstract Result Specialquery(Object request);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        public abstract Result Query(Object request);

        /// <summary>
        ///  翻页查询
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        public abstract Result PageQuery(Object request);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract Result Add(Object request);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract Result Delete(Object request);

        #endregion
    }
}
