using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    using Interfaces;
    using Role = AnyOfMiscs.RoleEnum;
    public class ORoleStore : IRoleStore
    {
        #region -- Interface APIMethods --
        /// <summary>
        /// 找到指定路径所需要的角色，若不存在这个路径，返回<see cref="Role.@default"/>的值
        /// </summary>
        /// <param name="path">虚拟路径</param>
        /// <returns><see cref="Role.@default"/>的值</returns>
        public Task<int> FindRoleByPath(string path)
        {
            var ounits = InMemoryStoragebox.ForNewOrExisted().UnitStoreRouterMapper;
            var pathkey = new PathString(path);
            if (!ounits.ContainsKey(pathkey)) return Task.FromResult((int)Role.@default);
            return Task.FromResult((int)ounits[pathkey].Role);
        }
        #endregion
    }
}
