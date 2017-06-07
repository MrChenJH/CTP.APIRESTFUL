﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTP.Redis
{


    public abstract class ReturnData
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool sucess { get; set; }

        /// <summary>
        /// 错误编码
        /// </summary>
        public string code { get; set; }

    }

    public class RSingle<T> : ReturnData where T : class
    {
        public T Result { get; set; }
    }

    public class RList<T> : ReturnData where T : class
    {
        public List<T> data { get; set; }
    }

    public class RPage<T> : ReturnData where T : class
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int total { get; set; }

        public List<T> data { get; set; }
    }
}
