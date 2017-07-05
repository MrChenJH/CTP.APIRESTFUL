using CTP.Redis.Factory.SiteNode;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Request.SiteNode
{
    public class SiteNodeCommon
    {
        public int AutoNo { get; set; }

        [JsonIgnore]
        public string Factory
        {
            get
            {
                Type t = this.GetType();
                string factory = string.Empty;
                switch (t.Name)
                {
                    case "SiteNodeModel":
                        factory = typeof(SiteNodeFactory).FullName;
                        break;
                    case "Manuscript":
                       factory = typeof(ManuscriptFactory).FullName;
                        break;
                    default:
                        break;
                }
                return factory;
            }
        }
    }
}
