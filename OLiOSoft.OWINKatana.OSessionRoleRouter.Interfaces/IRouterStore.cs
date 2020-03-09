using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter.Interfaces
{
    using OUnit = AnyOfMiscs.Unit;
    public interface IRouterStore
    {
        Task AddOrThrow(OUnit ounit);
        Task<ValueTuple<OUnit, object>> FindOUnitTypeofInstanceByPath(string path);
        Task Recycle(OUnit ounit, object instance);
    }
}
