using Microsoft.Owin;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    using Interfaces;
    using Role = AnyOfMiscs.RoleEnum;

    public class ORoleContext : IRoleContext
    {
        #region -- Private Data --
        private string m_Name = string.Empty;
        private Role m_Role = Role.@default;
        private bool m_NeedLogin = false;
        private bool m_NeedPermission = false;
        private IOwinContext m_HttpContext = null;
        #endregion

        #region -- New --
        private ORoleContext(IOwinContext httpContext)
            => m_HttpContext = httpContext;
        private ORoleContext(string name, Role role, bool needlogin, bool needpermission, IOwinContext httpContext)
        {
            m_Name = name;
            m_Role = role;
            m_NeedLogin = needlogin;
            m_NeedPermission = needpermission;
            m_HttpContext = httpContext;
        }
        static internal ORoleContext ForNew(IOwinContext httpContext)
            => new ORoleContext(httpContext);
        static internal ORoleContext ForNew(string name, Role role, bool needlogin, bool needpermission, IOwinContext httpContext)
            => new ORoleContext(name, role, needlogin, needpermission, httpContext);
        #endregion

        #region -- Interface ShotC --
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get => m_Name;
            set => m_Name = value;
        }
        /// <summary>
        /// 角色
        /// </summary>
        public Role Role
        {
            get => m_Role;
            set => m_Role = value;
        }
        /// <summary>
        /// 是否被禁止
        /// </summary>
        public bool IsForbidden
            => Role == Role.forbidden;
        /// <summary>
        /// 是否被授权
        /// </summary>
        public bool IsAuthorized
            => !(NeedLogin || NeedPermission);
        /// <summary>
        /// 需要登录
        /// </summary>
        public bool NeedLogin
        {
            get => m_NeedLogin;
            internal set => m_NeedLogin = value;
        }
        /// <summary>
        /// 需要允许
        /// </summary>
        public bool NeedPermission
        {
            get => m_NeedPermission;
            internal set => m_NeedPermission = value;
        }
        /// <summary>
        /// 管道上下文
        /// </summary>
        public IOwinContext HttpContext
        {
            get => m_HttpContext;
            internal set => m_HttpContext = value;
        }
        #endregion
    }
}
