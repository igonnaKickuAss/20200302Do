using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin
{
    using OLiOSoft.OWINKatana.OSessionRoleRouter;

    static public class ORouterMiddlewareOwinContextExtensions
    {
        /// <summary>
        /// 从OWIN字典上下文中获取ORouter上下文
        /// </summary>
        /// <param name="context"> <see cref="IOwinContext"/></param>
        /// <returns>如果没有可用的ORouter上下文，则使用ORouter上下文或null</returns>
        /// <remarks>如果没有可用的ORouter上下文，请在调用此方法的中间件之前注册ORouterMiddleware</remarks>
        static public ORouterContext GetORouterContext(this IOwinContext context)
            => context.Get<ORouterContext>(Constants.ORouterContextOwinEnvironmentKey);
    }
}
