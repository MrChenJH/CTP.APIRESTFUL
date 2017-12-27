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
using Util;
using CTP.Util;
using System.Text;
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
        public string UpdateMenu(long id, string name, string icon, string url)
        {
            return TextInvork<string>(() =>
            {
                RequesList<Emenu> list = new RequesList<Emenu>();
                list.Model = new Emenu { Id = id, Mname = name, Micon = icon, Mlink = url };
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
        public string UpdateTab(long id, string tabName, string icon)
        {
            return TextInvork<string>(() =>
            {
                RequesList<Etab> listTabs = new RequesList<Etab>();
                listTabs.Model = new Etab { Id = id, MName = tabName, MIcon = icon, Mid = id };
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


        class Va
        {
            public int c { get; set; }
        }

        /// <summary>
        /// 教育考试稿件
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="NodeId"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpGet("ExamScripts")]
        public string ExamScripts(string Title, int NodeId = 0, int PageIndex = 0, int PageSize = 0)
        {
            try
            {
                JgridResult result = new JgridResult();
                int start = (PageIndex - 1) * PageSize;
                int end = PageIndex * PageSize;
                string count = @" select count(1) as c from dbo.resourceobj79 r79 inner join 
                                 node_objdata objdata on r79.IDLeaf=objdata.ID_Leaf where   " + (NodeId != 0 ? "objdata.node_id = " + NodeId + "  and  " : "") + " r79.name like '%" + Title + "%'";
                var listc = SqlHepler.GetSqlDataBySql(count);
                var v = listc[0].ToEntity<Va>();

                string sql = @"select * from (
                                 select row_number()over(order by createtime desc) as num, IDLeaf,name,content,createtime from dbo.resourceobj79 r79 inner join 
                                 node_objdata objdata on r79.IDLeaf=objdata.ID_Leaf where   " + (NodeId != 0 ? "objdata.node_id = " + NodeId + "  and  " : "") + " r79.name like '%" + Title + "%') as data where num between " + start + " and " + end;

                var list = SqlHepler.GetSqlDataBySql(sql);
                result.records = v.c;
                result.total = v.c % PageSize == 0 ? v.c / PageSize : v.c / PageSize + 1;
                result.page = PageIndex;
                result.rows = SqlHepler.GetSqlDataBySql(sql);
                return result.ToJson().RedisDataToJson();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// 稿件调整
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        [HttpGet("AjustMentScripts")]
        public string AjustMentScripts(string code)
        {
            RList<string> r = new RList<string>();
            r.data = new List<string>();
            try
            {
                var sb = new StringBuilder();
                var insertstring = new StringBuilder();
                sb.AppendLine("select row_number()over(order by order_no, r79.createtime desc) as Rno,sitenode.node_name as NName,r79.name as name,ndata.order_no,r79.IDLeaf");
                sb.AppendLine("from resourceobj79 r79 ");
                sb.AppendLine("inner join node_objdata ndata     on r79.IDLeaf = ndata.ID_Leaf ");
                sb.AppendLine("inner join site_node sitenode  on ndata.node_id = sitenode.node_id ");
                sb.AppendLine("where sitenode.node_id=227");
                insertstring.AppendLine(" insert into node_objdataOrder ");
                insertstring.AppendLine(" select  " + code + ",a.* from dbo.node_objdata  a inner join  resourceobj79  b on a.ID_Leaf=b.IDLeaf  where a.node_id=227");
                string sql = "update a set order_no = b.rno from[node_objdataOrder] a inner join(" + sb.ToString() + ") b on a.ID_Leaf = b.IDLeaf and a.id = " + code;
                new SqlHepler().ExecuteNonQuery(insertstring.ToString());
                new SqlHepler().ExecuteNonQuery(sql);
                r.data = SqlHepler.GetSqlDataBySql(sb.ToString());
                r.sucess = true;
            }
            catch (Exception ex)
            {
                r.sucess = false;
                r.data.Add(ex.Message);
            }
            return r.ToJson().RedisDataToJson();
        }





        class JgridResult
        {
            public int records { get; set; }
            public int page { get; set; }
            public int total { get; set; }

            public List<string> rows { get; set; }
        }

        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpPost("AddENode")]
        public string AddENode(string nodeName, string path)
        {
            try
            {
                var shelper = new SqlHepler();
                string sql = "select max(node_id)+1 as c  from  site_node ";
                var listc = SqlHepler.GetSqlDataBySql(sql);
                var v = listc[0].ToEntity<Va>();
                sql = @"insert into site_node
                              (node_id,site_code,node_name,node_type,map_type,map_key,node_status,is_audit,audit_wf,is_review,is_auedit,creator,createtime,inputtime,isdept,urlPre)
                              values
                              (" + v.c + ",'FEBCFD753B600F0B','" + nodeName + "',0,0,'教育考试院对象',0,1,'教育考试院对象',0,1,'Creator','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "',0,'gg')";
                shelper.ExecuteNonQuery(sql);
                sql = @"insert into  node_net
                                        (node_id,node_pid,node_path,order_no)
                                  values(" + v.c + ",'" + path + "','" + path + "-" + v.c + "',1)";
                shelper.ExecuteNonQuery(sql);
                return v.c.ToString();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }





        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>

        [HttpPost("DelENode")]
        public string DelENode(string nodeId)
        {
            try
            {
                var shelper = new SqlHepler();
                string sql = "delete from dbo.node_net where node_path='" + nodeId + "'";
                var nodes = nodeId.Split('-');
                shelper.ExecuteNonQuery(sql);
                sql = "delete from dbo.node_net where node_path='" + nodes.Last() + "'";
                shelper.ExecuteNonQuery(sql);
                return "sucess";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// 教育考试栏目
        /// </summary>
        /// <returns></returns>
        [HttpGet("ExamSiteNodes")]
        public string ExamSiteNodes()
        {
            try
            {
                string sql = @"select node_name name,node_path as id,node_pid as pId from site_node node 
                                                       inner join  node_net net on node.node_id = net.node_id";
                return SqlHepler.GetSqlDataBySql(sql).ToJson().RedisDataToJson();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
