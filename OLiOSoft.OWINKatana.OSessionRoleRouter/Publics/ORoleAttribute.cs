using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    using Role = AnyOfMiscs.RoleEnum;
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed public class ORoleAttribute : Attribute
    {
        #region -- Private Data --
        readonly private Role m_Role = Role.@default;
        #endregion

        #region -- New --
        /// <summary>
        /// 指定一个角色，我会取最大的那个角色
        /// </summary>
        /// <param name="role">角色</param>
        public ORoleAttribute(Role role)
            => m_Role = role;
        #endregion

        #region -- Public ShotC --
        public Role Role => this.m_Role;
        #endregion
    }
}
