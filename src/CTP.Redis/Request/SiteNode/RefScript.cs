using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Redis.Request.SiteNode
{
    public class RefScript : SiteNodeCommon
    {
        public string RefKeyId { get; set; }
        public string NodeId { get; set; }
        public string IDLeaf { get; set; }
    }
}
