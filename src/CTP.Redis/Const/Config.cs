using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTP.Redis.Factory;

namespace CTP.Redis.Const
{
    public enum ExecMethod
    {   ///查询方法
        GetRedisData,
        ///增加
        AddRedisData
    }

    /// <summary>
    /// 工厂配置
    /// </summary>
    public class Config
    {

        public static string RedisPageFactoryName
        {
            get
            {
                return typeof(RedisPageFactory).FullName;
            }
        }


        public static string RedisListFactoryName
        {
            get
            {
                return typeof(RedisListFactory).FullName;
            }
        }

        public static string RedisSingleFactory
        {
           get
            {
                return typeof(RedisSingleFactory).FullName;
            }
        }
    }
}
