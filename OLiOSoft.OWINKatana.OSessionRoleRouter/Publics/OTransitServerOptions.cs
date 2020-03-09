using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    public class OTransitServerOptions
    {
        #region -- Private Data --
        private OSessionMiddlewareOptions m_OSessionMiddlewareOptions = null;
        private ORoleMiddlewareOptions m_ORoleMiddlewareOptions = null;
        private ORouterMiddlewareOptions m_ORouterMiddlewareOptions = null;
        private bool m_EnableOSessionMiddleware = true;
        private bool m_EnableORoleMiddleware = true;
        private bool m_EnableORouterMiddleware = true;
        private string m_FileName = string.Empty;
        #endregion

        #region -- New --
        public OTransitServerOptions(string fileName = null)
            => m_FileName = fileName;
        #endregion

        #region -- Public ShotC --
        public bool EnableOSessionMiddleware
        {
            get => m_EnableOSessionMiddleware;
            set
            {
                if (value) m_OSessionMiddlewareOptions = m_OSessionMiddlewareOptions ?? new OSessionMiddlewareOptions();
                m_EnableOSessionMiddleware = value;
            }
        }
        public bool EnableORoleMiddleware
        {
            get => m_EnableORoleMiddleware;
            set
            {
                if (value) m_ORoleMiddlewareOptions = m_ORoleMiddlewareOptions ?? new ORoleMiddlewareOptions();
                m_EnableORoleMiddleware = value;
            }
        }
        public bool EnableORouterMiddleware
        {
            get => m_EnableORouterMiddleware;
            set
            {
                if (string.IsNullOrEmpty(m_FileName)) throw new ArgumentNullException($"{nameof(m_FileName)}如果你要使用路由，程序集的地址就不能为空！");
                if (value) m_ORouterMiddlewareOptions = m_ORouterMiddlewareOptions ?? new ORouterMiddlewareOptions(m_FileName);
                m_EnableORouterMiddleware = value;
            }
        }

        public OSessionMiddlewareOptions OSessionMiddlewareOptions
        {
            get
            {
                if (!m_EnableOSessionMiddleware) return null;
                this.EnableOSessionMiddleware = true;
                return m_OSessionMiddlewareOptions;
            }
        }
        public ORoleMiddlewareOptions ORoleMiddlewareOptions
        {
            get
            {
                if (!m_EnableORoleMiddleware) return null;
                this.EnableORoleMiddleware = true;
                return m_ORoleMiddlewareOptions;
            }
        }
        public ORouterMiddlewareOptions ORouterMiddlewareOptions
        {
            get
            {
                if (!m_EnableORouterMiddleware) return null;
                this.EnableORouterMiddleware = true;
                return m_ORouterMiddlewareOptions;
            }
        }

        #endregion
    }
}
