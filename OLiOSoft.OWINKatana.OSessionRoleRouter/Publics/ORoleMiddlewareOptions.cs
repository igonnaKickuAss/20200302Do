using Microsoft.Owin;
using System;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    using AppAction = Action<Interfaces.IRoleContext>;
    public class ORoleMiddlewareOptions : SharedOptionsBase<ORoleMiddlewareOptions>
    {
        #region -- Private Data --
        private AppAction m_Action = null;
        private ORoleStore m_Store = null;
        #endregion

        #region -- New --
        public ORoleMiddlewareOptions() : base(new SharedOptions())
        {
            m_Action = ORoleMiddlewareDefaults.FillORoleContextAsYouLike;
            m_Store = ORoleMiddlewareDefaults.Store;
        }
        #endregion

        #region -- Public ShotC --
        /// <summary>
        /// orole存储，记录一些东西
        /// </summary>
        public ORoleStore Store
        {
            get => m_Store;
            set => m_Store = value;
        }
        public AppAction FillORoleContextAsYouLike
        {
            get => m_Action;
            set => m_Action = value;
        }
        #endregion
    }
}
