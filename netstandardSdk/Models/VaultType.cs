using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Paydock_dotnet_sdk.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VaultType
    {
        session,
        permanent
    }
} 