using CTP.Redis.Factory.UserCenter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Request.UserCenter
{
    public class Common
    {

        public int AutoNo { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DefaultValue("")]
        public string CreateTime { get; set; }


        /// <summary>
        /// 维护时间
        /// </summary>
        [DefaultValue("")]
        public string Inputtime { get; set; }

        [JsonIgnore]
        public string Factory
        {
            get
            {
                Type t = this.GetType();
                string factory = string.Empty;
                switch (t.Name)
                {
                    case "RegisterUser":
                        factory = typeof(RegisterUserFactory).FullName;
                        break;
                    default:
                        break;
                }
                return factory;
            }
        }
    }
}
