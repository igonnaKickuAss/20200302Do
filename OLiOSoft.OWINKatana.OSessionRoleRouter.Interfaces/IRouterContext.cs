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
        IOwinContext HttpContext { get; }
        bool Redirect { get; }
        string RequestPath { get; set; }
        bool Send { get; }
        string JSONMessage { get; set; }
    }
}
