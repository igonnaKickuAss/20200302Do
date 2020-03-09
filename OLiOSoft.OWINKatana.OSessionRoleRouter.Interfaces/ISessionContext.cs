using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter.Interfaces
{
    public interface ISessionContext
    {
        /// <summary>
        /// 管道上下文
        /// </summary>
        IOwinContext HttpContext { get; }
        /// <summary>
        /// 拿到当前OSession的Property
        /// </summary>
        /// <param name="key">property的键</param>
        /// <returns>property的值，如果没有找到该属性，则为null</returns>
        /// <exception cref="ArgumentNullException">当键为null时抛出</exception>
        object Get(string key);
        /// <summary>
        /// 拿到当前OSession的Property
        /// </summary>
        /// <typeparam name="T">property的值的类型</typeparam>
        /// <param name="key">property的键</param>
        /// <returns>property的值，如果没有找到该property，则为default(T)。</returns>
        /// <exception cref="ArgumentNullException">当键为null时抛出</exception>
        T Get<T>(string key);
        /// <summary>
        /// 为当前OSession增加或者更新Property
        /// </summary>
        /// <param name="key">property的键</param>
        /// <param name="value">property的值</param>
        /// <exception cref="ArgumentNullException">当键为null时抛出</exception>
        void AddOrUpdate(string key, object value);
        /// <summary>
        /// 为当前OSession删除一个Property
        /// </summary>
        /// <param name="key">property的键</param>
        /// <exception cref="ArgumentNullException">当键为null时抛出</exception>
        void Delete(string key);
        /// <summary>
        /// 清理当前OSession.
        /// </summary>
        void Clear();
    }
}
