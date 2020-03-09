using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    internal static class Constants
    {
        /// <summary>
        /// OWIN字典上下文中，存储OSession上下文的键
        /// </summary>
        public const string OSessionContextOwinEnvironmentKey = "OLiOSoft.OWINKatana.OSessionContext";
        /// <summary>
        /// OWIN字典上下文中，存储ORole上下文的键
        /// </summary>
        public const string ORoleContextOwinEnvironmentKey = "OLiOSoft.OWINKatana.ORoleContext";
        /// <summary>
        /// OWIN字典上下文中，存储ORouter上下文的键
        /// </summary>
        public const string ORouterContextOwinEnvironmentKey = "OLiOSoft.OWINKatana.ORouterContext";
    }
}
