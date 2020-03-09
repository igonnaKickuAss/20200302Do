using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    using Role = AnyOfMiscs.RoleEnum;
    public class ORoleMiddleware : OwinMiddleware
    {
        #region -- ReadOnly Data --
        readonly private ORoleMiddlewareOptions m_Options = null;
        #endregion

        #region -- New --
        /// <summary>
        /// <see cref="ORoleMiddleware"/>
        /// </summary>
        /// <param name="next">IAppBuilder中的下一个中间件</param>
        /// <param name="options">指定给中间件一个参数</param>
        public ORoleMiddleware(OwinMiddleware next, ORoleMiddlewareOptions options) : base(next)
            => m_Options = options ?? throw new ArgumentNullException(nameof(options));
        #endregion

        #region -- Override APIMethods --
        public override async Task Invoke(IOwinContext context)
        {
            var path = context.Request.Path.Value;
            var orole = ORoleContext.ForNew(context);
            context = context = context.Set(Constants.ORoleContextOwinEnvironmentKey, orole);
            m_Options.FillORoleContextAsYouLike.Invoke(orole);
            if (orole.Role == Role.@default) orole.NeedLogin = true;
            else if ((int)orole.Role > (await m_Options.Store.FindRoleByPath(path).ConfigureAwait(false))) orole.NeedPermission = true;
            else
            {
                orole.NeedLogin = false;
                orole.NeedPermission = false;
            }
            await Next.Invoke(context);
            orole.HttpContext = null;
        }
        #endregion
    }
}
