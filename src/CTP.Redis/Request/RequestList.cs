using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTP.Redis.Request
{
    public class RequestList : RequsetBase
    {

        public RequestList()
        {
            Set = new List<int>();
        }

        /// <summary>
        ///关联集合
        /// </summary>
        public List<int> Set { get; set; }
    }
}
