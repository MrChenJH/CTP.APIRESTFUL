using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTP.Redis.Request
{
    public class RequestPage : RequsetBase
    {
        /// <summary>
        /// 开始索引
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// 结束索引
        /// </summary>
        public int Stop { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyValue { get; set; }

    }
}
