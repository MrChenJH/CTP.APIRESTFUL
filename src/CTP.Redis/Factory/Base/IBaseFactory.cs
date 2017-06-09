
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
        ReturnData Specialquery(Object request);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        ReturnData Query(Object request);

        /// <summary>
        ///  翻页查询
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        ReturnData PageQuery(Object request);

       
    }
}
