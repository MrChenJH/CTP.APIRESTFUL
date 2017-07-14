using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Request.SiteNode
{
    public class ScriptClickRate : SiteNodeCommon
    {
        public string NodeId { get; set; }

        public string ObjName { get; set; }

        public string IDLeaf { get; set; }

        public string Month
        {
            get
            {
                return DateTime.Now.ToString("yyyyMM");
            }
        }
        public int ClickRate { get; set; }
    }
}
