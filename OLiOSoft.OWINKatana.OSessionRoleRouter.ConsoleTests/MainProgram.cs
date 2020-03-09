using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter.ConsoleTests
{
    using Role = AnyOfMiscs.RoleEnum;
    class MainProgram
    {
        static void Main(string[] args)
        {
            var url = "http://probook640g1:9000";
            using (WebApp.Start(url, Configuration))
            {
                Console.WriteLine($"端口：9000。已经被监听\n服务器：{url}");
                //var awaiter = client.GetStringAsync($"{url}").ContinueWith(f => Console.WriteLine(f.Result)).ConfigureAwait(false).GetAwaiter();
                //while (!awaiter.IsCompleted)
                //{
                //    Console.WriteLine("..");
                //    Thread.Sleep(500);
                //}
                Console.WriteLine("任意键关闭服务器并且退出程序。。");
                Console.ReadKey();
            }
        }

        static private void Configuration(IAppBuilder appBuilder)
        {
            var options = new OTransitServerOptions("./OLiOSoft.OWINKatana.OSessionRoleRouter.ConsoleTests.exe");
            options.ORoleMiddlewareOptions.FillORoleContextAsYouLike = (orole) =>
            {
                var oseesions = orole.HttpContext.GetOSessionContext();
                var account = (dynamic)oseesions.Get("account") ?? new { Name = "未登录的游客", Role = Role.@default };
                orole.Name = account.Name;
                orole.Role = account.Role;
                oseesions.AddOrUpdate("account", account);
            };
            options.ORouterMiddlewareOptions.Index = (orouter) =>
            {
                Console.WriteLine(
                    $"\n" +
                    $"请求：{orouter.HttpContext.Request.Path.Value}\n" +
                    $"名称：{orouter.HttpContext.GetORoleContext().Name}\n" +
                    $"角色：{orouter.HttpContext.GetORoleContext().Role.ToString()}\n" +
                    $"授权状态：{orouter.HttpContext.GetORoleContext().IsAuthorized}\n" +
                    "invoked by lambda named Index"
                    );
                orouter.RequestPath = "/index";
            };
            options.ORouterMiddlewareOptions.Forbidden = (orouter) =>
            {
                Console.WriteLine(
                    $"\n" +
                    $"请求：{orouter.HttpContext.Request.Path.Value}\n" +
                    $"名称：{orouter.HttpContext.GetORoleContext().Name}\n" +
                    $"角色：{orouter.HttpContext.GetORoleContext().Role.ToString()}\n" +
                    $"授权状态：{orouter.HttpContext.GetORoleContext().IsAuthorized}\n" +
                    "invoked by lambda named Forbidden"
                    );
                orouter.RequestPath = "/forbidden";
            };
            options.ORouterMiddlewareOptions.NoPermission = (orouter) =>
            {
                Console.WriteLine(
                    $"\n" +
                    $"请求：{orouter.HttpContext.Request.Path.Value}\n" +
                    $"名称：{orouter.HttpContext.GetORoleContext().Name}\n" +
                    $"角色：{orouter.HttpContext.GetORoleContext().Role.ToString()}\n" +
                    $"授权状态：{orouter.HttpContext.GetORoleContext().IsAuthorized}\n" +
                    "invoked by lambda named NoPermission"
                    );
                orouter.RequestPath = "/nopermission";
            };
            options.ORouterMiddlewareOptions.NotFound = (orouter) =>
            {
                Console.WriteLine(
                    $"\n" +
                    $"请求：{orouter.HttpContext.Request.Path.Value}\n" +
                    $"名称：{orouter.HttpContext.GetORoleContext().Name}\n" +
                    $"角色：{orouter.HttpContext.GetORoleContext().Role.ToString()}\n" +
                    $"授权状态：{orouter.HttpContext.GetORoleContext().IsAuthorized}\n" +
                    "invoked by lambda named NotFound"
                    );
                orouter.RequestPath = "/notfound";
            };
            options.ORouterMiddlewareOptions.NotLogin = (orouter) =>
            {
                Console.WriteLine(
                    $"\n" +
                    $"请求：{orouter.HttpContext.Request.Path.Value}\n" +
                    $"名称：{orouter.HttpContext.GetORoleContext().Name}\n" +
                    $"角色：{orouter.HttpContext.GetORoleContext().Role.ToString()}\n" +
                    $"授权状态：{orouter.HttpContext.GetORoleContext().IsAuthorized}\n" +
                    "invoked by lambda named NotLogin"
                    );
                orouter.RequestPath = "/login";
            };

            var phyfs = new PhysicalFileSystem("");
            var fs1 = new FileServerOptions
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileSystem = phyfs,
                RequestPath = new PathString("")
            };
            var fs1_1 = new FileServerOptions
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileSystem = phyfs,
                RequestPath = new PathString("/index")
            };
            var fs2 = new FileServerOptions
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileSystem = phyfs,
                RequestPath = new PathString("/login")
            };
            var fs3 = new FileServerOptions
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileSystem = phyfs,
                RequestPath = new PathString("/notfound")
            };
            var fs4 = new FileServerOptions
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileSystem = phyfs,
                RequestPath = new PathString("/nopermission")
            };
            var fs5 = new FileServerOptions
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileSystem = phyfs,
                RequestPath = new PathString("/forbidden")
            };
            var fs6 = new FileServerOptions
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileSystem = phyfs,
                RequestPath = new PathString("/pageone")
            };
            var fs7 = new FileServerOptions
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileSystem = phyfs,
                RequestPath = new PathString("/pagetwo")
            };

            fs1.DefaultFilesOptions.DefaultFileNames = new List<string> { "_index.html" };
            fs1_1.DefaultFilesOptions.DefaultFileNames = new List<string> { "_index.html" };
            fs2.DefaultFilesOptions.DefaultFileNames = new List<string> { "_login.html" };
            fs3.DefaultFilesOptions.DefaultFileNames = new List<string> { "_notfound.html" };
            fs4.DefaultFilesOptions.DefaultFileNames = new List<string> { "_nopermission.html" };
            fs5.DefaultFilesOptions.DefaultFileNames = new List<string> { "_forbidden.html" };
            fs6.DefaultFilesOptions.DefaultFileNames = new List<string> { "pageone.html" };
            fs7.DefaultFilesOptions.DefaultFileNames = new List<string> { "pagetwo.html" };

            appBuilder.UseFileServer(fs1);
            appBuilder.UseFileServer(fs2);
            appBuilder.UseFileServer(fs3);
            appBuilder.UseFileServer(fs4);
            appBuilder.UseFileServer(fs5);
            appBuilder.UseTransitServer(options);

            appBuilder.UseFileServer(fs6);
            appBuilder.UseFileServer(fs7);
        }
    }

    class TestMethods : OAPIMethodsBase
    {
        private TestMethods()
        {

        }
        static public TestMethods ForNew()
            => new TestMethods();

        [ORole(Role.admin)]
        [ORouter("/test1", true)]
        [ORouter("/test1_1", true)]
        public void Test1()
        {
            Console.WriteLine("\nhello world.. invoked by test1 in console way");
            HttpContext.Response.ContentType = "application/json;charset=utf-8";
            HttpContext.Response.Write("hello world.. invoked by test1 in broswer way");
        }

        [ORole(Role.forbidden)]
        [ORouter("/test2", true)]
        [ORouter("/test2_1", true)]
        public void Test2()
        {
            Console.WriteLine("\nhello world.. invoked by test2 in console way");
            HttpContext.Response.ContentType = "application/json;charset=utf-8";
            HttpContext.Response.Write("hello world.. invoked by test2 in broswer way");
        }

        [ORole(Role.@default)]
        [ORouter("/test3", true)]
        [ORouter("/test3_1", true)]
        public void Test3()
        {
            Console.WriteLine("\nhello world.. invoked by test3 in console way");
            HttpContext.Response.ContentType = "application/json;charset=utf-8";
            HttpContext.Response.Write("hello world.. invoked by test3 in broswer way");
        }

        [ORole(Role.staff)]
        [ORouter("/pageone", false)]
        [ORouter("/pageone", false)]
        public void Test4()
        {
            Console.WriteLine(
                $"\n" +
                $"请求：{HttpContext.Request.Path.Value}\n" +
                $"名称：{HttpContext.GetORoleContext().Name}\n" +
                $"角色：{HttpContext.GetORoleContext().Role.ToString()}\n" +
                $"授权状态：{HttpContext.GetORoleContext().IsAuthorized}\n" +
                "hello world.. invoked by test4 in console way， in other words， it sends a static file to broswer names pageone"
                );
        }

        [ORole(Role.admin)]
        [ORouter("/pagetwo", false)]
        [ORouter("/pagetwo", false)]
        public void Test5()
        {
            Console.WriteLine(
                $"\n" +
                $"请求：{HttpContext.Request.Path.Value}\n" +
                $"名称：{HttpContext.GetORoleContext().Name}\n" +
                $"角色：{HttpContext.GetORoleContext().Role.ToString()}\n" +
                $"授权状态：{HttpContext.GetORoleContext().IsAuthorized}\n" +
                "hello world.. invoked by test5 in console way， in other words， it sends a static file to broswer names pagetwo"
                );
        }

        [ORole(Role.@default)]
        [ORouter("/login", true)]
        public void Login()
        {
            Console.WriteLine("\nit has pressed the button named login.. invoked by login");
            HttpContext.Response.ContentType = "application/json;charset=utf-8";
            var formData = HttpContext.Request.ReadFormAsync().Result;
            if (formData == null || formData.Count() == 0)
            {
                HttpContext.Response.Write(JsonConvert.SerializeObject(new { ok = false, message = "有问题！刷新一下。" }));
                return;
            }
            var data = JsonConvert.DeserializeObject<dynamic>(formData["data"]);
            //HttpContext.GetOSessionContext().AddOrUpdate("account", new { data.Name, Role = Role.admin });
            //HttpContext.GetOSessionContext().AddOrUpdate("account", new { data.Name, Role = Role.staff });
            HttpContext.GetOSessionContext().AddOrUpdate("account", new { data.Name, Role = Role.forbidden });
            HttpContext.Response.Write(JsonConvert.SerializeObject(new { url = "/pageone" }));
            return;
        }
    }
}
