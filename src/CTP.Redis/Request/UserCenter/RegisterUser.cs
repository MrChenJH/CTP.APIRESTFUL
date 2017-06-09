using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.Redis.Request.UserCenter
{
    /// <summary>
    /// 用户注册
    /// </summary>
    public class RegisterUser : Common
    {

        /// <summary>
        /// 注册编号
        /// </summary>
        [DefaultValue("")]
        public string UserId { get; set; }


        /// <summary>
        /// 昵称
        /// </summary>
        [DefaultValue("")]
        public string UserName { get; set; }

        /// <summary>
        /// 注册密码
        /// </summary>
        [DefaultValue("")]
        public string UserPwd { get; set; }

        /// <summary>
        /// 绑定手机号
        /// </summary>
        [DefaultValue("")]
        public string UserPhone { get; set; }


        /// <summary>
        /// 绑定邮箱
        /// </summary>
        [DefaultValue("")]
        public string UserEmail { get; set; }


        /// <summary>
        /// 用户状态
        /// </summary>
        [DefaultValue(0)]
        public int UserStatus { get; set; }


        /// <summary>
        /// 用户角色
        /// </summary>
        [DefaultValue(0)]
        public int UserRole { get; set; }


        /// <summary>
        /// 用户授权
        /// </summary>
        [DefaultValue(0)]
        public int UserAuth { get; set; }


        /// <summary>
        /// 用户头像
        /// </summary>
        [DefaultValue("")]
        public string UserImg { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        [DefaultValue(0)]
        public int UserSex { get; set; }


        /// <summary>
        /// 出生年月
        /// </summary>
        [DefaultValue("")]
        public string UserBrithday { get; set; }


        /// <summary>
        /// 省份
        /// </summary>
        [DefaultValue("")]
        public string UserProvince { get; set; }


        /// <summary>
        /// 城市
        /// </summary>
        [DefaultValue("")]
        public string UserCity { get; set; }

    }
}
