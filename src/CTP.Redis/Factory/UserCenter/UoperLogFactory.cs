using CTP.Redis.Request;
using CTP.Redis.Request.UserCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Factory.UserCenter
{
    public class UOperLogFactory : UserCenterBase, IFactory
    {
        public ReturnData PageQuery(object request)
        {
            throw new NotImplementedException();
        }

        public ReturnData Specialquery(object request)
        {
            throw new NotImplementedException();
        }

        public ReturnData Query(object request)
        {
            RequesList<UOperLog> rp = (RequesList<UOperLog>)request;
            string conditon = rp.Model.ToQueryCondition();
            Client.GetZsetMultiByValue(GetKey(), conditon);
            if (Client.Sucess)
            {
                return new RList<string>()
                {
                    sucess = true,
                    data = Client.Result
                };
            }
            else
            {
                return new ErrorData()
                {
                    sucess = true,
                    code = Client.Code,
                    Occurrencetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

                };
            }
        }
    }
}
