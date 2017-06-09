using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Request.UserCenter
{
    public class AuthLoginUser : Common
    {
        public string OpenId { get; set; }
        public string UserId { get; set; }
        public string UserNode { get; set; }
        public string UserSource { get; set; }

    }
}
