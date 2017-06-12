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
                    case "AuthLoginUser":
                        factory = typeof(AuthLoginUserFactory).FullName;
                        break;
                    case "AppAccInterface":
                        factory = typeof(AppAccInterfaceFactory).FullName;
                        break;
                    case "LoginLog":
                        factory = typeof(LoginLogFactory).FullName;
                        break;
                    case "RegisterApp":
                        factory = typeof(RegisterAppFactory).FullName;
                        break;
                    case "UOperLog":
                        factory = typeof(UOperLogFactory).FullName;
                        break;
                    case "UserConfig":
                        factory = typeof(UserConfigFactory).FullName;
                        break;
                    case "UserExtInfo":
                        factory = typeof(UserExtinfoFactory).FullName;
                        break;
                    case "UserInterface":
                        factory = typeof(UserInterfaceFactory).FullName;
                        break;
                    default:
                        break;
                }
                return factory;
            }
        }
    }
}
