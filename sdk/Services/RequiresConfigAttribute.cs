using System;

namespace Paydock_dotnet_sdk.Services
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
