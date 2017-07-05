﻿using CTP.Redis.Factory.AreaBase;
using CTP.Redis.Request;
using CTP.Redis.Request.SiteNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Factory.SiteNode
{
    public class ManuscriptFactory : SiteNodeBase, IFactory
    {

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
            throw new NotImplementedException();
        }

        public ReturnData Specialquery(object request)
        {
            throw new NotImplementedException();
        }

        public ReturnData Query(object request)
        {
            RequestPage<Manuscript> rp = (RequestPage<Manuscript>)request;
            string conditon = String.Format("\"{0}\" :\"{1}\"", "nodeId", rp.Model.AutoNo);
            Client.GetZsetMultiByPage(GetKey() + rp.KeyValue, rp.Start, rp.Stop);
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