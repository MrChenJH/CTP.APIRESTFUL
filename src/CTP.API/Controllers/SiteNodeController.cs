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
    /// <summary>
    /// 站点栏目
    /// </summary>
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
        /// 栏目新增
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
        /// 根节点栏目查询
        /// </summary>
        /// <param name="nodeId">栏目编号</param>
        /// <param name="nodeIds">子栏目编号数组</param>
        /// <param name="pageSize">页数</param>
        /// <param name="pageIndex">第几页</param>
        /// <returns></returns>
        [HttpGet("RootNodeManuscriptQuery")]
        public string RootNodeManuscriptQuery(string nodeId, string nodeIds, int pageSize, int pageIndex)
        {
            return GridInvork<string>(() =>
            {

                RequestPage<Manuscript> registerUser = new RequestPage<Manuscript>()
                {
                    isSec = 1,
                    Model = new Manuscript { AutoNo = Convert.ToInt64(nodeId), content = nodeIds },
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
        /// 稿件列表查询
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
        /// 稿件列表查询
        /// </summary>
        /// <param name="nodeId">栏目编号</param>
        /// <param name="pageSize">页数</param> 
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        [HttpPost("ManuscriptQueryPost")]
        public string ManuscriptQueryPost(int nodeId, int pageSize, int pageIndex)
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
        /// 多栏目稿件查询
        /// </summary>
        /// <param name="querrycondtion">栏目条件</param>
        /// <returns></returns>
        [HttpGet("ManyManuscriptQuery")]
        public string ManyManuscriptQuery(string querrycondtion)
        {
            return ListInvork<string>(() =>
            {
                RequestPage<Manuscript> mqueryy = new RequestPage<Manuscript>()
                {
                    isSec = 3,
                    Model = new Manuscript { content = querrycondtion }
                };
                FactoryAgent f = new FactoryAgent(mqueryy, ExecMethod.Specialquery.Convert(""));
                f.InvokeFactory();
                return (RList<string>)f.Result;
            });
        }


        /// <summary>
        /// 稿件详情查询
        /// </summary>
        /// <param name="nodeId">栏目编号</param>
        /// <param name="idleaf">稿件编号</param> 
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
        /// 稿件详情列表信息
        /// </summary>
        /// <param name="nodeId">栏目编号</param>
        /// <param name="idleafs">稿件编号数组</param> 
        /// <returns></returns>
        [HttpGet("ManuscriptListDetailQuery")]
        public  string ManuscriptListDetailQuery(int nodeId, string idleafs)
        {
            return ListInvork<string>(() =>
            {
                RequestPage<Manuscript> registerUser = new RequestPage<Manuscript>()
                {
                    isSec = 1,
                    Model = new Manuscript { AutoNo = nodeId, content = idleafs },
                    Start = 0,
                    Stop = 10000,
                    KeyValue = string.Format("{0}{1}{2}", nodeId.ToString(), ",", "1")
                };
                FactoryAgent f = new FactoryAgent(registerUser, ExecMethod.PageQuery.Convert(""));
                f.InvokeFactory();
                return (RList<string>)f.Result;
            });
        }

        /// <summary>
        /// 稿件新增
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

        /// <summary>
        /// 稿件点击率
        /// </summary>
        /// <param name="nodeId">栏目编号</param>
        /// <param name="objName">对象名称</param>
        /// <param name="idLeaf">稿件编号</param>
        /// <returns></returns>
        [HttpPost("AddScriptRate")]
        public string AddScriptRate(int nodeId, string objName, string idLeaf)
        {
            return TextInvork<string>(() =>
            {
                RequesList<ScriptClickRate> manuscript = new RequesList<ScriptClickRate>();
                manuscript.Model = new ScriptClickRate { AutoNo = 0, NodeId = Convert.ToString(nodeId), IDLeaf = idLeaf, ObjName = objName };
                FactoryAgent f = new FactoryAgent(manuscript, ExecMethod.AddOrUpdate.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }


        /// <summary>
        /// 点击率查询
        /// </summary>
        /// <param name="pageSize">页数</param>
        /// <param name="pageIndex">第几页</param>
        /// <returns></returns>
        [HttpGet("ScriptRateQuery")]
        public string ScriptRateQuery(int pageSize, int pageIndex)
        {
            return GridInvork<string>(() =>
            {
                RequestPage<ScriptClickRate> registerUser = new RequestPage<ScriptClickRate>()
                {
                    isSec = 1,
                    Model = new ScriptClickRate { AutoNo = 0 },
                    Start = pageSize * (pageIndex - 1),
                    Stop = pageSize * (pageIndex),
                    KeyValue = ""
                };
                FactoryAgent f = new FactoryAgent(registerUser, ExecMethod.PageQuery.Convert(""));
                f.InvokeFactory();
                return (RPage<string>)f.Result;
            });
        }


        /// <summary>
        /// 删除稿件
        /// </summary>
        /// <param name="msrcripts">稿件详情列表</param>
        /// <returns></returns>
        [HttpPost("RomoveScript")]
        public string RomoveScript([FromBody]RequesList<List<Manuscript>> msrcripts)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(msrcripts, ExecMethod.Delete.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });
        }

        /// <summary>
        /// 清除稿件
        /// </summary>
        /// <param name="pwd">权限密码</param>
        /// <returns></returns>
        [HttpPost("ClearScript")]
        public string ClearScript(string pwd)
        {
            if (pwd == "cjh1qaz")
            {
                return TextInvork<string>(() =>
            {
                RequesList<Manuscript> msrcripts = new RequesList<Manuscript>();
                msrcripts.isSec = 88888888;

                FactoryAgent f = new FactoryAgent(msrcripts, ExecMethod.Delete.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }

                return (ReturnData)f.Result;

            });
            }
            else
            {
                return "密码不对";
            }
        }


        /// <summary>
        /// 关联稿件信息
        /// </summary>
        /// <param name="idLeaf">稿件编号</param>
        /// <returns></returns> 
        [HttpGet("RefscriptQuery")]
        public string RefscriptQuery(string idLeaf)
        {
            return TextInvork<string>(() =>
            {
                RequesList<RefScript> reqRefScript = new RequesList<RefScript>();
                reqRefScript.Model = new RefScript { IDLeaf = idLeaf };
                FactoryAgent f = new FactoryAgent(reqRefScript, ExecMethod.Query.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });

        }

        /// <summary>
        /// 关联稿件信息数量
        /// </summary>
        /// <param name="idLeafs">稿件编号数组</param>
        /// <returns></returns> 
        [HttpGet("RefscriptNumQuery")]
        public string RefscriptNumQuery(string idLeafs)
        {
            return TextInvork<string>(() =>
            {
                if (string.IsNullOrEmpty(idLeafs))
                {
                    throw new ProcessException("idLeafs 为空");
                }
                var ids = idLeafs.Split(',');
                RequesList<List<RefScript>> reqRefScript = new RequesList<List<RefScript>>();
                foreach (var v in ids)
                {
                    reqRefScript.Model.Add(new RefScript { IDLeaf = v });
                }
                FactoryAgent f = new FactoryAgent(reqRefScript, ExecMethod.Specialquery.Convert(""));
                f.InvokeFactory();
                if (!f.Result.sucess)
                {
                    throw new ProcessException(f.Result.ToJson());
                }
                return (ReturnData)f.Result;
            });

        }



        /// <summary>
        /// 新增关联稿件
        /// </summary>
        /// <param name="refscripts">关连稿件编号数组</param>
        /// <returns></returns>
        [HttpPost("AddRefScript")]
        public string AddRefScript([FromBody]RequesList<List<RefScript>> refscripts)
        {
            return TextInvork<string>(() =>
            {
                FactoryAgent f = new FactoryAgent(refscripts, ExecMethod.AddOrUpdate.Convert(""));
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
