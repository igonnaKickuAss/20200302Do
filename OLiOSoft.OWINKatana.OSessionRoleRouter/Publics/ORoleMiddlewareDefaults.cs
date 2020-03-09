using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    using AppAction = Action<Interfaces.IRoleContext>;
    public class ORoleMiddlewareDefaults
    {
        static public AppAction FillORoleContextAsYouLike = InMemoryMethods.FillORoleContextAsYouLike;
        static public ORoleStore Store = new ORoleStore();
    }
}
