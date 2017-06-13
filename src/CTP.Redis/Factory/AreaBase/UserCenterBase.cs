using CTP.Redis.Factory;
using CTP.Redis.Request;
using CTP.Redis.Request.UserCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CTP.Redis.Factory.UserCenter
{
    public class UserCenterBase : ABaseFactory
    {
        public override string GetKey()
        {
            string key = string.Empty;
            Type t = this.GetType();
            switch (t.Name)
            {
                ///
                case "RegisterUserFactory":
                    key = "registerUser";
                    break;
                case "AuthLoginUserFactory":
                    key = "authLoginUser";
                    break;
                case "AppAccInterfaceFactory":
                    key = "appAccInterface";
                    break;
                case "LoginLogFactory":
                    key = "loginLog";
                    break;
                case "RegisterAppFactory":
                    key = "registerApp";
                    break;
                case "UoperLogFactory":
                    key = "uoperLog";
                    break;
                case "UserConfigFactory":
                    key = "userConfig";
                    break;
                case "UserExtinfoFactory":
                    key = "userExtinfo";
                    break;
                case "UserInterfaceFactory":
                    key = "userInterface";
                    break;
                default:
                    break;
            }
            return key;
        }

    }
}
