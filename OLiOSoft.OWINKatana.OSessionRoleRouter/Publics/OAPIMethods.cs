using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    public abstract class OAPIMethodsBase : IAPIMethods
    {
        #region -- Private Data --
        private IOwinContext m_HttpContext = null;
        #endregion

        #region -- Protected ShotC --
        protected IOwinContext HttpContext => m_HttpContext;
        #endregion

        #region -- Internal ShotC --
        internal IOwinContext SetHttpContext { set => m_HttpContext = value; }
        #endregion
    }
}
