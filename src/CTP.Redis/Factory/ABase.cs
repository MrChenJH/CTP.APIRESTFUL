using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTP.Redis.Response;


namespace CTP.Redis
{
    public abstract class ARedisBase
    {
        #region 属性
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message = string.Empty;

        /// <summary>
        /// Redis 连接客户端
        /// </summary>
        public RedisClient Client = new RedisClient();

        #endregion



        #region  抽象方法

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        public abstract Result Select(Object request);
       
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        public abstract Result SelectList(Object request);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        public abstract Result SelectPage(Object request);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract Result Add(Object request);

        #endregion
    }
}
