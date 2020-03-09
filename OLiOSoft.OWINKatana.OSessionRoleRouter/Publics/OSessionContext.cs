using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    using Interfaces;
    using Microsoft.Owin;

    public class OSessionContext : ISessionContext
    {
        #region -- ReadOnly Data --
        readonly private string m_OSessionId = string.Empty;
        readonly private IDictionary<string, object> m_Properties = null;
        readonly private bool m_IsNew = false;
        #endregion

        #region -- Private Data --
        private bool m_IsModified = false;
        private IOwinContext m_HttpContext = null;
        #endregion

        #region -- New --
        private OSessionContext(string osessionId, IDictionary<string, object> properties, bool isNew, IOwinContext httpContext)
        {
            if (string.IsNullOrEmpty(osessionId)) throw new ArgumentNullException(nameof(osessionId));
            m_Properties = properties ?? throw new ArgumentNullException(nameof(properties));
            m_OSessionId = osessionId;
            m_IsNew = isNew;
            m_HttpContext = httpContext;
        }
        static internal OSessionContext ForExistedProperties(string osessionId, IEnumerable<KeyValuePair<string, object>> properties, IOwinContext httpContext)
            => new OSessionContext(osessionId, properties?.ToDictionary(x => x.Key, x => x.Value), false, httpContext);
        static internal OSessionContext ForNew(string osessionId, IOwinContext httpContext)
            => new OSessionContext(osessionId, new Dictionary<string, object>(), true, httpContext);
        #endregion

        #region -- Internal ShotC --
        internal string OSessionId => m_OSessionId;
        internal bool IsNew => m_IsNew;
        internal bool IsEmpty => !m_Properties.Any();
        internal IEnumerable<KeyValuePair<string, object>> Properties => m_Properties.Select(x => x);
        internal bool IsModified
        {
            get => m_IsModified;
            set => m_IsModified = value;
        }
        #endregion

        #region -- Interfaces APIMethods --
        /// <summary>
        /// 管道上下文
        /// </summary>
        public IOwinContext HttpContext
        {
            get => m_HttpContext;
            internal set => m_HttpContext = value;
        }
        /// <summary>
        /// 为当前OSession增加或者更新Property
        /// </summary>
        /// <param name="key">property的键</param>
        /// <param name="value">property的值</param>
        /// <exception cref="ArgumentNullException">当键为null时抛出</exception>
        public void AddOrUpdate(string key, object value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            if (m_Properties.ContainsKey(key)) m_Properties[key] = value;
            else m_Properties.Add(key, value);
            if (!m_IsNew) m_IsModified = true;
        }
        /// <summary>
        /// 清理当前OSession.
        /// </summary>
        public void Clear()
        {
            if (!m_Properties.Any()) return;
            m_Properties.Clear();
            if (!m_IsNew) m_IsModified = true;
        }
        /// <summary>
        /// 为当前OSession删除一个Property
        /// </summary>
        /// <param name="key">property的键</param>
        /// <exception cref="ArgumentNullException">当键为null时抛出</exception>
        public void Delete(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            m_Properties.Remove(key);
            if (!m_IsNew) m_IsModified = true;
        }
        /// <summary>
        /// 拿到当前OSession的Property
        /// </summary>
        /// <param name="key">property的键</param>
        /// <returns>property的值，如果没有找到该属性，则为null</returns>
        /// <exception cref="ArgumentNullException">当键为null时抛出</exception>
        public object Get(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            if (!m_Properties.ContainsKey(key)) return null;
            return m_Properties[key];
        }
        /// <summary>
        /// 拿到当前OSession的Property
        /// </summary>
        /// <typeparam name="T">property的值的类型</typeparam>
        /// <param name="key">property的键</param>
        /// <returns>property的值，如果没有找到该property，则为default(T)。</returns>
        /// <exception cref="ArgumentNullException">当键为null时抛出</exception>
        public T Get<T>(string key) => (T)(Get(key) ?? default(T));
        #endregion
    }
}
