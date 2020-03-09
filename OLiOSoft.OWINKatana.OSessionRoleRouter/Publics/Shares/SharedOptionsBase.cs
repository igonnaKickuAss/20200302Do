using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    public abstract class SharedOptionsBase<T>
    {
        #region -- Private Data --
        private SharedOptions m_SharedOptions = null;
        #endregion

        #region -- New --
        protected SharedOptionsBase(SharedOptions shareOptions)
            => SharedOptions = shareOptions ?? throw new ArgumentNullException($"参数{nameof(SharedOptions)}的引用为空");
        #endregion

        #region -- Protected ShotC --
        protected SharedOptions SharedOptions { get => m_SharedOptions; private set => m_SharedOptions = value; }
        #endregion

        #region -- Protected ShotC --
        protected KeyValuePair<PathString, Action> NotFoundMapper { get => m_SharedOptions.NotFoundMapper; set => m_SharedOptions.NotFoundMapper = value; }
        protected KeyValuePair<PathString, Action> NoPermissionMapper { get => m_SharedOptions.NoPermissionMapper; set => m_SharedOptions.NoPermissionMapper = value; }
        protected KeyValuePair<PathString, Action> NotLoginMapper { get => m_SharedOptions.NotLoginMapper; set => m_SharedOptions.NotLoginMapper = value; }
        protected KeyValuePair<PathString, Action> ForbiddenMapper { get => m_SharedOptions.ForbiddenMapper; set => m_SharedOptions.ForbiddenMapper = value; }
        protected KeyValuePair<PathString, Action> IndexMapper { get => m_SharedOptions.IndexMapper; set => m_SharedOptions.IndexMapper = value; }
        #endregion

        #region -- Public ShotC --
        public string Filename { get => m_SharedOptions.Filename; set => m_SharedOptions.Filename = value; }
        #endregion
    }
}
