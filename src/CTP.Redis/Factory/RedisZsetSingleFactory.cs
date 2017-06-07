using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CTP.Redis.Request;
using CTP.Redis.Response;


namespace CTP.Redis
{
    internal class RedisSingleFactory 
    {
        public override Result Process(Object request)
        {
            string reslutMsg = string.Empty;

            RequestSingle s = (RequestSingle)request;

            if (Client.IsExistKey(s.Key))
            {
                Client.GetZsetSingleByAuto(s.Key, s.Score);
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
                result.msg = Util.native2Ascii(Client.Result.FirstOrDefault(), s.isSec == 1);
            }

            return result;
        }
    }
}
