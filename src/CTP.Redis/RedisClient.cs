using Microsoft.Extensions.Logging;
using NLog;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace CTP.Redis
{
    public class RedisClient
    {

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns></returns>
        public static RedisClient GetInstance()
        {
            // 如果类的实例不存在则创建，否则直接返回
            if (rClient == null)
            {
                rClient = new RedisClient();
            }
            return rClient;
        }

        /// <summary>
        /// 客户端
        /// </summary>
        private static RedisClient rClient;


        /// <summary>
        /// Redis客户端
        /// </summary>
        private RedisClient()
        {
            Result = new List<string>();
            client = Connection.GetDatabase();
            Sucess = true;
        }

        public Boolean Sucess { get; set; }

        public string Message { get; set; }

        public string Code { get; set; }

        public List<string> Result { get; set; }

        public long Count { get; set; }

        private IDatabase client { get; set; }

        /// <summary>
        /// 日记
        /// </summary>
        protected Logger Logger = LogManager.GetCurrentClassLogger();
        #region 查询

        /// <summary>
        ///  判断 key 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsExistKey(string key)
        {
            Count = 0;
            try
            {
          
                var result = client.KeyExists(key);

                if (result)
                {
                    Sucess = true;
                }
                else
                {
                    Message = "Redis库中不存在" + key;
                    Code = ErrorCode.NotExistKeyErrorCode;
                    Sucess = false;
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Code = ErrorCode.ReadRedisErrorCode;
                Sucess = false;
            }
            return Sucess;
        }


        /// <summary>
        /// 清除Zset
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ClearZSet(string key)
        {
            Count = 0;
            try
            {
     
                var result = client.SortedSetRemoveRangeByRank(key, 0, -1);
                Sucess = true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Code = ErrorCode.ReadRedisErrorCode;
                Sucess = false;
            }
            return Sucess;
        }

        /// <summary>
        /// 获取 ZSet 里的Value 值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void GetZsetMultiByValue(string key, string keyvalue, int start, int end = 1000000)
        {
            var list = new List<string>();
            try
            {
       

                if (!string.IsNullOrWhiteSpace(keyvalue))
                {

                    var result = client.SortedSetScan(key, string.Format("{0}{1}{2}", "*", keyvalue, "*"), 1, 0, 0, CommandFlags.None);
                    var reg = new Regex("^\\d+$");
                    for (int i = 0; i < result.Count(); i++)
                    {
                        var v = result.ToList()[i].Element;
                        if (!reg.Match(v).Success)
                        {
                            list.Add(v);
                        }
                    }

                }
                else
                {
                    var result = client.SortedSetRangeByRank(key, 0, -1);
                    var reg = new Regex("^\\d+$");
                    for (int i = 0; i < result.Length; i++)
                    {
                        var v = result[i];
                        if (!reg.Match(v).Success)
                        {
                            list.Add(v);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Code = ErrorCode.ReadRedisErrorCode;
                Sucess = false;
            }

            Sucess = true;

            Count = list.Count();

            if (list.Count > 0)
            {

                Result = list.Skip(start).Take(end - start).ToList();
            }
        }

        /// <summary>
        /// 获取 ZSet 里的Value 值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void GetZsetMultiByPage(string key, int start, int end = 1000000)
        {
            var list = new List<string>();
            try
            {
        
                var result = client.SortedSetRangeByScore(key, 0, 100000000000000000, Exclude.None, Order.Descending, start, end - start);
                var reg = new Regex("^\\d+$");
                for (int i = 0; i < result.Count(); i++)
                {
                    var v = result[i];
                    if (!reg.Match(v).Success)
                    {
                        list.Add(v);
                    }
                }
                Count = client.SortedSetLength(key);
                Result = list;

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Code = ErrorCode.ReadRedisErrorCode;
                Sucess = false;
            }

            Sucess = true;


        }


        public void ZUNIONSTORE(string key, string[] keys)
        {


            try
            {


                foreach (var p in keys)
                {

                    var result = client.SortedSetCombineAndStore(SetOperation.Union, key, key, p);
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Code = ErrorCode.ReadRedisErrorCode;
                Sucess = false;
                return;
            }

            Sucess = true;
        }

        /// <summary>
        /// 根据key 获取所有值
        /// </summary>
        /// <param name="key"></param>
        public void GetZsetByKey(string key)
        {
            var list = new List<string>();
            try
            {

                var result = client.SortedSetRangeByRank(key, 0, -1);
                for (int i = 0; i < result.Count(); i++)
                {
                    list.Add(result[i]);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Code = ErrorCode.ReadRedisErrorCode;
                Sucess = false;
            }

            Sucess = true;
            Count = list.Count();
        }

        /// <summary>
        /// 根据散列获取相应 List
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void GetList(string key, List<int> fileds)
        {
            try
            {
 

                if (fileds.Count > 0)
                {
                    foreach (var t in fileds)
                    {


                        var result = client.SortedSetRangeByScore(key, t, t);

                        for (int i = 0; i < result.Length; i++)
                        {
                            Result.Add(System.Text.Encoding.UTF8.GetString(result[i]));
                        }
                    }
                }
                else
                {
                    var result = client.SortedSetRangeByRank(key, 0, -1);
                    for (int i = 0; i < result.Length; i++)
                    {
                        Result.Add(System.Text.Encoding.UTF8.GetString(result[i]));
                    }
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Code = ErrorCode.ReadRedisErrorCode;
                Sucess = false;
            }
            Sucess = true;
            Count = Result.Count();
        }

        /// <summary>
        /// 获取ZSet里面的Value 值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void GetZsetSingleByAuto(string key, int autono)
        {
            try
            {

                var result = client.SortedSetRangeByScore(key, autono, autono);
                for (int i = 0; i < result.Length; i++)
                {
                    Result.Add(System.Text.Encoding.UTF8.GetString(result[i]));
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Code = ErrorCode.ReadRedisErrorCode;
                Sucess = false;
            }
            Sucess = true;
            Count = Result.Count;
        }

        /// <summary>
        /// 获取Hash 全部信息
        /// </summary>
        /// <param name="key"></param>
        public void GetHash(string key)
        {
            try
            {

                var result = client.HashGetAll(key);
                for (int i = 0; i < result.Length; i++)
                {
                    Result.Add(System.Text.Encoding.UTF8.GetString(result[i].Name));
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Code = ErrorCode.ReadRedisErrorCode;
                Sucess = false;
            }
            Sucess = true;
            Count = Result.Count;
        }

        /// <summary>
        /// 通过分数删除
        /// </summary>
        public void ZsetDelBySocre(string key, string keyvalue)
        {
            try
            {
    
                var result = client.SortedSetScan(key, string.Format("{0}{1}{2}", "*", keyvalue, "*"), 100000, 0, 0);
                foreach (var v in result)
                {
                    client.SortedSetRemove(key, v.Element);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Code = ErrorCode.ReadRedisErrorCode;
                Sucess = false;
            }
            Sucess = true;
            Count = Result.Count;
        }

        #endregion

        #region 新增

        /// <summary>
        ///  判断 key 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool AddZset(string key, List<KeyValuePair<long, string>> values)
        {
            Count = 0;
            try
            {

                SortedSetEntry[] entryArray = new SortedSetEntry[values.Count];
                for (int i = 0; i < values.Count; i++)
                {
                    entryArray[i] = new SortedSetEntry(values[i].Value, values[i].Key);
                }

                var result = client.SortedSetAdd(key, entryArray);

                if (result > 0)
                {
                    Sucess = true;
                }
                else
                {
                    Message = "没有添加任何值";
                    Code = ErrorCode.NotExistKeyErrorCode;
                    Sucess = false;
                }

            }
            catch (Exception ex)
            {

                Message = ex.Message;
                Code = ErrorCode.ReadRedisErrorCode;
                Sucess = false;
            }
            return Sucess;
        }


        /// <summary>
        /// 增量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool Zincrby(string key, string value)
        {
            Count = 0;
            try
            {


                var result = client.SortedSetIncrement(key, value, 1);

                if (result > 0)
                {
                    Sucess = true;
                }
                else
                {
                    Message = "没有对Member";
                    Code = ErrorCode.NotExistKeyErrorCode;
                    Sucess = false;
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Code = ErrorCode.ReadRedisErrorCode;
                Sucess = false;
            }
            return Sucess;
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public bool RemoveZsetValues(string key, List<KeyValuePair<long, string>> values)
        {
            Count = 0;
            try
            {


                foreach (var p in values)
                {
                    var result = client.SortedSetRangeByScore(key, p.Key, p.Key);
                    foreach (var v1 in result)
                    {
                        string val = v1;
                        var entity = val.ToEntity<Request.SiteNode.Resourceobj>();
                        if (entity.IDLeaf.Equals(p.Value))
                        {
                            client.SortedSetRemove(key, v1);
                        }
                    }
                }
                Sucess = true; ;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Code = ErrorCode.ReadRedisErrorCode;
                Sucess = false;
            }
            return Sucess;

        }

        #endregion

        #region 链接

        /// <summary>
        /// 链接
        /// </summary>
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(Profile.redisIp + ",abortConnect=false");
        });

        /// <summary>
        /// Redis连接
        /// </summary>
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
        public IDatabase db { get; set; }

        #endregion 

    }
}
