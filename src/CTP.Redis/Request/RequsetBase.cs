using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTP.Redis
{
    public class RequsetBase
    {
        /// <summary>
        /// Key值
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 是否加密
        /// </summary>
        public int isSec { get; set; }



    }

}
