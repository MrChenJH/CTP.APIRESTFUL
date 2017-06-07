using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTP.Redis.Response
{
    /// <summary>
    /// 后端结果
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool sucess { get; set; }

        /// <summary>
        /// 错误编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 总量
        /// </summary>
        public int total { get; set; }
    }
}
