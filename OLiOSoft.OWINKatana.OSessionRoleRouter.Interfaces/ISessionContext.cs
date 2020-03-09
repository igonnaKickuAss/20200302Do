using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter.Interfaces
{
    public interface ISessionContext
    {
        IOwinContext HttpContext { get; }
        object Get(string key);
        T Get<T>(string key);
        void AddOrUpdate(string key, object value);
        void Delete(string key);
        void Clear();
    }
}
