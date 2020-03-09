using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter.Interfaces
{
    public interface ISessionStore
    {
        Task<IEnumerable<KeyValuePair<string, object>>> FindById(string osessionId);
        Task Add(string osessionId, IEnumerable<KeyValuePair<string, object>> properties);
        Task Update(string osessionId, IEnumerable<KeyValuePair<string, object>> properties);
        Task Delete(string osessionId);
    }
}
