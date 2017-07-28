using System;
using System.Collections.Generic;
using System.Text;
using CTP.Redis.Factory.AreaBase;
using CTP.Redis.Request;
using CTP.Redis.Request.SiteNode;
using CTP.Redis.Request.Examination;
using CTP.Redis.Client;

namespace CTP.Redis.Factory.Examination
{

    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuFactory : ExaminationBase, IFactory
    {
        public ReturnData PageQuery(object request)
        {

            throw new NotImplementedException();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override ReturnData Delete(object request)
        {
            RequesList<List<Emenu>> menu = (RequesList<List<Emenu>>)request;
            var item = GetAddOrUpdateOrDeleteValue(request);
            Client.RemoveZsetValues<Emenu>(GetKey(), item, "AutoNo");
            if (Client.Sucess)
            {
                return new ReturnData { sucess = Client.Sucess };
            }
            else
            {
                return new ErrorData { sucess = Client.Sucess, code = Client.Code };
            }
        }


        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ReturnData Query(object request)
        {

            RequesList<Emenu> rp = (RequesList<Emenu>)request;
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


        public ReturnData Specialquery(object request)
        {
            return new ReturnData();
        }

    }
}
