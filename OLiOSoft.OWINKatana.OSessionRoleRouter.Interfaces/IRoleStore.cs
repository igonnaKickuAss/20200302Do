using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter.Interfaces
{
    public interface IRoleStore
    {
        Task<int> FindRoleByPath(string path);
    }
}
