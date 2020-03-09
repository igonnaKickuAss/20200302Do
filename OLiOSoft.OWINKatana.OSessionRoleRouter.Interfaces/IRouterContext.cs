using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter.Interfaces
{
    public interface IRouterContext
    {
        /// <summary>
        /// 管道上下文
        /// </summary>
        IOwinContext HttpContext { get; }
        /// <summary>
        /// 指示是否重定向
        /// </summary>
        bool Redirect { get; }
        /// <summary>
        /// 重定向地址，一旦修改<see cref="Redirect"/>为真，<see cref="Send"/>为假
        /// </summary>
        string RequestPath { get; set; }
        /// <summary>
        /// 指示是否发送JSON数据包
        /// </summary>
        bool Send { get; }
        /// <summary>
        /// JSON数据包，一旦修改<see cref="Send"/>为真，<see cref="Redirect"/>为假
        /// </summary>
        string JSONMessage { get; set; }
    }
}
