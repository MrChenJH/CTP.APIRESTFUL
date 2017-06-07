using CTP.Redis.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Factory
{
    public class RedisZsetAll 
    {
        public override Result Process(Object request)
        {
            string reslutMsg = string.Empty;

            RequsetBase m = (RequsetBase)request;

            if (Client.IsExistKey(m.Key))
            {
                Client.GetZsetByKey(m.Key);
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
                result.total = Client.Count;
                result.msg = Util.native2Ascii(Client.Result.ToJson(false), m.isSec == 1);
            }

            return result;
        }
    }
}
