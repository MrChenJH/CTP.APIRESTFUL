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
        /// 查询 获取注册用户信息
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

        /// <summary>
        /// 查询 授权登录用户
        /// </summary>
        /// <param name="authLoginUser">授权登录用户</param>
        /// <returns></returns>
        [HttpGet("AuthLoginUserQuery")]
        public string AuthLoginUserQuery(RequesList<AuthLoginUser> authLoginUser)
        {
            return ListInvork<string>(() =>
                    {
                        FactoryAgent f = new FactoryAgent(authLoginUser, ExecMethod.Query.Convert(""));
                        f.InvokeFactory();
                        return (RList<string>)f.Result;
                    });
        }

        /// <summary>
        /// 查询 应用授权接口
        /// </summary>
        /// <param name="appAccInterface">应用授权接口</param>
        /// <returns></returns>
        [HttpGet("AppAccInterfaceQuery")]
        public string AppAccInterfaceQuery(RequesList<AppAccInterface> appAccInterface)
        {
            return ListInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(appAccInterface, ExecMethod.Query.Convert(""));
                f.InvokeFactory();
                return (RList<string>)f.Result;
            });
        }

        /// <summary>
        /// 查询 用户扩展信息
        /// </summary>
        /// <param name="userExtInfo">用户扩展信息</param>
        /// <returns></returns>
        [HttpGet("UserExtinfoQuery")]
        public string UserExtinfoQuery(RequesList<UserExtInfo> userExtInfo)
        {
            return ListInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(userExtInfo, ExecMethod.Query.Convert(""));
                f.InvokeFactory();
                return (RList<string>)f.Result;
            });
        }

        /// <summary>
        /// 查询 操作日志
        /// </summary>
        /// <param name="uOperLog">操作日志</param>
        /// <returns></returns>
        [HttpGet("UOperLogQuery")]
        public string UOperLogQuery(RequesList<UOperLog> uOperLog)
        {
            return ListInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(uOperLog, ExecMethod.Query.Convert(""));
                f.InvokeFactory();
                return (RList<string>)f.Result;
            });
        }


        /// <summary>
        /// 查询 用户配置
        /// </summary>
        /// <param name="userConfig">用户配置</param>
        /// <returns></returns>
        [HttpGet("UserConfigQuery")]
        public string UserConfigQuery(RequesList<UserConfig> userConfig)
        {
            return ListInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(userConfig, ExecMethod.Query.Convert(""));
                f.InvokeFactory();
                return (RList<string>)f.Result;
            });
        }


        /// <summary>
        /// 查询 用户接口
        /// </summary>
        /// <param name="userInterfac">用户接口</param>
        /// <returns></returns>
        [HttpGet("UserInterfaceQuery")]
        public string UserInterfaceQuery(RequesList<UserInterface> userInterfac)
        {
            return ListInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(userInterfac, ExecMethod.Query.Convert(""));
                f.InvokeFactory();
                return (RList<string>)f.Result;
            });
        }

        #endregion

        #region Post方法

        /// <summary>
        /// 新增和修改 授权登录用户信息
        /// </summary>
        /// <param name="registerUser">授权登录用户</param>
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


        /// <summary>
        /// 新增和修改 授权登录用户
        /// </summary>
        /// <param name="registerUser">授权登录用户</param>
        /// <returns></returns>
        [HttpPost("AuthLoginUserUserAddOrUpdate")]
        public string AuthLoginUserUserAddOrUpdate([FromBody]RequesList<List<AuthLoginUser>> registerUser)
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


        /// <summary>
        /// 新增和修改 应用授权接口
        /// </summary>
        /// <param name="appAccInterface">应用授权接口</param>
        /// <returns></returns>
        [HttpPost("AppAccInterfaceAddOrUpdate")]
        public string AppAccInterfaceAddOrUpdate([FromBody]RequesList<List<AppAccInterface>> appAccInterface)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(appAccInterface, ExecMethod.AddOrUpdate.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }


        /// <summary>
        /// 新增和修改 用户扩展信息
        /// </summary>
        /// <param name="userExtinfo"> 用户扩展信息</param>
        /// <returns></returns>
        [HttpPost("UserExtinfoAddOrUpdate")]
        public string UserExtinfoAddOrUpdate([FromBody]RequesList<List<UserExtInfo>> userExtinfo)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(userExtinfo, ExecMethod.AddOrUpdate.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }

        /// <summary>
        /// 新增和修改 用户配置
        /// </summary>
        /// <param name="userConfig"> 用户配置</param>
        /// <returns></returns>
        [HttpPost("UserConfigQueryAddOrUpdate")]
        public string UserConfigQueryAddOrUpdate([FromBody]RequesList<List<UserConfig>> userConfig)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(userConfig, ExecMethod.AddOrUpdate.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }

        /// <summary>
        /// 新增和修改 操作日志
        /// </summary>
        /// <param name="uOperLog"> 操作日志</param>
        /// <returns></returns>
        [HttpPost("UOperLogAddOrUpdate")]
        public string UOperLogAddOrUpdate([FromBody]RequesList<List<UserConfig>> uOperLog)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(uOperLog, ExecMethod.AddOrUpdate.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }


        /// <summary>
        /// 新增和修改 用户接口
        /// </summary>
        /// <param name="userInterface"> 用户接口</param>
        /// <returns></returns>
        [HttpPost("UserInterfaceAddOrUpdate")]
        public string UserInterfaceAddOrUpdate([FromBody]RequesList<List<UserInterface>> userInterface)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(userInterface, ExecMethod.AddOrUpdate.Convert(""));
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
        /// 注册用户 删除
        /// </summary>
        /// <param name="registerUser">注册用户 参数</param>
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

        /// <summary>
        /// 授权登录用户 删除
        /// </summary>
        /// <param name="authLoginrUser">授权登录用户 参数</param>
        /// <returns></returns>
        [HttpDelete("AuthLoginUserUserDelete")]
        public string AuthLoginUserUserDelete([FromBody]RequesList<List<AuthLoginUser>> authLoginrUser)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(authLoginrUser, ExecMethod.Delete.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }

        /// <summary>
        /// 应用授权接口 删除
        /// </summary>
        /// <param name="appAccInterface">应用授权接口</param>
        /// <returns></returns>
        [HttpDelete("AppAccInterfaceDelete")]
        public string AppAccInterfaceDelete([FromBody]RequesList<List<AppAccInterface>> appAccInterface)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(appAccInterface, ExecMethod.Delete.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }

        /// <summary>
        /// 用户扩展信息 删除
        /// </summary>
        /// <param name="userExtInfo">用户扩展信息</param>
        /// <returns></returns>
        [HttpDelete("UserExtinfoDelete")]
        public string UserExtinfoDelete([FromBody]RequesList<List<UserExtInfo>> userExtInfo)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(userExtInfo, ExecMethod.Delete.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }

        /// <summary>
        ///  用户配置 删除
        /// </summary>
        /// <param name="userConfig">用户配置</param>
        /// <returns></returns>
        [HttpDelete("UserConfigDelete")]
        public string UserConfigDelete([FromBody]RequesList<List<UserConfig>> userConfig)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(userConfig, ExecMethod.Delete.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }

        /// <summary>
        ///  操作日记 删除
        /// </summary>
        /// <param name="uOperLog">操作日记</param>
        /// <returns></returns>
        [HttpDelete("UOperLogDelete")]
        public string UOperLogDelete([FromBody]RequesList<List<UOperLog>> uOperLog)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(uOperLog, ExecMethod.Delete.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }

        /// <summary>
        ///  用户接口 删除
        /// </summary>
        /// <param name="userInterface">用户接口</param>
        /// <returns></returns>
        [HttpDelete("UserInterfaceDelete")]
        public string UserInterfaceDelete([FromBody]RequesList<List<UserInterface>> userInterface)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(userInterface, ExecMethod.Delete.Convert(""));
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
