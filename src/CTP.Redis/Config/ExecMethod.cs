using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTP.Redis.Factory;

namespace CTP.Redis.Const
{
    public enum ExecMethod
    {
        /// <summary>
        /// 特殊查询
        /// </summary>
        Specialquery,

        /// <summary>
        /// 查询
        /// </summary>
        Query,

        /// <summary>
        /// 翻页查询
        /// </summary>
        PageQuery,

        /// <summary>
        /// 新增或修改
        /// </summary>
        AddOrUpdate,

        /// <summary>
        /// 删除
        /// </summary>
        Delete,
    }
}
