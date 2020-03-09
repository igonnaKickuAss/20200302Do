using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    using Unit = AnyOfMiscs.Unit;
    internal class InMemoryStoragebox
    {
        #region -- ReadOnly Data --
        readonly private Dictionary<string, IEnumerable<KeyValuePair<string, object>>> m_OSessionStoreMapper = null;
        readonly private Dictionary<PathString, Unit> m_UnitStoreRouterMapper = null;
        readonly private Dictionary<Type, Stack<OAPIMethodsBase>> m_OAPIMethodsStoreTypeMapper = null;
        readonly private List<Type> m_Inheritances = null;
        #endregion

        #region -- 单例 --
        private InMemoryStoragebox()
        {
            //TODO.. init
            m_OSessionStoreMapper = new Dictionary<string, IEnumerable<KeyValuePair<string, object>>>();
            m_UnitStoreRouterMapper = new Dictionary<PathString, Unit>();
            m_OAPIMethodsStoreTypeMapper = new Dictionary<Type, Stack<OAPIMethodsBase>>();
            m_Inheritances = new List<Type>();
        }
        static private InMemoryStoragebox instance = null;
        static internal InMemoryStoragebox ForNewOrExisted() =>
            instance ?? (instance = new InMemoryStoragebox());
        #endregion

        #region -- Public ShotC --
        public Dictionary<string, IEnumerable<KeyValuePair<string, object>>> OSessionStoreMapper => m_OSessionStoreMapper;
        public Dictionary<PathString, Unit> UnitStoreRouterMapper => m_UnitStoreRouterMapper;
        public Dictionary<Type, Stack<OAPIMethodsBase>> OAPIMethodsStoreTypeMapper => m_OAPIMethodsStoreTypeMapper;
        public List<Type> Inheritances => m_Inheritances;
        #endregion
    }
}
