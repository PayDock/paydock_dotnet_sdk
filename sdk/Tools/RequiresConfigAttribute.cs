using Paydock_dotnet_sdk.Services;
using System;

namespace Paydock_dotnet_sdk.Tools
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequiresConfigAttribute : Attribute
    {
        public RequiresConfigAttribute()
        {
            if (string.IsNullOrEmpty(Config.SecretKey))
                throw new InvalidOperationException("Required Configuration missing");
        }
    }
}
