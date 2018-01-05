using Newtonsoft.Json;

namespace Paydock_dotnet_sdk.Tools
{
	public static class SerializeHelper
	{
		public static string Serialize(object data)
		{
			return JsonConvert.SerializeObject(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DateTimeZoneHandling = DateTimeZoneHandling.Utc });
		}

		public static T Deserialize<T>(string data)
		{			
			return (T)JsonConvert.DeserializeObject(data, typeof(T), new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Utc } );
		}
	}
}
