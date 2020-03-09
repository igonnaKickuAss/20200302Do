using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    using AppAction = Action<Interfaces.IRouterContext>;
    public class ORouterMiddlewareDefaults
    {
        static public ORouterStore Store = new ORouterStore();

        static public AppAction Forbidden = InMemoryMethods.DefaultForbidden;
        static public AppAction NotLogin = InMemoryMethods.DefaultNotLogin;
        static public AppAction NotFound = InMemoryMethods.DefaultNotFound;
        static public AppAction NoPermission = InMemoryMethods.DefaultNoPermission;
        static public AppAction Index = InMemoryMethods.DefaultIndex;
    }
}
