using Microsoft.Owin;
using System;
using System.Reflection;

namespace OLiOSoft.OWINKatana.OSessionRoleRouter.AnyOfMiscs
{
    public enum RoleEnum : int
    {
        admin,
        boss,
        manager,
        staff,
        customer,
        @default,
        forbidden
    }
    public struct Unit
    {
        public Type TypeofInstance;
        public MethodInfo Method;
        public PathString RequestPath;
        public RoleEnum Role;
        public bool Final;
    }
}
