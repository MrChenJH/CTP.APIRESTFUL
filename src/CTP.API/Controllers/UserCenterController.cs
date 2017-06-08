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

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CTP.API.Controllers
{
    [Route("api/[controller]")]
    public class UserCenterController : BaseController
    {
        #region Get方法

        // GET: api/values
        [HttpGet]
        public string GetRegisterUser([FromBody]RequesList<RegisterUser> registerUser)
        {

            return string.Empty;
        }

        #endregion

        #region Post方法
        // POST api/values
        [HttpPost]
        public string AddOrUpdate([FromBody]RequesList<List<RegisterUser>> registerUser)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(registerUser, ExecMethod.Add.Convert(""));
                f.InvokeFactory();
                return new ReturnData();
            });
        }

        #endregion



        #region Delete方法
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion
    }
}
