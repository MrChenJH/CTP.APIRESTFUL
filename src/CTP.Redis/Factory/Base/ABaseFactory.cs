using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NLog;
using System.Reflection;
using CTP.Redis.Request.SiteNode;
using CTP.Redis.Client;

namespace CTP.Redis
{
    public abstract class ABaseFactory
    {
        #region 属性

        public ABaseFactory()
        {
            Logger = LogManager.GetCurrentClassLogger();
            Client = RedisClient.GetInstance();
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 日记
        /// </summary>
        protected Logger Logger { get; set; }

        /// <summary>
        /// Redis 连接客户端
        /// </summary>
        public RedisClient Client { get; set; }

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
            Type t = request.GetType();
            bool isneedSync = Convert.ToBoolean(t.GetProperty("isNeedSync").GetValue(request, null));
            Client.AddZset(GetKey(), item);
            var list = new List<string>();
            list.Add(GetKey());
            if (isneedSync)
            {
                foreach (var v in item)
                {
                    list.Add(v.Value);
                }
                Client.Command(RedisCommand.lpush, list);
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
        /// 删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual ReturnData Delete(object request)
        {
            Type t = request.GetType();
            var model = t.GetProperty("Model").GetValue(request, null);
            var key = t.GetProperty("isSec").GetValue(request, null);
            if (key.Convert(0) == 88888888)
            {
                Client.GetZsetByKey("SiteNode");
                foreach (string str in Client.Result)
                {
                    var site = str.ToEntity<SiteNodeModel>();
                    Client.ClearZSet(GetKey() + site.NodeId);
                }
            }
            else
            {
                var item = GetAddOrUpdateOrDeleteValue(request);
                Client.RemoveZsetValues<Resourceobj>(GetKey() + key, item, "IDLeaf");
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
