using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using CTP.Redis.Factory.Examination;
using Newtonsoft.Json;

namespace CTP.Redis.Request.Menu
{
    public class ExaminationCommon
    {
        public ExaminationCommon()
        {
            Id = 100000000;
        }
        [DefaultValue(100000000)]
        public long Id { get; set; }

        [JsonIgnore]
        public string Factory
        {
            get
            {
                Type t = this.GetType();
                string factory = string.Empty;
                switch (t.Name)
                {
                    case "Emenu":
                        factory = typeof(MenuFactory).FullName;
                        break;
                    case "Etab":
                        factory = typeof(TabFactory).FullName;
                        break;
                    default:
                        break;
                }
                return factory;
            }
        }


    }
}
