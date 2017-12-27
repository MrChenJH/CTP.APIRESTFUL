using System;
using System.Collections.Generic;
using System.Text;
using CTP.Redis.Factory.AreaBase;
using CTP.Redis.Request;
using CTP.Redis.Request.SiteNode;

namespace CTP.Redis.Factory.SiteNode
{
    public class RefScriptFactory : SiteNodeBase, IFactory
    {
        public ReturnData PageQuery(object request)
        {
            throw new NotImplementedException();
        }

        public ReturnData Specialquery(object request)
        {
            return null;
        }

        public ReturnData Query(object request)
        {
            RequesList<RefScript> rp = (RequesList<RefScript>)request;
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

        public ReturnData Update(object request)
        {
            throw new NotImplementedException();
        }
    }
}
