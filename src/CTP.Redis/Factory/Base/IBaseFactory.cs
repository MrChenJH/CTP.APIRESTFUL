using CTP.Redis.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Factory
{
    interface IFactory
    {
        /// <summary>
        /// 查询拓展
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        Result Specialquery(Object request);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        Result Query(Object request);

        /// <summary>
        ///  翻页查询
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        Result PageQuery(Object request);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Result Add(Object request);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Result Delete(Object request);
    }
}
