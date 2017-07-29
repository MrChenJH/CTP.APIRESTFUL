using CTP.Redis.Factory.AreaBase;
using CTP.Redis.Request;
using CTP.Redis.Request.Examination;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Redis.Factory.Examination
{
    public class TabFactory : ExaminationBase, IFactory
    {


        /// <summary>
        /// 删除选项卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override ReturnData Delete(object request)
        {
            RequesList<List<Etab>> menu = (RequesList<List<Etab>>)request;
            var mode = menu.Model[0];
            Emenu menu1 = new Emenu { TabId = Convert.ToInt32(mode.AutoNo) };
            var item = GetAddOrUpdateOrDeleteValue(request);
            var items = new List<KeyValuePair<long, string>>();
         
            foreach (var v in item)
            {
                items.Add(new KeyValuePair<long, string>(v.Key, Convert.ToString(v.Key)));
            }
            var conditon = menu1.ToQueryCondition();
            Client.GetZsetMultiByValue(GetKey(), conditon);

            if (Client.Sucess) {
            Client.RemoveZsetByValues("Emenu", Client.Result);
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
            throw new NotImplementedException();
        }


        /// <summary>
        /// 查询所有选项卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ReturnData Query(object request)
        {
            RequesList<Etab> rp = (RequesList<Etab>)request;
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
            throw new NotImplementedException();
        }
    }
}
