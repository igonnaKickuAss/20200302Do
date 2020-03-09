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
    static public class OTransitServerOwinContextExtensions
    {
        /// <summary>
        /// 使用OSession，ORole，ORouter中间件.
        /// </summary>
        /// <param name="appBuilder"><see cref="IAppBuilder"/></param>
        /// <param name="options">OTransitServerOptions</param>
        /// <returns><see cref="IAppBuilder"/></returns>
        static public IAppBuilder UseTransitServer(this IAppBuilder appBuilder, OTransitServerOptions options)
        {
            if (options.EnableOSessionMiddleware) appBuilder.Use<OSessionMiddleware>(options.OSessionMiddlewareOptions);
            if (options.EnableORoleMiddleware) appBuilder.Use<ORoleMiddleware>(options.ORoleMiddlewareOptions);
            if (options.EnableORouterMiddleware) appBuilder.Use<ORouterMiddleware>(options.ORouterMiddlewareOptions);
            return appBuilder;
        }

        /// <summary>
        /// 使用OSession，ORole，ORouter中间件.
        /// </summary>
        /// <param name="appBuilder"><see cref="IAppBuilder"/></param>
        /// <param name="fileName">应用方法所在的程序集全名</param>
        /// <param name="options">OTransitServerOptions</param>
        /// <returns></returns>
        static public IAppBuilder UseTransitServer(this IAppBuilder appBuilder, string fileName = null, OTransitServerOptions options = null)
            => appBuilder.UseTransitServer(options ?? new OTransitServerOptions(fileName));
    }
}
