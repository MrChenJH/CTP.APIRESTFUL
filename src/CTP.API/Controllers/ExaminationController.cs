﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CTP.Redis;
using CTP.Redis.Agent;
using CTP.Redis.Config;
using CTP.Redis.Const;
using CTP.Redis.Request;
using CTP.Redis.Request.Examination;
using CTP.Redis.Request.SiteNode;
using Microsoft.AspNetCore.Mvc;
using Util;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CTP.API.Controllers
{

    /// <summary>
    /// 菜单
    /// </summary>
    [Route("api/[controller]")]
    public class ExaminationController : BaseController
    {
         /// <summary>
         /// 更新菜单
         /// </summary>
         /// <param name="id">菜单编号</param>
         /// <param name="name">菜单名称</param>
         /// <param name="icon">菜单图标</param>
         /// <param name="url">菜单地址</param>
         /// <returns></returns>
        [HttpPost("UpdateMenu")]
        public string UpdateMenu(long id, string name, string icon,string url)
        {
            return TextInvork<string>(() =>
            {
                RequesList<Emenu> list = new RequesList<Emenu>();
                list.Model =new Emenu { Id=id,  Mname=name,Micon=icon, Mlink=url };
                list.isNeedSync = true;
                int b = Math.Abs(Guid.NewGuid().GetHashCode());
                FactoryAgent f = new FactoryAgent(list, ExecMethod.Update.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="Emenus">菜单列表</param>
        /// <returns></returns>
        [HttpPost("AddMenu")]
        public string AddMenu([FromBody]List<Emenu> Emenus)
        {
            return TextInvork<string>(() =>
            {
                RequesList<List<Emenu>> list = new RequesList<List<Emenu>>();
                list.Model = Emenus;
                list.isNeedSync = true;
                int b = Math.Abs(Guid.NewGuid().GetHashCode());
                FactoryAgent f = new FactoryAgent(list, ExecMethod.AddOrUpdate.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }

          
        /// <summary>
        /// 更新选项卡
        /// </summary>
        /// <param name="id">选项卡编号</param>
        /// <param name="tabName">选项名字</param>
        /// <param name="icon">选项卡图标</param>
        /// <returns></returns>
        [HttpPost("UpdateTab")]
        public string UpdateTab(long id,string tabName,string  icon)
        {
            return TextInvork<string>(() =>
            {
                RequesList<Etab> listTabs = new RequesList<Etab>();
                listTabs.Model = new Etab {  Id=id, MName=tabName, MIcon=icon, Mid=id};
                listTabs.isNeedSync = true;
                int b = Math.Abs(Guid.NewGuid().GetHashCode());
                FactoryAgent f = new FactoryAgent(listTabs, ExecMethod.Update.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }


        /// <summary>
        /// 新增选项卡
        /// </summary>
        /// <param name="Tabs">选项卡列表</param>
        /// <returns></returns>
        // GET: api/values
        [HttpPost("AddTab")]
        public string AddTab([FromBody]List<Etab> Tabs)
        {
            return TextInvork<string>(() =>
            {
                RequesList<List<Etab>> listTabs = new RequesList<List<Etab>>();
                listTabs.Model = Tabs;
                listTabs.isNeedSync = true;
                int b = Math.Abs(Guid.NewGuid().GetHashCode());
                FactoryAgent f = new FactoryAgent(listTabs, ExecMethod.AddOrUpdate.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }


        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="tabId">选项卡编号</param>
        /// <returns></returns>
        [HttpGet("GetMenuList")]
        public string GetMenuList(int tabId)
        {
            return ListInvork<string>(() =>
            {
                RequesList<Emenu> tab = new RequesList<Emenu>()
                {
                    isSec = 1,
                    Model = new Emenu { TabId = tabId },
                };
                FactoryAgent f = new FactoryAgent(tab, ExecMethod.Query.Convert(""));
                f.InvokeFactory();


                return (RList<string>)f.Result;
            });
        }

        /// <summary>
        /// 获取图表
        /// </summary>
        /// <param name="cmdText">图标逻辑参数</param>
        /// <returns></returns>
        [HttpGet("GetChartData")]
        public string GetChartData(string cmdText)
        {
            return ListInvork<string>(() =>
            {
                Logger.Info(cmdText);
                var json = Data.GetMySqlDataBySql(cmdText, Profile.con);
                RList<string> r = new RList<string>();
                r.sucess = true;
                r.data = json;
                return r;
            });
        }

        /// <summary>
        /// 获取所有选项卡
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTab")]
        public string GetTab()
        {
            return ListInvork<string>(() =>
             {
                 RequesList<Etab> tab = new RequesList<Etab>()
                 {
                     isSec = 1,
                     Model = new Etab(),
                 };
                 FactoryAgent f = new FactoryAgent(tab, ExecMethod.Query.Convert(""));
                 f.InvokeFactory();
                 return (RList<string>)f.Result;
             });
        }


        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">菜单</param>
        /// <returns></returns>
        [HttpDelete("DeleteMenu")]
        public string DeleteMenu(int menuId)
        {
            return TextInvork<string>(() =>
            {
                RequesList<List<Emenu>> menu = new RequesList<List<Emenu>>
                {
                    isSec = 0
                };
                menu.isNeedSync = true;
                menu.Model.Add(new Emenu
                {

                    Id = menuId
                });
                FactoryAgent f = new FactoryAgent(menu, ExecMethod.Delete.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }



        /// <summary>
        /// 删除选项卡
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteTab")]
        public string DeleteTab(int Id)
        {
            return TextInvork<string>(() =>
            {
                RequesList<List<Etab>> tab = new RequesList<List<Etab>>
                {
                    isSec = 0
                };
                tab.isNeedSync = true;
                tab.Model.Add(new Etab
                {
                    Id = Id
                });
                FactoryAgent f = new FactoryAgent(tab, ExecMethod.Delete.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }
    }
}
