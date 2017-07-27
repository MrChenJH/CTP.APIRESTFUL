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
        [DefaultValue(0)]
        public long AutoNo { get; set; }

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
                    default:
                        break;
                }
                return factory;
            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

    }
}
