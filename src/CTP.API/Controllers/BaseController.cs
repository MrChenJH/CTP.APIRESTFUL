using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CTP.Redis;
using NLog;

namespace CTP.API.Controllers
{

    public class BaseController : Controller
    {

        /// <summary>
        /// 日记
        /// </summary>
        protected static Logger Logger = LogManager.GetCurrentClassLogger();
       
        /// <summary>
        /// Grid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_func"></param>
        public string GridInvork<T>(Func<RPage<T>> _func) where T : class
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            string result = ExceptionHook(() =>
            {
                return _func().ToJson(false);
            });
            return DataToJson(result);
        }


        /// <summary>
        /// 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_func"></param>
        public string ListInvork<T>(Func<RList<T>> _func) where T : class
        {
            string result = ExceptionHook(() =>
            {
                return _func().ToJson(false);
            });
            return DataToJson(result);
        }


        /// <summary>
        ///一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_func"></param>
        public string SingleInvork<T>(Func<RSingle<T>> _func) where T : class
        {
            string result = ExceptionHook(() =>
            {
                return _func().ToJson(false);
            });
            return DataToJson(result);
        }


        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="jsonData"></param>
        private string DataToJson(string jsonData)
        {
            var json = jsonData.Replace("[\"{", "[{").Replace("}\"]", "}]").Replace("\\\"", "\"").Replace("\"Result\":\"{", "\"Result\":{").Replace("}\"}", "}}");
            Response.ContentType = "application/json";
            return json;
        }

        /// <summary>
        /// 错误钩子
        /// </summary>
        /// <param name="_fun"></param>
        /// <returns></returns>
        private string ExceptionHook(Func<string> _fun)
        {
            try
            {
                return _fun();
            }

            catch (Exception ex)
            {

                return new RSingle<string> { code = ErrorCode.IllegalValueErrorCode, sucess = false }.ToJson(false);
            }
        }
    }
}
