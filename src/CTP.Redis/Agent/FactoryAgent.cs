using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CTP.Redis.Const;
using CTP.Redis.Response;

namespace CTP.Redis.Agent
{
    public class FactoryAgent
    {
        public FactoryAgent()
        {

        }

        public FactoryAgent(string factoryName, RequsetBase request, string method)
        {
            this.FactoryName = factoryName;
            this.Request = request;
            this.Method = method;
        }

        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 处理参数
        /// </summary>
        public RequsetBase Request { get; set; }



        /// <summary>
        /// 后台返回参数
        /// </summary>
        public Result Result { get; set; }

        /// <summary>
        /// 调用后台工厂
        /// </summary>
        public void InvokeFactory()
        {
            try
            {

                Assembly assembly = Assembly.Load(new AssemblyName("CTP.Redis"));
                Type type = assembly.GetType(FactoryName);
                object instance = assembly.CreateInstance(FactoryName);
                Type[] params_type = new Type[1];
                params_type[0] = Request.GetType();
                Object[] params_obj = new Object[1];
                params_obj[0] = Request;
                Result = (Result)type.GetMethod(Method, params_type).Invoke(instance, params_obj);
            }
            catch (Exception ex)
            {
                Result = new Result()
                {
                    sucess = false,
                    code = ErrorCode.NotExistKeyErrorCode,
                    msg = ex.Message
                };
            }
        }
    }
}
