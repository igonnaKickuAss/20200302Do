using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed public class ORouterAttribute : Attribute
    {
        #region -- Readonly Private Data --
        readonly private string m_Path = string.Empty;
        readonly private bool m_Final = false;
        #endregion

        #region -- New --
        /// <summary>
        /// 指定一个虚拟路径（格式：/xxx，或者/xxx/xx。）一个是否终止管道的决定值
        /// </summary>
        /// <param name="path">虚拟路径</param>
        /// <param name="final">决定值</param>
        public ORouterAttribute(string path, bool final)
        {
            m_Path = path;
            m_Final = final;
        }
        #endregion

        #region -- Public ShotC --
        public string Path => this.m_Path;
        public bool Final => this.m_Final;
        #endregion
    }
}
