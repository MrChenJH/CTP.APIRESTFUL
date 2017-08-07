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
            RequesList<List<RefScript>> rp = (RequesList<List<RefScript>>)request;
            List<string> strs = new List<string>();
            foreach (var v in rp.Model)
            {
                string conditon = v.ToQueryCondition();
                Client.GetZsetMultiByValue(GetKey(), conditon);
                if (Client.Sucess)
                {
                    strs.Add(String.Format("{0}\"{1}\":\"{2}\"{3}", "{", v.IDLeaf, Convert.ToString(Client.Result.Count), "}"));
                }
            }
            if (Client.Sucess)
            {
                return new RList<string>()
                {
                    sucess = true,
                    data = strs
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
