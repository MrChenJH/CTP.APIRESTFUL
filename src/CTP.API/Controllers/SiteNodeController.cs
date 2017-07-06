using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CTP.Redis.Request.SiteNode;
using CTP.Redis.Request;
using CTP.Redis.Agent;
using CTP.Redis.Const;
using CTP.Redis.Config;
using CTP.Redis;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CTP.API.Controllers
{
    [Route("api/[controller]")]
    public class SiteNodeController : BaseController
    {
        /// <summary>
        /// 栏目
        /// </summary>
        /// <param name="nodeId">栏目编号</param>
        /// <returns></returns>
        [HttpGet("SiteNodeQuery")]
        public string SiteNodeQuery(int nodeId)
        {
            return ListInvork<string>(() =>
            {
                RequesList<SiteNodeModel> registerUser = new RequesList<SiteNodeModel>()
                {
                    isSec = 1,
                    Model = new SiteNodeModel { NodeId = nodeId }
                };
                FactoryAgent f = new FactoryAgent(registerUser, ExecMethod.Query.Convert(""));
                f.InvokeFactory();
                return (RList<string>)f.Result;
            });
        }


        /// <summary>
        /// 稿件列表
        /// </summary>
        /// <param name="nodeId">栏目编号</param>
        /// <param name="pageSize">页数</param> 
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        [HttpGet("ManuscriptQuery")]
        public string ManuscriptQuery(int nodeId, int pageSize, int pageIndex)
        {
            return GridInvork<string>(() =>
            {
                RequestPage<Manuscript> registerUser = new RequestPage<Manuscript>()
                {
                    isSec = 1,
                    Model = new Manuscript { AutoNo = nodeId },
                    Start = pageSize * (pageIndex - 1),
                    Stop = pageSize * (pageIndex),
                    KeyValue = nodeId.ToString()

                };
                FactoryAgent f = new FactoryAgent(registerUser, ExecMethod.Query.Convert(""));
                f.InvokeFactory();
                return (RPage<string>)f.Result;
            });
        }


        /// <summary>
        /// 稿件详情
        /// </summary>
        /// <param name="nodeId">栏目编号</param>
        /// <param name="idleaf">页数</param> 
       /// <returns></returns>
        [HttpGet("ManuscriptDetailQuery")]
        public string ManuscriptDetailQuery(int nodeId, int idleaf)
        {
            return ListInvork<string>(() =>
            {
                RequestPage<Manuscript> registerUser = new RequestPage<Manuscript>()
                {
                    isSec = 1,
                    Model = new Manuscript { AutoNo = nodeId },
                    Start = 0,
                    Stop = 10000,
                    KeyValue = string.Format("{0}{1}{2}", nodeId.ToString(), ",", idleaf)
                };
                FactoryAgent f = new FactoryAgent(registerUser, ExecMethod.Specialquery.Convert(""));
                f.InvokeFactory();
                return (RList<string>)f.Result;
            });
        }

        /// <summary>
        /// 对象增加
        /// </summary>
        /// <param name="sitenodeModel">站点栏目</param>
        /// <returns></returns>
        [HttpPost("AddSiteNode")]
        public string AddSiteNode([FromBody]RequesList<List<SiteNodeModel>> sitenodeModel)
        {
            return TextInvork<string>(() =>
            {
                int b = Math.Abs(Guid.NewGuid().GetHashCode());
                FactoryAgent f = new FactoryAgent(sitenodeModel, ExecMethod.AddOrUpdate.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }


        /// <summary>
        /// 新增稿件
        /// </summary>
        /// <param name="manuscript">稿件信息</param>
        /// <returns></returns>
        [HttpPost("AddManuscript")]
        public string AddManuscript([FromBody]RequesList<List<Manuscript>> manuscript)
        {
            return TextInvork<string>(() =>
            {

                FactoryAgent f = new FactoryAgent(manuscript, ExecMethod.AddOrUpdate.Convert(""));
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
