using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    using Interfaces;
    using AppAction = Action<Interfaces.IRouterContext>;
    public class ORouterContext : IRouterContext
    {
        #region -- Private Data --
        private bool m_Redirect = false;
        private bool m_Send = false;
        private string m_RequestPath = string.Empty;
        private string m_JSONMessage = string.Empty;
        private AppAction m_Handle = null;
        private IOwinContext m_HttpContext = null;
        #endregion

        #region -- New --
        private ORouterContext(IOwinContext httpContext)
            => m_HttpContext = httpContext;
        static internal ORouterContext ForNew(IOwinContext httpContext)
            => new ORouterContext(httpContext);
        #endregion

        #region -- Internal ShotC --
        internal AppAction Handle
        {
            get => m_Handle;
            set => m_Handle = value;
        }
        #endregion

        #region -- Interface APIMethods --
        /// <summary>
        /// 指示是否重定向
        /// </summary>
        public bool Redirect => m_Redirect;
        /// <summary>
        /// 指示是否发送JSON数据包
        /// </summary>
        public bool Send => m_Send;
        /// <summary>
        /// 重定向地址，一旦修改<see cref="Redirect"/>为真，<see cref="Send"/>为假
        /// </summary>
        public string RequestPath
        {
            get => m_RequestPath;
            set
            {
                m_Redirect = true;
                m_Send = false;
                m_RequestPath = value;
            }
        }
        /// <summary>
        /// JSON数据包，一旦修改<see cref="Send"/>为真，<see cref="Redirect"/>为假
        /// </summary>
        public string JSONMessage
        {
            get => m_JSONMessage;
            set
            {
                m_Redirect = false;
                m_Send = true;
                m_JSONMessage = value;
            }
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
