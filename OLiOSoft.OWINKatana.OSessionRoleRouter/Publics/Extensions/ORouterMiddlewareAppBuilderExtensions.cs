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
    static public class ORouterMiddlewareAppBuilderExtensions
    {
        /// <summary>
        /// 使用ORouter中间件.
        /// </summary>
        /// <param name="appBuilder"><see cref="IAppBuilder"/></param>
        /// <param name="options">ORouterMiddlewareOptions</param>
        /// <returns><see cref="IAppBuilder"/></returns>
        static public IAppBuilder UseORouterMiddleware(this IAppBuilder appBuilder, ORouterMiddlewareOptions options = null)
            => appBuilder.Use<ORouterMiddleware>(options ?? new ORouterMiddlewareOptions(string.Empty));
        /// <summary>
        /// 使用ORouter中间件.
        /// </summary>
        /// <param name="appBuilder"><see cref="IAppBuilder"/></param>
        /// <param name="fileName">应用方法所在的程序集</param>
        /// <returns></returns>
        static public IAppBuilder UseORouterMiddleware(this IAppBuilder appBuilder, string fileName)
            => appBuilder.UseORouterMiddleware(new ORouterMiddlewareOptions(fileName));
    }
}
