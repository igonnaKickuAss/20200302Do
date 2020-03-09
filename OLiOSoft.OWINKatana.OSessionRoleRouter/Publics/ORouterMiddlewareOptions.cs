using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    using Unit = AnyOfMiscs.Unit;
    using Role = AnyOfMiscs.RoleEnum;
    using AppAction = Action<Interfaces.IRouterContext>;

    public class ORouterMiddlewareOptions : SharedOptionsBase<ORouterMiddlewareOptions>
    {
        #region -- Private Data --
        private ORouterStore m_Store = null;
        private AppAction m_Forbidden = null;
        private AppAction m_NotLogin = null;
        private AppAction m_NotFound = null;
        private AppAction m_NoPermission = null;
        private AppAction m_Index = null;
        #endregion
        
        #region -- New --
        public ORouterMiddlewareOptions(string fileName) : base(new SharedOptions())
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException($"参数{nameof(fileName)}不能为空，调用该方法所在的程序集的文件名字");
            this.Filename = fileName;
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            m_Store = ORouterMiddlewareDefaults.Store;
            m_NotLogin = ORouterMiddlewareDefaults.NotLogin;
            m_NotFound = ORouterMiddlewareDefaults.NotFound;
            m_Forbidden = ORouterMiddlewareDefaults.Forbidden;
            m_NoPermission = ORouterMiddlewareDefaults.NoPermission;
            m_Index = ORouterMiddlewareDefaults.Index;
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            var sender = Assembly.LoadFrom(fileName);
            if (sender == null) throw new ArgumentNullException($"参数{nameof(fileName)}所描述的地址不存在程序集！");
            var box = InMemoryStoragebox.ForNewOrExisted();
            var types = sender.GetTypes();
            var typeofInstancebase = typeof(OAPIMethodsBase);
            var typeofInstances = box.Inheritances;
            typeofInstances.RemoveAll(f => f == typeof(DefaultMethods));
            typeofInstances.Add(typeof(DefaultMethods));
            for (int i = 0, length = types.Length; i < length; i++)
            {
                var type = types[i];
                while (type.BaseType != null)
                {
                    if (type.BaseType != typeofInstancebase) break;
                    typeofInstances.RemoveAll(f => f == type);
                    typeofInstances.Add(type);
                    break;
                }
            }
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            for (int i = 0, count = typeofInstances.Count; i < count; i++)
            {
                var typeofInstance = typeofInstances[i];
                var enumerator = typeofInstance.GetMethods().GetEnumerator();
                var test1 = new GetCorrectORouterAttr();
                var test2 = new GetCorrectORoleAttr();
                while (enumerator.MoveNext())
                {
                    var method = (MethodInfo)enumerator.Current;
                    //TODO.. 我只接受无参的方法..
                    if (method.GetParameters().Length != 0) continue;
                    var orouterattrs = (ORouterAttribute[])method.GetCustomAttributes(typeof(ORouterAttribute), false);
                    var oroleattrs = (ORoleAttribute[])method.GetCustomAttributes(typeof(ORoleAttribute), false);
                    if (orouterattrs.Length == 0 || oroleattrs.Length == 0) continue;
                    //var paths = orouterattrs.Select(f => new { f.Path, f.Final }).Distinct();
                    var _orouterattrs = orouterattrs.Select(f => new ORouterAttr { Final = f.Final, Path = f.Path }).Distinct(test1).ToArray();
                    var _orouterattrlist = new List<ORouterAttr>();
                    var _oroleattrs = oroleattrs.Select(f => f.Role).Distinct(test2).ToArray();
                    var oroleattr = Role.@default;
                    {
                        for (int j = 0, length = _oroleattrs.Length; j < length; j++)
                        {
                            if (j == 0)
                            {
                                oroleattr = _oroleattrs[j];
                                continue;
                            }
                            if ((int)_oroleattrs[j - 1] < (int)_oroleattrs[j]) oroleattr = _oroleattrs[j - 1];
                            else continue;
                        }
                    }
                    {
                        for (int j = 0, length = _orouterattrs.Length; j < length; j++)
                        {
                            _orouterattrlist.Add(_orouterattrs[j]);
                            _orouterattrlist.Add(new ORouterAttr { Final = _orouterattrs[j].Final, Path = $"{_orouterattrs[j].Path}/" });
                        }
                    }
                    {
                        for (int j = 0, jcount = _orouterattrlist.Count; j < jcount; j++)
                            m_Store.AddOrThrow(
                                new Unit
                                {
                                    RequestPath = new PathString(_orouterattrlist[j].Path),
                                    Final = _orouterattrlist[j].Final,
                                    Method = method,
                                    Role = oroleattr,
                                    TypeofInstance = typeofInstance
                                });
                    }
                }
            }
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            var storagebox = InMemoryStoragebox.ForNewOrExisted();
            var storageboxenumerator = storagebox.UnitStoreRouterMapper.GetEnumerator();
            while (storageboxenumerator.MoveNext())
            {
                var keyvaluepair = storageboxenumerator.Current;
                Console.WriteLine(
                    $"虚拟路径：{keyvaluepair.Key.Value}  " +
                    $"访问角色：{keyvaluepair.Value.Role}"
                    );
            }
        }
        #endregion

        #region -- Private Structs --
        private struct ORouterAttr
        {
            public bool Final;
            public string Path;
        }
        #endregion

        #region -- Private Comparers --
        private class GetCorrectORouterAttr : IEqualityComparer<ORouterAttr>
        {
            public bool Equals(ORouterAttr x, ORouterAttr y)
                => x.Path.Trim() == y.Path.Trim();
            public int GetHashCode(ORouterAttr obj)
                => obj.GetHashCode();
        }
        private class GetCorrectORoleAttr : IEqualityComparer<Role>
        {
            public bool Equals(Role x, Role y)
                => x == y;
            public int GetHashCode(Role obj)
                => obj.GetHashCode();
        }
        #endregion

        #region -- Public ShotC --
        public ORouterStore Store
        {
            get => m_Store;
            set => m_Store = value;
        }
        public AppAction Forbidden
        {
            get => m_Forbidden;
            set => m_Forbidden = value;
        }
        public AppAction NotLogin
        {
            get => m_NotLogin;
            set => m_NotLogin = value;
        }
        public AppAction NotFound
        {
            get => m_NotFound;
            set => m_NotFound = value;
        }
        public AppAction NoPermission
        {
            get => m_NoPermission;
            set => m_NoPermission = value;
        }
        public AppAction Index
        {
            get => m_Index;
            set => m_Index = value;
        }
        #endregion
    }
}

