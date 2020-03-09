using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    public class SharedOptions
    {
        #region -- Private Data --
        private KeyValuePair<PathString, Action> m_NotFoundMapper = default(KeyValuePair<PathString, Action>);
        private KeyValuePair<PathString, Action> m_NoPermissionMapper = default(KeyValuePair<PathString, Action>);
        private KeyValuePair<PathString, Action> m_NotLoginMapper = default(KeyValuePair<PathString, Action>);
        private KeyValuePair<PathString, Action> m_ForbiddenMapper = default(KeyValuePair<PathString, Action>);
        private KeyValuePair<PathString, Action> m_IndexMapper = default(KeyValuePair<PathString, Action>);
        private string m_Filename = string.Empty;
        #endregion

        #region -- New --
        public SharedOptions()
        {

        }
        #endregion

        #region -- Public ShotC --
        public string Filename { get => m_Filename; internal set => m_Filename = value; }
        public KeyValuePair<PathString, Action> NotFoundMapper { get => m_NotFoundMapper; internal set => m_NotFoundMapper = value; }
        public KeyValuePair<PathString, Action> NoPermissionMapper { get => m_NoPermissionMapper; internal set => m_NoPermissionMapper = value; }
        public KeyValuePair<PathString, Action> NotLoginMapper { get => m_NotLoginMapper; internal set => m_NotLoginMapper = value; }
        public KeyValuePair<PathString, Action> ForbiddenMapper { get => m_ForbiddenMapper; internal set => m_ForbiddenMapper = value; }
        public KeyValuePair<PathString, Action> IndexMapper { get => m_IndexMapper; internal set => m_IndexMapper = value; }

        #endregion
    }
}
