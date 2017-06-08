using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Request.UserCenter
{
    /// <summary>
    /// 用户注册
    /// </summary>
    public class RegisterUser : Common
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }


        public int UserStatus { get; set; }


        public int UserRole { get; set; }


        public int UserAuth { get; set; }


        public string UserImg { get; set; }

        [DefaultValue(null)]
        public int UserSex { get; set; }

        [DefaultValue(null)]
        public string UserBrithday { get; set; }

        [DefaultValue(null)]
        public string UserProvince { get; set; }

        [DefaultValue(null)]
        public string UserCity { get; set; }

    }
}
