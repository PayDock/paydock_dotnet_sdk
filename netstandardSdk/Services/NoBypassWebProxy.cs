using System;
using System.Net;

namespace Paydock_dotnet_sdk.Tools
{
    /// <summary>
    ///     Basic <see cref="IWebProxy" /> implementation.  No bypassing so <see cref="IsBypassed" /> always returns false.
    /// </summary>
    public class NoBypassWebProxy : IWebProxy
    {
        private readonly Uri _uri;

        public NoBypassWebProxy(Uri uri, ICredentials credentials = null)
        {
            _uri = uri ?? throw new ArgumentNullException(nameof(uri));
            Credentials = credentials ?? new NetworkCredential("", "");
        }

        public Uri GetProxy(Uri destination)
        {
            return _uri;
        }

        public bool IsBypassed(Uri host)
        {
            return false;
        }

        public ICredentials Credentials { get; set; }
    }
}