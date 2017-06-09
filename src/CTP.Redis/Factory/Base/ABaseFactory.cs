using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NLog;
using System.Reflection;

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

        #region 虚方法
        /// <summary>
        /// 新增修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual ReturnData AddOrUpdate(object request)
        {
            var item = GetAddOrUpdateOrDeleteValue(request);
            Client.AddZset(GetKey(), item);
            if (Client.Sucess)
            {
                return new ReturnData { sucess = Client.Sucess };
            }
            else
            {
                return new ErrorData { sucess = Client.Sucess, code = Client.Code };
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual ReturnData Delete(object request)
        {
            var items = GetAddOrUpdateOrDeleteValue(request);
            foreach (var v in items)
            {
                Client.ZsetDelBySocre(GetKey(), v.Key);
                if (!Client.Sucess)
                {
                    break;
                }
            }
            if (Client.Sucess)
            {
                return new ReturnData { sucess = Client.Sucess };
            }
            else
            {
                return new ErrorData { sucess = Client.Sucess, code = Client.Code };
            }
        }

        /// <summary>
        /// 获取 新增修改删除参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual List<KeyValuePair<long, string>> GetAddOrUpdateOrDeleteValue(object request)
        {
            Type t = request.GetType();
            var model = t.GetProperty("Model").GetValue(request, null);
            return model.ToListKeyValuePair();
        }

        #endregion

        #region  抽象方法

        /// <summary>
        ///获取对应表名 
        /// </summary>
        /// <returns></returns>
        public abstract string GetKey();

        #endregion
    }
}
