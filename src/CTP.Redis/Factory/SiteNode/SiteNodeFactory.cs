using CTP.Redis.Factory.AreaBase;
using CTP.Redis.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CTP.Redis.Request.SiteNode;

namespace CTP.Redis.Factory.SiteNode
{
    public class SiteNodeFactory : SiteNodeBase, IFactory
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
            RequesList<SiteNodeModel> rp = (RequesList<SiteNodeModel>)request;
            string conditon = rp.Model.ToQueryCondition();
            Client.GetZsetMultiByValue(GetKey(), conditon, 0);
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
