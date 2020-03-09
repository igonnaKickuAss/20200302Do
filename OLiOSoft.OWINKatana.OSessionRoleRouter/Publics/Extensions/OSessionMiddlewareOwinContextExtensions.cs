using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin
{
    using OLiOSoft.OWINKatana.OSessionRoleRouter;

    static public class OSessionMiddlewareOwinContextExtensions
    {
        /// <summary>
        /// 从OWIN字典上下文中获取OSession上下文
        /// </summary>
        /// <param name="context"> <see cref="IOwinContext"/></param>
        /// <returns>如果没有可用的OSession上下文，则使用OSession上下文或null</returns>
        /// <remarks>如果没有可用的OSession上下文，请在调用此方法的中间件之前注册OSessionMiddleware</remarks>
        static public OSessionContext GetOSessionContext(this IOwinContext context)
            => context.Get<OSessionContext>(Constants.OSessionContextOwinEnvironmentKey);
    }
}
