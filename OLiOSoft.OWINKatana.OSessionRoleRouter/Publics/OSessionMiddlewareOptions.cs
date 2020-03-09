using OLiOSoft.OWINKatana.OSessionRoleRouter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    public class OSessionMiddlewareOptions
    {
        /// <summary>
        /// 带有osession的cookie的名称。默认情况下<see cref="MyOLiO"/> 
        /// </summary>
        public string CookieName { get; set; } = OSessionMiddlewareDefaults.CookieName;
        /// <summary>
        /// 带有osession的cookie的域。默认情况下<see cref=""/> 
        /// </summary>
        public string CookieDomain { get; set; } = OSessionMiddlewareDefaults.CookieDomain;
        /// <summary>
        /// 带有osession的cookie的路径。默认情况下<see cref="/"/> 
        /// </summary>
        public string CookiePath { get; set; } = OSessionMiddlewareDefaults.CookiePath;
        /// <summary>
        /// 带有osession的cookie的生命。默认情况下null。
        /// </summary>
        public TimeSpan? CookieLife { get; set; } = OSessionMiddlewareDefaults.CookieLife;
        /// <summary>
        /// 安全cookie仅由浏览器通过HTTPS发送。默认情况下关闭此设置。
        /// </summary>
        public bool UseSecureCookie { get; set; } = OSessionMiddlewareDefaults.CookieSecure;
        /// <summary>
        /// osession存储
        /// </summary>
        public ISessionStore Store { get; set; } = new OSessionStore();
        /// <summary>
        /// 每当需要生成新的session id时，都会调用<see cref="Func{String}"/> 
        /// 基于唯一的默认唯一session id生成器<see cref="Guid"/>结合<see cref="RNGCryptoServiceProvider"/>随机产生的部分
        /// </summary>
        public Func<string> UniqueSessionIdGenerator { get; set; } = OSessionMiddlewareDefaults.UniqueSessionIdGenerator;
    }
}
