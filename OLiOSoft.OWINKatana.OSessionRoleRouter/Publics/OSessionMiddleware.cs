using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    public class OSessionMiddleware : OwinMiddleware
    {
        #region -- ReadOnly Data --
        readonly private OSessionMiddlewareOptions m_Options = null;
        #endregion

        #region -- New --
        /// <summary>
        /// <see cref="OSessionMiddleware"/>
        /// </summary>
        /// <param name="next">IAppBuilder中的下一个中间件</param>
        /// <param name="options">指定给中间件一个参数</param>
        public OSessionMiddleware(OwinMiddleware next, OSessionMiddlewareOptions options) : base(next)
            => m_Options = options ?? throw new ArgumentNullException(nameof(options));
        #endregion

        #region -- Override APIMethods --
        public override async Task Invoke(IOwinContext context)
        {
            var osessions = await GetOrCreateOSessionContext(context.Request, context.Response);
            context = context.Set(Constants.OSessionContextOwinEnvironmentKey, osessions);
            //TODO.. might be handle with orolemiddleware
            await Next.Invoke(context);
            //TODO.. 管道的最后检查更新
            await UpdateOSessionStore(osessions);
            osessions.HttpContext = null;
        }
        #endregion

        #region -- Private APIMethods --
        /// <summary>
        /// 读取request的cookie里的osession id
        /// </summary>
        /// <param name="request">当前request.</param>
        /// <returns>String</returns>
        private string ReadOSessionIdFromRequest(IOwinRequest request) => request.Cookies[m_Options.CookieName];
        /// <summary>
        /// 将osession id写入response的cookie里
        /// </summary>
        /// <param name="response">当前response.</param>
        /// <param name="osessionId">osession id.</param>
        private void WriteOSessionIdToResponse(IOwinResponse response, string osessionId)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = m_Options.UseSecureCookie,
                Domain = m_Options.CookieDomain,
                Expires = m_Options.CookieLife.HasValue ? DateTime.UtcNow.Add(m_Options.CookieLife.Value) : (DateTime?)null,
                Path = m_Options.CookiePath
            };
            response.Cookies.Append(m_Options.CookieName, osessionId, cookieOptions);
        }
        /// <summary>
        /// 拿到或者为当前request创建一个<see cref="OSessionContext"/>
        /// 如果介个osession似需要被创建的，那就把他直接写入response的cookie里
        /// </summary>
        /// <param name="request">当前request.</param>
        /// <param name="response">当前response.</param>
        /// <returns>已存在的或者似新创建的OSession上下文</returns>
        private async Task<OSessionContext> GetOrCreateOSessionContext(IOwinRequest request, IOwinResponse response)
        {
            var osessionId = ReadOSessionIdFromRequest(request);
            if (osessionId != null)
            {
                var properties = await m_Options.Store.FindById(osessionId).ConfigureAwait(false)
                    ?? Enumerable.Empty<KeyValuePair<string, object>>();
                return OSessionContext.ForExistedProperties(osessionId, properties, request.Context);
            }
            osessionId = m_Options.UniqueSessionIdGenerator();
            var _sc = OSessionContext.ForNew(osessionId, request.Context);
            _sc.AddOrUpdate(
                "cookie", new { Name = m_Options.CookieName, Value = osessionId, Path = m_Options.CookiePath }
                );
            WriteOSessionIdToResponse(response, osessionId);
            return _sc;
        }
        /// <summary>
        /// 将<see cref="OSessionContext"/>更新
        /// </summary>
        /// <param name="osessions">要在存储区中更新的OSession上下文</param>
        /// <returns>异步执行<see cref="Task"/></returns>
        private async Task UpdateOSessionStore(OSessionContext osessions)
        {
            if (osessions.IsNew && !osessions.IsEmpty) await m_Options.Store.Add(osessions.OSessionId, osessions.Properties).ConfigureAwait(false);
            if (osessions.IsModified)
            {
                if (!osessions.IsEmpty) await m_Options.Store.Update(osessions.OSessionId, osessions.Properties).ConfigureAwait(false);
                else await m_Options.Store.Delete(osessions.OSessionId).ConfigureAwait(false);
            }
        }
        #endregion
    }
}
