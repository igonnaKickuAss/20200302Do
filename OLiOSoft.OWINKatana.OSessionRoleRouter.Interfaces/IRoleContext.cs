using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter.Interfaces
{
    using Role = AnyOfMiscs.RoleEnum;
    public interface IRoleContext
    {
        IOwinContext HttpContext { get; }
        bool IsAuthorized { get; }
        bool IsForbidden { get; }
        string Name { get; set; }
        Role Role { get; set; }
        bool NeedLogin { get; }
        bool NeedPermission { get; }
    }
}
