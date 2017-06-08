using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CTP.Redis.Response;
using CTP.Redis.Request;

namespace CTP.Redis.Factory
{
    public class SiteNodeFactory : ABase
    {
        public override Result Add(object request)
        {
            Logger.Info("Add");
            throw new NotImplementedException();
        }

        public override Result Delete(object request)
        {
            Logger.Info("Delete");
            throw new NotImplementedException();
        }

        public override Result Query(object request)
        {
            Logger.Info("Query");
            string reslutMsg = string.Empty;

            RequestNode s = (RequestNode)request;

            if (Client.IsExistKey(s.Key))
            {
                Client.GetZsetSingleByAuto(s.Key, 1);
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

        public override Result PageQuery(object request)
        {
          
            throw new NotImplementedException();
        }

        public override Result Specialquery(object request)
        {
            Logger.Info("Specialquery");
            throw new NotImplementedException();
        }
    }
}
