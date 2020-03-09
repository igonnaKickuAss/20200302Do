using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin
{
    using OLiOSoft.OWINKatana.OSessionRoleRouter;
    /// <summary>
    /// 该方法集成在<see cref="IAppBuilder"/>内
    /// </summary>
    static public class OSessionMiddlewareAppBuilderExtensions
    {
        /// <summary>
        /// 使用OSession中间件.
        /// </summary>
        /// <param name="appBuilder"><see cref="IAppBuilder"/></param>
        /// <param name="options">OSessionMiddlewareOptions</param>
        /// <returns><see cref="IAppBuilder"/></returns>
        static public IAppBuilder UseOSessionMiddleware(this IAppBuilder appBuilder, OSessionMiddlewareOptions options = null)
            => appBuilder.Use<OSessionMiddleware>(options ?? new OSessionMiddlewareOptions());
    }
}
