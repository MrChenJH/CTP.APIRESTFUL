using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CTP.Redis.Request;
using CTP.Redis.Response;

namespace CTP.Redis
{
    internal class SiteFactory : ARedisBase
    {
        public override Result Process(Object request)
        {
            string reslutMsg = string.Empty;

            RequestList l = (RequestList)request;

            if (Client.IsExistKey(l.Key))
            {
                Client.GetList(l.Key, l.Set);
            }

            var result = new Result();

            result.sucess = Client.Sucess;

            if (!Client.Sucess)
            {
                result.code = Client.Code;
                result.msg = Client.Message;
            }
            else
            {
                result.msg = Util.native2Ascii(Client.Result.ToJson(false), l.isSec == 1);
            }

            return result;
        }
    }
}
