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
using CTP.Redis.Request.UserCenter;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CTP.API.Controllers
{

    /// <summary>
    /// 网站所有信息
    /// </summary>
    [Route("Api/[controller]")]
    public class SiteController : BaseController
    {
    
    }
}
