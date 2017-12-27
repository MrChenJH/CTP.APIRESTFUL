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
using StackExchange.Redis;
using CTP.Util;
using Util;
using Helpers;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CTP.API.Controllers
{
    /// <summary>
    /// 站点栏目
    /// </summary>
    [Route("api/[controller]")]
    public class SiteNodeController : BaseController
    {
        private string key1 = "hashscript";
        private string pfix = "N_";

        private string key2 = "ScriptClickRate";

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
        /// 
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet("ManuscriptQuery")]
        public string ManuscriptQuery(int nodeId, int pageSize, int pageIndex)
        {
            RPage<string> rpage = new RPage<string>();

            try
            {
                RedisClient redisClient = RedisClient.GetInstance();
                List<HashEntry> hashList = new List<HashEntry>();
                redisClient.GetZsetMultiByPage(pfix + nodeId, pageSize * (pageIndex - 1), pageSize * (pageIndex));
                rpage.data = redisClient.HashGetM(key1, redisClient.Result);
                rpage.total = redisClient.Count;
                rpage.sucess = true;

            }
            catch (Exception ex)
            {
                rpage.data = new List<string>() { ex.Message };
                rpage.total = 0;
                rpage.sucess = false;
            }
            return rpage.ToJson().RedisDataToJson();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="idleaf"></param>
        /// <returns></returns>
        [HttpGet("ManuscriptDetailQuery")]
        public string ManuscriptDetailQuery(int nodeId, int idleaf)
        {
            RList<string> r = new RList<string>();
            r.data = new List<string>();
            try
            {
                RedisClient redisClient = RedisClient.GetInstance();
                List<HashEntry> hashList = new List<HashEntry>();
                r.data.Add(redisClient.HashGetS(key1, nodeId + "_" + idleaf));
                r.sucess = true;
            }
            catch (Exception ex)
            {
                r.sucess = false;
                r.data.Add(ex.Message);

            }
            return r.ToJson().RedisDataToJson();
        }




        /// <summary>
        /// 多栏目稿件查询
        /// </summary>
        /// <param name="querrycondtion">栏目条件</param>
        /// <returns></returns>
        [HttpGet("ManyManuscriptQuery")]
        public string ManyManuscriptQuery(string querrycondtion)
        {
            RPage<string> rpage = new RPage<string>();
            rpage.data = new List<string>();
            try
            {
                RedisClient redisClient = RedisClient.GetInstance();
                List<HashEntry> hashList = new List<HashEntry>();
                var querryConditons = querrycondtion.Split(',');
                foreach (var v in querryConditons)
                {
                    var args = v.Split('|');
                    int size = Convert.ToInt32(args[1]);
                    int index = Convert.ToInt32(args[2]);
                    redisClient.GetZsetMultiByPage(pfix + args[0], size * (index - 1), size * index);
                    rpage.data.AddRange(redisClient.HashGetM(key1, redisClient.Result));
                    rpage.total += redisClient.Count;
                }
                rpage.sucess = true;

            }
            catch (Exception ex)
            {
                rpage.data = new List<string>() { ex.Message };
                rpage.total = 0;
                rpage.sucess = false;
            }
            return rpage.ToJson().RedisDataToJson();


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="nodeIds"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet("RootNodeManuscriptQuery")]
        public string RootNodeManuscriptQuery(string nodeId, string nodeIds, int pageSize, int pageIndex)
        {
            RPage<string> rpage = new RPage<string>();
            rpage.data = new List<string>();
            try
            {
                RedisClient redisClient = RedisClient.GetInstance();
                List<HashEntry> hashList = new List<HashEntry>();
                string[] nodesids = nodeIds.Split(',');
                List<string> keys = new List<string>();
                nodesids.ToList().ForEach(p => { keys.Add(pfix + p); });
                redisClient.ZUNIONSTORE(pfix + nodeId, keys.ToArray());
                redisClient.GetZsetMultiByPage(pfix + nodeId, pageSize * (pageIndex - 1), pageSize * (pageIndex));
                rpage.data = redisClient.HashGetM(key1, redisClient.Result);
                rpage.total = redisClient.Count;
                rpage.sucess = true;
            }
            catch (Exception ex)
            {
                rpage.data = new List<string>() { ex.Message };
                rpage.total = 0;
                rpage.sucess = false;
            }
            return rpage.ToJson().RedisDataToJson();

        }


        /// <summary>
        /// 稿件详情列表信息
        /// </summary>
        /// <param name="nodeId">栏目编号</param>
        /// <param name="idleafs">稿件编号数组</param> 
        /// <returns></returns>
        [HttpGet("ManuscriptListDetailQuery")]
        public string ManuscriptListDetailQuery(int nodeId, string idleafs)
        {

            RList<string> rpage = new RList<string>();
            rpage.data = new List<string>();
            try
            {
                RedisClient redisClient = RedisClient.GetInstance();
                List<HashEntry> hashList = new List<HashEntry>();
                string[] idleafss = idleafs.Split(',');
                idleafss.ToList().ForEach(t =>
                {
                    rpage.data.Add(redisClient.HashGetS(key1, nodeId + "_" + t));
                });

                rpage.sucess = true;
            }
            catch (Exception ex)
            {
                rpage.sucess = false;
                rpage.data.Add(ex.Message);

            }
            return rpage.ToJson().RedisDataToJson();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rscirpts"></param>
        /// <returns></returns>
        [HttpPost("RomoveScript")]
        public string RomoveScript([FromBody]List<RemoveScript> rscirpts)
        {
            RList<string> r = new RList<string>();
            try
            {
                RedisClient redisClient = RedisClient.GetInstance();
                rscirpts.ForEach(t =>
                {
                    redisClient.RemoveZsetByValue(pfix + t.nodeid, t.nodeid + "_" + t.idleaf);
                    redisClient.RemoveHashByField(key1, t.nodeid + "_" + t.idleaf);

                });
                r.sucess = true;
            }
            catch (Exception ex)
            {
                r.sucess = false;
                r.data.Add(ex.Message);
            }
            return r.ToJson().RedisDataToJson();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptScores"></param>
        /// <returns></returns>
        [HttpPost("EditScriptScore")]
        public string EditScriptScore([FromBody]List<Script> scriptScores)
        {
            try
            {
                RedisClient redisClient = RedisClient.GetInstance();
                List<HashEntry> hashList = new List<HashEntry>();
                scriptScores.ForEach(t =>
                {
                    redisClient.AddZset(pfix + t.Key, t.Member, Convert.ToDouble(t.Score));
                });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "success";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scripts"></param>
        /// <returns></returns>
        [HttpPost("AddScript")]
        public string AddScript([FromBody]List<Script> scripts)
        {
            try
            {
                RedisClient redisClient = RedisClient.GetInstance();
                List<HashEntry> hashList = new List<HashEntry>();
                scripts.ForEach(t =>
                {
                    redisClient.AddZset(pfix + t.Key, t.Member, Convert.ToDouble(t.Score));
                    redisClient.HashSetS(key1, t.Member, t.Content);
                });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "success";
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
            ReturnData r = new ReturnData();
            try
            {
                RedisClient redisClient = RedisClient.GetInstance();
                redisClient.ZincrbyZset(key2, nodeId + "_" + objName + "_" + idLeaf);
                r.sucess = true;
            }
            catch (Exception ex)
            {
                r.sucess = false;
            }
            return r.ToJson();
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
        /// 稿件关联图片
        /// </summary>
        /// <param name="idleaf"></param>
        /// <returns></returns>
        [HttpGet("RefImages")]
        public string RefImages(int idleaf)
        {
            RList<string> r = new RList<string>();
            try
            {

                string sql = @"select img.* from media_img_rel  imgUse 
                                        inner join  media_image        img on imgUse.img_id=img.img_id  
                                        where imgUse.mapping_val='" + idleaf + "'";

                Logger.Info(sql);
                var mysql = new MySqlHelper(Profile.con);
                var json = mysql.GetSqlDataBySql(sql);

                r.sucess = true;
                r.data = json;
                return r.ToJson().RedisDataToJson();
            }
            catch (Exception ex)
            {
                r.sucess = false;
                r.data.Add(ex.Message);
                return r.ToJson().RedisDataToJson();
            }
        }

        /// <summary>
        /// 关联稿件查询
        /// </summary>
        /// <param name="nodeid"></param>
        /// <param name="IdLeaf"></param>
        /// <returns></returns>
        [HttpGet("RefscriptQuery")]
        public string RefscriptQuery(int nodeid, int IdLeaf)
        {
            RList<string> rlist = new RList<string>();
            try
            {
                RedisClient redisClient = RedisClient.GetInstance();
                string refids = redisClient.HashGetS("ref", string.Format("{0}_{1}", nodeid, IdLeaf));
                string[] values = refids.Split(',');
                List<string> keys = new List<string>();
                values.ToList().ForEach(p => keys.Add(string.Format("{0}_{1}", IdLeaf, p)));
                var rvalues1 = redisClient.HashGetM("ref", keys);
                rlist.sucess = true;
                rlist.data = rvalues1;

            }
            catch (Exception ex)
            {
                rlist.sucess = false;
                rlist.data.Add(ex.Message);

            }
            return rlist.ToJson().RedisDataToJson();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeid"></param>
        /// <param name="IdLeaf"></param>
        /// <param name="refIdleaf"></param>
        /// <returns></returns>
        [HttpGet("RefScriptSingleQuery")]
        public string RefScriptSingleQuery(int nodeid, int IdLeaf, int refIdleaf)
        {
            RSingle<string> rlist = new RSingle<string>();
            try
            {
                RedisClient redisClient = RedisClient.GetInstance();
                string refids = redisClient.HashGetS("ref", string.Format("{0}_{1}_{2}", IdLeaf, nodeid, refIdleaf));
                rlist.sucess = true;
                rlist.Result = refids;

            }
            catch (Exception ex)
            {
                rlist.sucess = false;

            }
            return rlist.ToJson().RedisDataToJson();
        }

        /// <summary>
        /// 新增稿件
        /// </summary>
        /// <param name="refscripts"></param>
        /// <returns></returns>
        [HttpPost("AddRefScript")]
        public string AddRefScript([FromBody]List<RefScript> refscripts)
        {
            try
            {
                RedisClient redisClient = RedisClient.GetInstance();
                List<HashEntry> hashList = new List<HashEntry>();
                refscripts.ForEach(p => hashList.Add(new HashEntry(p.filed, p.content)));
                redisClient.AddZset("ref", hashList.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "success";
        }





    }
}
