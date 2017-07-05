using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CTP.Redis.Request.SiteNode;

namespace CTP.Redis.Request.SiteNode
{
    public class SiteNodeModel: SiteNodeCommon
    {
        public string NodeName { get; set; }

        public int NodeId { get; set; }

        public int[] ChildNodeIds { get; set; }

    }
}
