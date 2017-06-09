using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CTP.Redis.Request;
using CTP.Redis.Request.UserCenter;
using CTP.Redis.Agent;
using CTP.Redis.Const;
using CTP.Redis;
using CTP.Redis.Config;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CTP.API.Controllers
{
    [Route("api/[controller]")]
    public class UserCenterController : BaseController
    {
        #region Get方法

        /// <summary>
        /// 获取注册用户信息
        /// </summary>
        /// <param name="registerUser">参数</param>
        /// <returns></returns>
        [HttpGet("RegisterUserQuery")]
        public string RegisterUserQuery(RequesList<RegisterUser> registerUser)
        {

            return ListInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(registerUser, ExecMethod.Query.Convert(""));
                f.InvokeFactory();
                return (RList<string>)f.Result;
            });
        }

        #endregion

        #region Post方法

        //[HttpPost("RegisterUserQuery")]
        //public string RegisterUserQuery([FromBody]RequesList<RegisterUser> registerUser)
        //{

        //    return ListInvork<string>(() =>
        //    {
        //        FactoryAgent f = new FactoryAgent(registerUser, ExecMethod.Query.Convert(""));
        //        f.InvokeFactory();
        //        return (RList<string>)f.Result;
        //    });
        //}

        /// <summary>
        /// 新增和修改用户注册信息
        /// </summary>
        /// <param name="registerUser">用户注册提交值</param>
        /// <returns></returns>
        [HttpPost("RegisterUserAddOrUpdate")]
        public string RegisterUserAddOrUpdate([FromBody]RequesList<List<RegisterUser>> registerUser)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(registerUser, ExecMethod.AddOrUpdate.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }

        #endregion



        #region Delete方法
        /// <summary>
        /// 注册用户删除
        /// </summary>
        /// <param name="registerUser">注册用户参数</param>
        /// <returns></returns>
        [HttpDelete("RegisterUserDelete")]
        public string RegisterUserDelete([FromBody]RequesList<List<RegisterUser>> registerUser)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(registerUser, ExecMethod.Delete.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }
        #endregion
    }
}
