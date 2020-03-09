using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    using Interfaces;
    using Role = AnyOfMiscs.RoleEnum;
    internal class InMemoryMethods
    {
        #region -- Const Data --
        public const string defaultName = "未登录的游客";
        static readonly public Role defaultRole = Role.@default;
        #endregion

        #region -- Static APIMethods --
        static public void FillORoleContextAsYouLike(IRoleContext context)
        {
            var orole = (ORoleContext)context;
            orole.Name = defaultName;
            orole.Role = defaultRole;
            orole.NeedLogin = false;
            orole.NeedPermission = false;
        }
        static public void DefaultNotFound(IRouterContext context)
        {
            var orole = context.HttpContext.GetORoleContext() ?? ORoleContext.ForNew(context.HttpContext);
            Console.WriteLine(
                $"\n" +
                $"请求：{context.HttpContext.Request.Path.Value}\n" +
                $"名称：{orole.Name}\n" +
                $"角色：{orole.Role.ToString()}\n" +
                $"授权状态：{orole.IsAuthorized}\n" +
                $"404 Not Found..");
        }
        static public void DefaultIndex(IRouterContext context)
        {
            var orole = context.HttpContext.GetORoleContext() ?? ORoleContext.ForNew(context.HttpContext);
            Console.WriteLine(
                $"\n" +
                $"请求：{context.HttpContext.Request.Path.Value}\n" +
                $"名称：{orole.Name}\n" +
                $"角色：{orole.Role.ToString()}\n" +
                $"授权状态：{orole.IsAuthorized}\n" +
                "this is index..");
        }
        static public void DefaultNoPermission(IRouterContext context)
        {
            var orole = context.HttpContext.GetORoleContext() ?? ORoleContext.ForNew(context.HttpContext);
            Console.WriteLine(
                $"\n" +
                $"请求：{context.HttpContext.Request.Path.Value}\n" +
                $"名称：{orole.Name}\n" +
                $"角色：{orole.Role.ToString()}\n" +
                $"授权状态：{orole.IsAuthorized}\n" +
                "401 No Permission");
        }
        static public void DefaultNotLogin(IRouterContext context)
        {
            var orole = context.HttpContext.GetORoleContext() ?? ORoleContext.ForNew(context.HttpContext);
            Console.WriteLine(
                $"\n" +
                $"请求：{context.HttpContext.Request.Path.Value}\n" +
                $"名称：{orole.Name}\n" +
                $"角色：{orole.Role.ToString()}\n" +
                $"授权状态：{orole.IsAuthorized}\n" +
                "you have not login yet..");
        }
        static public void DefaultForbidden(IRouterContext context)
        {
            var orole = context.HttpContext.GetORoleContext() ?? ORoleContext.ForNew(context.HttpContext);
            Console.WriteLine(
                $"\n" +
                $"请求：{context.HttpContext.Request.Path.Value}\n" +
                $"名称：{orole.Name}\n" +
                $"角色：{orole.Role.ToString()}\n" +
                $"授权状态：{orole.IsAuthorized}\n" +
                "403 Forbidden..");
        }
        #endregion
    }

    internal class DefaultMethods : OAPIMethodsBase
    {
        public const string notfound = "/notfound";
        public const string notlogin = "/notlogin";
        public const string nopermission = "/nopermission";
        public const string fobidden = "/fobidden";
        public const string index = "/index";
        private DefaultMethods() { }
        static public DefaultMethods ForNew()
            => new DefaultMethods();

        [ORouter(notfound, true)]
        [ORole(Role.@default)]
        public void Test1() => Test();

        [ORouter(notlogin, true)]
        [ORole(Role.@default)]
        public void Test2() => Test();

        [ORouter(nopermission, true)]
        [ORole(Role.@default)]
        public void Test3() => Test();

        [ORouter(fobidden, true)]
        [ORole(Role.@default)]
        public void Test4() => Test();

        [ORouter(index, true)]
        [ORole(Role.@default)]
        public void Test5() => Test();

        private void Test()
        {
            var orouter = HttpContext.Get<ORouterContext>(Constants.ORouterContextOwinEnvironmentKey);
            orouter.Handle.Invoke(orouter);
            if (orouter.Redirect) HttpContext.Response.Redirect(orouter.RequestPath);
            else if (orouter.Send) HttpContext.Response.Write(orouter.JSONMessage);
            else return;
        }
    }


}
