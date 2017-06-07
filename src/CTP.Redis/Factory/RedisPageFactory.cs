using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTP.Redis.Response;
using CTP.Redis.Request;


namespace CTP.Redis.Factory
{
    public class RedisPageFactory 
    {
        public override Result Process(Object request)
        {
            string reslutMsg = string.Empty;

            RequestPage m = (RequestPage)request;

            if (Client.IsExistKey(m.Key))
            {
                Client.GetZsetMultiByValue(m.Key, m.KeyValue, m.Start, m.Stop);
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
