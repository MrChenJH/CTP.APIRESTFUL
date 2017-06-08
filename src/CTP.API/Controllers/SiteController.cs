using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CTP.Redis.Request;
using CTP.Redis;
using CTP.Redis.Agent;
using CTP.Redis.Const;
using Microsoft.Extensions.Logging;
using NLog;
using System.Threading;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CTP.API.Controllers
{

    /// <summary>
    /// 网站所有信息
    /// </summary>
    [Route("Api/[controller]")]
    public class SiteController : BaseController
    {
        
        /// <summary>
        /// 获取当前栏目子栏目信息
        /// </summary>
        /// <param name="requestParam">参数</param>
        /// <returns></returns>
        [HttpPost("Nodes")]
        public string GetChildNodes([FromBody]RequestNode requestParam)
        {
            Logger.Info("Server is running...");
            Logger.Info(string.Format("Current Thead Id:{0}", Thread.CurrentThread.ManagedThreadId));
            return GridInvork<string>(() =>
                        {
                            FactoryAgent f = new FactoryAgent(Config.RedisPageFactoryName, requestParam, ExecMethod.GetRedisData.Convert(""));
                            f.InvokeFactory();

                            if (f.Result.sucess)
                            {
                                return new RPage<string>()
                                {
                                    sucess = true,
                                    total = f.Result.total,
                                    data = f.Result.msg.ToEntity<List<string>>()
                                };
                            }
                            else
                            {
                                return new RPage<string>()
                                {
                                    sucess = false,
                                    code = f.Result.code
                                };
                            }
                        });
        }
    }
}
