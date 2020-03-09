using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    using Role = AnyOfMiscs.RoleEnum;
    public class ORouterMiddleware : OwinMiddleware
    {
        #region -- ReadOnly Data --
        readonly private ORouterMiddlewareOptions m_Options = null;
        readonly private string m_DefaultName = "游客";
        readonly private Role m_DefaultRole = Role.@default;
        #endregion

        #region -- New --
        /// <summary>
        /// <see cref="ORouterMiddleware"/>
        /// </summary>
        /// <param name="next">IAppBuilder中的下一个中间件</param>
        /// <param name="options">指定给中间件一个参数</param>
        public ORouterMiddleware(OwinMiddleware next, ORouterMiddlewareOptions options) : base(next)
            => m_Options = options ?? throw new ArgumentNullException(nameof(options));
        #endregion

        #region -- Override APIMethods --
        public override async Task Invoke(IOwinContext context)
        {
            var path = string.Empty;
            var orole = context.Get<ORoleContext>(Constants.ORoleContextOwinEnvironmentKey) ??
                ORoleContext.ForNew(m_DefaultName, m_DefaultRole, false, false, context);
            var orouter = ORouterContext.ForNew(context);
            context = context.Set(Constants.ORouterContextOwinEnvironmentKey, orouter);
            { path = context.Request.Path.Value; orouter.Handle = m_Options.NotFound; }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            var ounittypeofinstance = await m_Options.Store.FindOUnitTypeofInstanceByPath(path).ConfigureAwait(false);
            var ounit = ounittypeofinstance.Item1;
            var typeofinstance = (OAPIMethodsBase)ounittypeofinstance.Item2;
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if(ounit.Role== Role.forbidden)
            {
                if (!ounit.Final) await Next.Invoke(context);
                await m_Options.Store.Recycle(ounit, typeofinstance).ConfigureAwait(false);
                orouter.Handle = null;
                await Task.FromResult(0);
                return;
            }
            if (ounit.Role == Role.@default)
            {
                //TODO..方法本身角色就是Role.@default
                typeofinstance.SetHttpContext = context;
                ounit.Method.Invoke(typeofinstance, null);
                if (!ounit.Final) await Next.Invoke(context);
                await m_Options.Store.Recycle(ounit, typeofinstance).ConfigureAwait(false);
                orouter.Handle = null;
                orouter.HttpContext = null;
                await Task.FromResult(0);
                return;
            }
            if (orole.IsForbidden) { path = DefaultMethods.fobidden; orouter.Handle = m_Options.Forbidden; }
            else if (orole.IsAuthorized)
            {
                typeofinstance.SetHttpContext = context;
                ounit.Method.Invoke(typeofinstance, null);
                if (!ounit.Final) await Next.Invoke(context);
                await m_Options.Store.Recycle(ounit, typeofinstance).ConfigureAwait(false);
                orouter.Handle = null;
                orouter.HttpContext = null;
                await Task.FromResult(0);
                return;
            }
            else if (orole.NeedLogin) { path = DefaultMethods.notlogin; orouter.Handle = m_Options.NotLogin; }
            else if (orole.NeedPermission) { path = DefaultMethods.nopermission; orouter.Handle = m_Options.NoPermission; }
            else { path = DefaultMethods.index; orouter.Handle = m_Options.Index; }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ounittypeofinstance = await m_Options.Store.FindOUnitTypeofInstanceByPath(path).ConfigureAwait(false);
            ounit = ounittypeofinstance.Item1;
            typeofinstance = (OAPIMethodsBase)ounittypeofinstance.Item2;
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            typeofinstance.SetHttpContext = context;
            ounit.Method.Invoke(typeofinstance, null);
            if (!ounit.Final) await Next.Invoke(context);
            await m_Options.Store.Recycle(ounit, typeofinstance).ConfigureAwait(false);
            orouter.Handle = null;
            orouter.HttpContext = null;
            await Task.FromResult(0);
            return;
        }
        #endregion
    }
}
