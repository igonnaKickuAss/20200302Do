using System;
using System.Security.Cryptography;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    public class OSessionMiddlewareDefaults
    {
        /// <summary>
        /// 默认cookie名称.
        /// </summary>
        public const string CookieName = "MyOLiO";
        /// <summary>
        /// 默认cookie路径.
        /// </summary>
        public const string CookiePath = "/";
        /// <summary>
        /// 默认cookie域.
        /// </summary>
        public const string CookieDomain = "";
        /// <summary>
        /// 默认cookie生命.
        /// </summary>
        static public TimeSpan? CookieLife = new TimeSpan(0, 1, 20);
        /// <summary>
        /// 默认cookie安全策略
        /// </summary>
        public const bool CookieSecure = false;
        /// <summary>
        /// 基于唯一的默认唯一session id生成器<see cref="Guid"/>结合<see cref="RNGCryptoServiceProvider"/>随机产生
        /// </summary>
        /// <returns>一个特别的session id，最终字符串的最大长度为45个字符</returns>
        static public string UniqueSessionIdGenerator()
        {
            byte[] random = new byte[8];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider()) rng.GetBytes(random);
            return $"{Guid.NewGuid():N}.{Convert.ToBase64String(random)}";
        }
    }
}
