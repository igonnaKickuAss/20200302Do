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
    static public class ORoleMiddlewareAppBuilderExtensions
    {
        /// <summary>
        /// 使用ORole中间件.
        /// </summary>
        /// <param name="appBuilder"><see cref="IAppBuilder"/></param>
        /// <param name="options">ORoleMiddlewareOptions</param>
        /// <returns><see cref="IAppBuilder"/></returns>
        static public IAppBuilder UseORoleMiddleware(this IAppBuilder appBuilder, ORoleMiddlewareOptions options = null)
            => appBuilder.Use<ORoleMiddleware>(options ?? new ORoleMiddlewareOptions());
    }
}
