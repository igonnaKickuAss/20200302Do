using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    using Interfaces;
    using Properites = IEnumerable<KeyValuePair<string, object>>;
    public class OSessionStore : ISessionStore
    {
        #region -- Interfaces APIMethods --
        /// <summary>
        /// 新增一个键值对到存储中
        /// </summary>
        /// <param name="osessionId">键</param>
        /// <param name="properties">值</param>
        /// <returns>Task</returns>
        public Task Add(string osessionId, Properites properties)
        {
            InMemoryStoragebox.ForNewOrExisted().OSessionStoreMapper.Add(osessionId, properties.ToList());
            return Task.FromResult(0);
        }
        /// <summary>
        /// 删除一个键值对通过键
        /// </summary>
        /// <param name="osessionId">键</param>
        /// <returns>Task</returns>
        public Task Delete(string osessionId)
        {
            var osessionMapper = InMemoryStoragebox.ForNewOrExisted().OSessionStoreMapper;
            if (!osessionMapper.ContainsKey(osessionId)) return Task.FromResult(0);
            osessionMapper.Remove(osessionId);
            return Task.FromResult(0);
        }
        /// <summary>
        /// 找到值通过键
        /// </summary>
        /// <param name="osessionId">键</param>
        /// <returns><see cref="Properites"/></returns>
        public Task<Properites> FindById(string osessionId)
        {
            var osessionMapper = InMemoryStoragebox.ForNewOrExisted().OSessionStoreMapper;
            var properties = osessionMapper.ContainsKey(osessionId) ? osessionMapper[osessionId] : null;
            return Task.FromResult(properties);
        }
        /// <summary>
        /// 更新键值对，喂一个键，喂一个值
        /// </summary>
        /// <param name="osessionId">键</param>
        /// <param name="properties">值</param>
        /// <returns>Task</returns>
        public Task Update(string osessionId, Properites properties)
        {
            var osessionMapper = InMemoryStoragebox.ForNewOrExisted().OSessionStoreMapper;
            if (!osessionMapper.ContainsKey(osessionId)) throw new KeyNotFoundException(osessionId);
            osessionMapper[osessionId] = properties.ToList();
            return Task.FromResult(0);
        }
        #endregion
    }
}
