using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Factory.AreaBase
{
    public class SiteNodeBase : ABaseFactory
    {
        public override string GetKey()
        {
            string key = string.Empty;
            Type t = this.GetType();
            switch (t.Name)
            {

                case "SiteNodeFactory":
                    key = "SiteNode";
                    break;
                case "ManuscriptFactory":
                    key = "Manuscript";
                    break;
                case "ScriptClickRateFactory":
                    key = "ScriptClickRate";
                    break;
                case "RefScriptFactory":
                    key = "RefScript";
                    break;
                default:
                    break;
            }
            return key;
        }
    }
}
