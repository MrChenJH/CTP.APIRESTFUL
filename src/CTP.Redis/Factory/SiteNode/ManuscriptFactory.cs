using CTP.Redis.Factory.AreaBase;
using CTP.Redis.Request;
using CTP.Redis.Request.SiteNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace CTP.Redis.Factory.SiteNode
{
    public class ManuscriptFactory : SiteNodeBase, IFactory
    {
        /// <summary>
        /// 获取 新增修改删除参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override List<KeyValuePair<long, string>> GetAddOrUpdateOrDeleteValue(object request)
        {
            Type t = request.GetType();
            var model = t.GetProperty("Model").GetValue(request, null);
            return model.ToListKeyValuePairScript();
        }

        public override ReturnData AddOrUpdate(object request)
        {
            RequesList<List<Manuscript>> list = (RequesList<List<Manuscript>>)request;
            var listkey = new List<KeyValuePair<long, string>>();
            foreach (Manuscript m in list.Model)
            {

                listkey.Add(new KeyValuePair<long, string>(m.AutoNo, m.content));
            }
            Client.AddZset(GetKey() + list.isSec, listkey);
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
            RequestPage<Manuscript> rp = (RequestPage<Manuscript>)request;
            var vs = rp.KeyValue.Split(',');
            List<string> liststr = new List<string>();
            if (!string.IsNullOrEmpty(rp.Model.content))
            {
                var list = rp.Model.content.Split(',');
                foreach (var v in list)
                {
                    string conditon = String.Format("\"{0}\":\"{1}\"", "IDLeaf", v);
                    Client.GetZsetMultiByValue(GetKey() + vs[0], conditon, rp.Start, rp.Stop);
                    liststr.AddRange(Client.Result);
                }

            }
            else
            {
                Client.Sucess = false;
            }

            if (liststr.Count() > 0)
            {

                return new RList<string>()
                {
                    sucess = true,
                    data = liststr
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
            if (!string.IsNullOrWhiteSpace(rp.Model.content))
            {
                Client.ZUNIONSTORE("Manuscript" + rp.KeyValue.Trim(), rp.Model.content.Split(',').Select(p => "Manuscript" + p).ToArray());
            }
            if (Client.Sucess)
            {
                Client.GetZsetMultiByPage(GetKey() + rp.KeyValue.Trim(), rp.Start, rp.Stop);
            }
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
