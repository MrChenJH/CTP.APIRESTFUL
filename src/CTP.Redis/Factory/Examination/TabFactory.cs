using CTP.Redis.Client;
using CTP.Redis.Factory.AreaBase;
using CTP.Redis.Request;
using CTP.Redis.Request.Examination;
using System;
using System.Collections.Generic;
using System.Reflection;
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
            Emenu menu1 = new Emenu { TabId = Convert.ToInt32(mode.Id) };

            var items = new List<KeyValuePair<long, string>>();

            foreach (var v in menu.Model)
            {
                items.Add(new KeyValuePair<long, string>(v.Id, Convert.ToString(v.Id)));
            }

            Client.RemoveZsetValues<Etab>(GetKey(), items, "Id");

            var conditon = menu1.ToQueryCondition();
            Client.GetZsetMultiByValue("Emenu", conditon);

            if (Client.Sucess)
            {
                items.Clear();
                foreach (var v in Client.Result)
                {
                    var entity = v.ToEntity<Emenu>();
                    items.Add(new KeyValuePair<long, string>(entity.Id, v));
                }

                Client.RemoveZsetValues<Emenu>(GetKey(), items, "Id");
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
        /// 新增修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override ReturnData AddOrUpdate(object request)
        {
            RequesList<List<Etab>> tabs = (RequesList<List<Etab>>)request;
            //foreach (var tab in tabs.Model)
            //{
            //    tab.Id = Math.Abs(Guid.NewGuid().GetHashCode());
            //}

            var item = GetAddOrUpdateOrDeleteValuebyId(tabs);

            Type t = request.GetType();
            bool isneedSync = Convert.ToBoolean(t.GetProperty("isNeedSync").GetValue(request, null));

            Client.AddZset(GetKey(), item);

            if (isneedSync)
            {
                var cmdInsertText = new List<string>();
                cmdInsertText.Add(Profile.typeLink + GetKey());
                foreach (var v in tabs.Model)
                {
                    cmdInsertText.Add(new LinkCcntent<Etab> { Type = LinkType.InsertLinkType, Content = v }.ToJson());
                }
                Client.Command(RedisCommand.lpush, cmdInsertText);
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

        public ReturnData Update(object request)
        {
            RequesList<Etab> tab = (RequesList<Etab>)request;
            Type t = request.GetType();
            ////key=分数,value检索字段值
            var items = new List<KeyValuePair<long, string>>();
            items.Add(new KeyValuePair<long, string>(tab.Model.Id, Convert.ToString(tab.Model.Id)));
            var oldTab = Client.RemoveZsetValues<Etab>(GetKey(), items, "Id");

            ///是否需要同步到关系型数据库
            bool isneedSync = Convert.ToBoolean(t.GetProperty("isNeedSync").GetValue(request, null));

            if (isneedSync)
            {   ///入队
                var cmdDelText = new List<string>();
                cmdDelText.Add(Profile.typeLink + GetKey());
                cmdDelText.Add(new LinkCcntent<Etab> { Type = LinkType.DelLinkType, Content = oldTab }.ToJson());
                Client.Command(RedisCommand.lpush, cmdDelText);
            }
            var newvalue = tab.Model.MergeOldEntity<Etab>(oldTab);
            var addlist = new List<KeyValuePair<long, string>>();
            addlist.Add(new KeyValuePair<long, string>(newvalue.Id, newvalue.ToJson()));
            Client.AddZset(GetKey(), addlist);
            if (isneedSync)
            {
                var list = new List<string>();
                list.Add(Profile.typeLink + GetKey());
                list.Add(new LinkCcntent<Etab> { Type = LinkType.InsertLinkType, Content = newvalue }.ToJson());
                Client.Command(RedisCommand.lpush, list);
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
    }
}
