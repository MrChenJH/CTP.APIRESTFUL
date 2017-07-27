using System;
using System.Collections.Generic;
using System.Text;
using CTP.Redis.Request.Menu;

namespace CTP.Redis.Request.Examination
{
    /// <summary>
    /// 菜单项
    /// </summary>
    public class Emenu : ExaminationCommon
    {

        /// <summary>
        /// 父编号
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Mname { get; set; }

        /// <summary>
        /// 是否分类
        /// </summary>
        public string IsCategory { get; set; }

        /// <summary>
        /// 存在子节点
        /// </summary>
        public string HasChild { get; set; }

        /// <summary>
        /// 顶部菜单
        /// </summary>
        public string TopMenu { get; set; }

        /// <summary>
        /// 菜单链接
        /// </summary>
        public string Mlink { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Micon { get; set; }
    }
}
