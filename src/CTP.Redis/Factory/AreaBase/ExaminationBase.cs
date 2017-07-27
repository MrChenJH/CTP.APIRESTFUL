using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Redis.Factory.AreaBase
{
    public class ExaminationBase : ABaseFactory
    {
        public override string GetKey()
        {
            string key = string.Empty;
            Type t = this.GetType();
            switch (t.Name)
            {

                case "EMenuFactory":
                    key = "Emenu";
                    break;
                default:
                    break;
            }
            return key;
        }
    }
}
