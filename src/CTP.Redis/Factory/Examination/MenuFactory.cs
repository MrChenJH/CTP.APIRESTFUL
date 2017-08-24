using System;
using System.Collections.Generic;
using System.Text;
using CTP.Redis.Factory.AreaBase;
using CTP.Redis.Request;
using CTP.Redis.Request.SiteNode;
using CTP.Redis.Request.Examination;
using CTP.Redis.Client;
using System.Reflection;
using CTP.Redis;

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
        /// 新增修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override ReturnData AddOrUpdate(object request)
        {
            RequesList<List<Emenu>> menu = (RequesList<List<Emenu>>)request;
            //foreach (var v in menu.Model)
            //{
            //    v.Id = Math.Abs(Guid.NewGuid().GetHashCode());
            //}

            var item = GetAddOrUpdateOrDeleteValuebyId(menu);
            Type t = request.GetType();
            bool isneedSync = Convert.ToBoolean(t.GetProperty("isNeedSync").GetValue(request, null));
            Client.AddZset(GetKey(), item);

            if (isneedSync)
            {
                var cmdInsertText = new List<string>();
                cmdInsertText.Add(Profile.typeLink + GetKey());
                foreach (var v in menu.Model)
                {
                    cmdInsertText.Add(new LinkCcntent<Emenu> { Type = LinkType.InsertLinkType, Content = v }.ToJson());
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
        /// 删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override ReturnData Delete(object request)
        {
            RequesList<List<Emenu>> menu = (RequesList<List<Emenu>>)request;
            var item = GetAddOrUpdateOrDeleteValuebyId(request);
            var items = new List<KeyValuePair<long, string>>();


            foreach (var v in item)
            {
                items.Add(new KeyValuePair<long, string>(v.Key, Convert.ToString(v.Key)));
            }

            Client.RemoveZsetValues<Emenu>(GetKey(), items, "Id");
            if (Client.Sucess)
            {
                return new ReturnData { sucess = Client.Sucess };
            }
            else
            {
                return new ErrorData { sucess = Client.Sucess, code = Client.Code };
            }
        }

        private List<MenuResult> EmenuList = new List<MenuResult>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="menu"></param>
        /// <param name="emenuList"></param>
        private void SetMenu(string parentId, MenuResult menu, List<Emenu> emenuList)
        {
            for (int i = 0; i < emenuList.Count; i++)
            {
                if (emenuList[i].ParentId == parentId)
                {
                    var re = new MenuResult();
                    re.TabId = Convert.ToString(emenuList[i].TabId);
                    re.Id = Convert.ToString(emenuList[i].Id);
                    re.icon = emenuList[i].Micon;
                    re.name = emenuList[i].Mname;
                    re.url = emenuList[i].Mlink;
                    SetMenu(Convert.ToString(emenuList[i].Id), re, emenuList);
                    if (parentId.Equals("0"))
                    {
                        EmenuList.Add(re);
                    }
                    else
                    {
                        menu.Child.Add(re);
                    }
                }
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
            if (rp.isSec == 10)
            {

            }
            string conditon = rp.Model.ToQueryCondition();
            Client.GetZsetMultiByValue(GetKey(), conditon);

            List<Emenu> listMenu = new List<Emenu>();
            if (Client.Sucess)
            {
                foreach (var c in Client.Result)
                {
                    var v = c.ToEntity<Emenu>();
                    listMenu.Add(v);
                }


                SetMenu("0", null, listMenu);
                List<string> str = new List<string>();
                foreach (var p in EmenuList)
                {
                    str.Add(p.ToJson());
                }
                return new RList<string>()
                {
                    sucess = true,
                    data = str
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

        public ReturnData Update(object request)
        {
            RequesList<Emenu> menu = (RequesList<Emenu>)request;
            Type t = request.GetType();
            ////key=分数,value检索字段值
            var items = new List<KeyValuePair<long, string>>();
            items.Add(new KeyValuePair<long, string>(menu.Model.Id, Convert.ToString(menu.Model.Id)));
            var oldMenu = Client.RemoveZsetValues<Emenu>(GetKey(), items, "Id");

            ///是否需要同步到关系型数据库
            bool isneedSync = Convert.ToBoolean(t.GetProperty("isNeedSync").GetValue(request, null));

            if (isneedSync)
            {   ///入队
                var cmdDelText = new List<string>();
                cmdDelText.Add(Profile.typeLink + GetKey());
                cmdDelText.Add(new LinkCcntent<Emenu> { Type = LinkType.DelLinkType, Content = oldMenu }.ToJson());
                Client.Command(RedisCommand.lpush, cmdDelText);
            }
            var newvalue = menu.Model.MergeOldEntity<Emenu>(oldMenu);

            var addlist = new List<KeyValuePair<long, string>>();
            addlist.Add(new KeyValuePair<long, string>(newvalue.Id, newvalue.ToJson()));
            Client.AddZset(GetKey(), addlist);
            if (isneedSync)
            {
                var list = new List<string>();
                list.Add(Profile.typeLink + GetKey());
                list.Add(new LinkCcntent<Emenu> { Type = LinkType.InsertLinkType, Content = newvalue }.ToJson());
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
