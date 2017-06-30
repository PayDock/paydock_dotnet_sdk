using Paydock.Net.Sdk.Services;
using System;

namespace Paydock.Net.Sdk.Tools
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
