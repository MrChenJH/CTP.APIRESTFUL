using System;
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
        /// 新增菜单
        /// </summary>
        /// <param name="Emenus">菜单列表</param>
        /// <returns></returns>
        // GET: api/values
        [HttpPost("AddMenu")]
        public string AddMenu([FromBody]RequesList<List<Emenu>> Emenus)
        {
            return TextInvork<string>(() =>
            {
                int b = Math.Abs(Guid.NewGuid().GetHashCode());
                FactoryAgent f = new FactoryAgent(Emenus, ExecMethod.AddOrUpdate.Convert(""));
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
        /// <param name="Tabs">选项卡</param>
        /// <returns></returns>
        // GET: api/values
        [HttpPost("AddTab")]
        public string AddTab([FromBody]RequesList<List<Etab>> Tabs)
        {
            return TextInvork<string>(() =>
            {
                int b = Math.Abs(Guid.NewGuid().GetHashCode());
                FactoryAgent f = new FactoryAgent(Tabs, ExecMethod.AddOrUpdate.Convert(""));
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
                menu.Model.Add( new Emenu
                {

                    AutoNo = menuId
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
        public string DeleteTab(int Id)        {
            return TextInvork<string>(() =>
            {
                RequesList<List<Etab>> tab = new RequesList<List<Etab>>
                {
                    isSec = 0
                  

                };
                tab.Model.Add(new Etab
                {

                    AutoNo = Id
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
