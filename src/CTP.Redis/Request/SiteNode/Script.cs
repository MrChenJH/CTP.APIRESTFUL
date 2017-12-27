using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Redis.Request.SiteNode
{
    public class Script
    {  
        public int Key { get; set; }

        public long Score { get; set; }

        public string Member { get; set; }

        public string Content { get; set; }
    }
}
