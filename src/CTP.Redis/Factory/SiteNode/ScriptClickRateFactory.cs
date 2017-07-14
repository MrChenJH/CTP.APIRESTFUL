using CTP.Redis.Factory.AreaBase;
using CTP.Redis.Request;
using CTP.Redis.Request.SiteNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Factory.SiteNode
{
    public class ScriptClickRateFactory : SiteNodeBase, IFactory
    {
        public override ReturnData AddOrUpdate(object request)
        {
            RequesList<List<ScriptClickRate>> list = (RequesList<List<ScriptClickRate>>)request;
            Client.Zincrby(GetKey(), list.Model.ToJson());
            if (Client.Sucess)
            {
                return new ReturnData { sucess = Client.Sucess };
            }
            else
            {
                return new ErrorData { sucess = Client.Sucess, code = Client.Code };
            }
        }
        public ReturnData PageQuery(object request)
        {
            throw new NotImplementedException();
        }

        public ReturnData Specialquery(object request)
        {
            RequestPage<Manuscript> rp = (RequestPage<Manuscript>)request;
            var vs = rp.KeyValue.Split(',');
            string conditon = String.Format("\"{0}\":\"{1}\"", "IDLeaf", vs[1]);
            Client.GetZsetMultiByValue(GetKey() + vs[0], conditon, rp.Start, rp.Stop);
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

        public ReturnData Query(object request)
        {
            RequestPage<Manuscript> rp = (RequestPage<Manuscript>)request;
            string conditon = String.Format("\"{0}\":\"{1}\"", "nodeId", rp.Model.AutoNo);
            Client.GetZsetMultiByPage(GetKey() + rp.KeyValue.Trim(), rp.Start, rp.Stop);
            if (Client.Sucess)
            {

                return new RPage<string>()
                {
                    sucess = true,
                    total = Client.Count,
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
