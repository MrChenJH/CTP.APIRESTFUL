using Microsoft.Extensions.Logging;
using NLog;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace CTP.Redis
{
    public class RedisClient
    {
 

        public RedisClient()
        {
            Result = new List<string>();
            Sucess = true;
        }

        public Boolean Sucess { get; set; }

        public string Message { get; set; }

        public string Code { get; set; }

        public List<string> Result { get; set; }

        public int Count { get; set; }

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
                var client = Connection.GetDatabase();
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
        /// 获取 ZSet 里的Value 值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void GetZsetMultiByValue(string key, string keyvalue, int start, int end)
        {
            var list = new List<string>();
            try
            {

                var client = Connection.GetDatabase();
                var result = client.SortedSetScan(key, string.Format("{0}{1}{2}", "*", keyvalue, "*"), start - end, 0, start);
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
        /// 根据key 获取所有值
        /// </summary>
        /// <param name="key"></param>
        public void GetZsetByKey(string key)
        {
            var list = new List<string>();
            try
            {
                var client = Connection.GetDatabase();
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
                var client = Connection.GetDatabase();

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
                var client = Connection.GetDatabase();
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
                var client = Connection.GetDatabase();
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
                var client = Connection.GetDatabase();
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

        #endregion

        #region 链接

        /// <summary>
        /// 链接
        /// </summary>
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("192.168.1.141,abortConnect=false");
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
