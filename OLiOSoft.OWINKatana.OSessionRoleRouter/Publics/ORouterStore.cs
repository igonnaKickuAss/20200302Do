using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using System.Reflection;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    using Interfaces;
    using OUnit = AnyOfMiscs.Unit;

    public class ORouterStore : IRouterStore
    {
        #region -- Interface APIMethods --
        /// <summary>
        /// 初始化路由（出现重复路由地址会抛异常）
        /// </summary>
        /// <param name="ounit">方法单元</param>
        /// <returns>Task</returns>
        public Task AddOrThrow(OUnit ounit)
        {
            var ounits = InMemoryStoragebox.ForNewOrExisted().UnitStoreRouterMapper;
            if (ounits.ContainsKey(ounit.RequestPath)) throw new Exception($"存在相同的路径！");
            ounits[ounit.RequestPath] = ounit;
            return Task.FromResult(0);
        }
        /// <summary>
        /// 指定路径找到方法单元和方法所在类型的实例
        /// </summary>
        /// <param name="path">虚拟路径</param>
        /// <returns>二元组</returns>
        public Task<(OUnit, object)> FindOUnitTypeofInstanceByPath(string path)
        {
            var ounits = InMemoryStoragebox.ForNewOrExisted().UnitStoreRouterMapper;
            var oapimethods = InMemoryStoragebox.ForNewOrExisted().OAPIMethodsStoreTypeMapper;
            var pathkey = new PathString(path);
            if (!ounits.ContainsKey(pathkey))
            {
                var _ounit = ounits[new PathString(DefaultMethods.notfound)];
                var _instance = oapimethods.ContainsKey(_ounit.TypeofInstance) && oapimethods[_ounit.TypeofInstance].Count > 0 ?
                    oapimethods[_ounit.TypeofInstance].Pop() : 
                    _ounit.TypeofInstance.GetMethod("ForNew", BindingFlags.Public | BindingFlags.Static).Invoke(null, null);
                return Task.FromResult(new ValueTuple<OUnit, object>(_ounit, _instance));
            }
            var ounit = ounits[pathkey];
            //TODO.. 怎么在不同线程中抓到异常？
            var instance = oapimethods.ContainsKey(ounit.TypeofInstance) && oapimethods[ounit.TypeofInstance].Count > 0 ?
                oapimethods[ounit.TypeofInstance].Pop() :
                (ounit.TypeofInstance.GetMethod("ForNew", BindingFlags.Public | BindingFlags.Static) ??
                throw new Exception($"在{nameof(ounit.TypeofInstance)}里面必须实现名字为ForNew的无参静态函数！"))
                .Invoke(null, null);
            return Task.FromResult(new ValueTuple<OUnit, object>(ounit, instance));
        }
        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="ounit">方法单元</param>
        /// <param name="instance">方法所在类型的实例</param>
        /// <returns>Task</returns>
        public Task Recycle(OUnit ounit, object instance)
        {
            var oapimethods = InMemoryStoragebox.ForNewOrExisted().OAPIMethodsStoreTypeMapper;
            if (!oapimethods.ContainsKey(ounit.TypeofInstance)) oapimethods[ounit.TypeofInstance] = new Stack<OAPIMethodsBase>();
            var typeofinstance = (OAPIMethodsBase)instance;
            typeofinstance.SetHttpContext = null;
            oapimethods[ounit.TypeofInstance].Push(typeofinstance);
            return Task.FromResult(0);
        }



        #endregion
    }
}
