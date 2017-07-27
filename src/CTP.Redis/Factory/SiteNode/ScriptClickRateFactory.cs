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
            var key = GetKey();
            RequesList<ScriptClickRate> scr = (RequesList<ScriptClickRate>)request;
            var value = scr.Model.ToJson();
            Client.GetZsetMultiByValue(key, scr.Model.ToQueryCondition());
            if (Client.Count > 0)
            {
                var cr = Client.Result[0].ToEntity<ScriptClickRate>();
                var klist = new List<KeyValuePair<long, string>>();
                klist.Add(new KeyValuePair<long, string>(cr.AutoNo, cr.IDLeaf));
                Client.RemoveZsetValues<ScriptClickRate>(key, klist, "IDLeaf");
                klist.Clear();
                cr.ClickRate = cr.ClickRate + 1;
                klist.Add(new KeyValuePair<long, string>(cr.AutoNo, cr.ToJson()));
                Client.AddZset(key, klist);
            }
            else
            {
                Client.GetZsetMultiByValue(key, string.Empty);
                var cr = value.ToEntity<ScriptClickRate>();
                cr.AutoNo = Client.Count + 1;
                var klist = new List<KeyValuePair<long, string>>();
                cr.ClickRate = cr.ClickRate + 1;
                klist.Add(new KeyValuePair<long, string>(cr.AutoNo, cr.ToJson()));
                Client.AddZset(key, klist);
            }
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
            RequestPage<ScriptClickRate> rp = (RequestPage<ScriptClickRate>)request;
            Client.GetZsetMultiByPage(GetKey(), rp.Start, rp.Stop);
            if (Client.Sucess)
            {
                return new RPage<string>()
                {
                    sucess = true,
                    data = Client.Result,
                     total=Client.Count
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

        public ReturnData Specialquery(object request)
        {
            throw new NotImplementedException();
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
