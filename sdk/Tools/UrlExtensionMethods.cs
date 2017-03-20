using System;

namespace Paydock_dotnet_sdk.Tools
{
    public static class UrlExtensionMethods
    {
        public static string AppendParameter(this string url, string parameterName, string value)
        {
            if (value != null)
                return addUrlParameter(url, parameterName, value);

            return url;
        }

        public static string AppendParameter(this string url, string parameterName, int? value)
        {
            if (value.HasValue)
                return addUrlParameter(url, parameterName, value.Value.ToString());

            return url;
        }

        public static string AppendParameter(this string url, string parameterName, DateTime? value)
        {
            if (value.HasValue)
                return addUrlParameter(url, parameterName, value.Value.ToString("yyyy-MM-dd"));

            return url;
        }

        public static string AppendParameter(this string url, string parameterName, bool? value)
        {
            if (value.HasValue)
                return addUrlParameter(url, parameterName, value.Value.ToString().ToLower());

            return url;
        }


        private static string addUrlParameter(string currrentUrl, string name, string value)
        {
            if (!currrentUrl.Contains("?"))
                currrentUrl += "?";
            else
                currrentUrl += "&";

            return currrentUrl + Uri.EscapeUriString(name) + "=" + Uri.EscapeUriString(value);
        }
    }
}
