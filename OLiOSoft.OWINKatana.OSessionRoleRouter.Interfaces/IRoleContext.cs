using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter.Interfaces
{
    using Role = AnyOfMiscs.RoleEnum;
    public interface IRoleContext
    {
        /// <summary>
        /// 管道上下文
        /// </summary>
        IOwinContext HttpContext { get; }
        /// <summary>
        /// 是否被授权
        /// </summary>
        bool IsAuthorized { get; }
        /// <summary>
        /// 是否被禁止
        /// </summary>
        bool IsForbidden { get; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 角色
        /// <see cref="AnyOfMiscs.RoleEnum.@default"/>
        /// <see cref="AnyOfMiscs.RoleEnum.admin"/>
        /// <see cref="AnyOfMiscs.RoleEnum.boss"/>
        /// <see cref="AnyOfMiscs.RoleEnum.manager"/>
        /// <see cref="AnyOfMiscs.RoleEnum.staff"/>
        /// <see cref="AnyOfMiscs.RoleEnum.customer"/>
        /// <see cref="AnyOfMiscs.RoleEnum.forbidden"/>
        /// </summary>
        Role Role { get; set; }
        /// <summary>
        /// 需要登录
        /// </summary>
        bool NeedLogin { get; }
        /// <summary>
        /// 需要允许
        /// </summary>
        bool NeedPermission { get; }
    }
}
