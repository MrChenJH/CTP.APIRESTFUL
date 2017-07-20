using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CTP.Redis.Request;
using CTP.Redis.Request.UserCenter;

namespace CTP.Redis.Factory.UserCenter
{
    public class RegisterAppFactory : UserCenterBase, IFactory
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
            RequesList<RegisterApp> rp = (RequesList<RegisterApp>)request;
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
