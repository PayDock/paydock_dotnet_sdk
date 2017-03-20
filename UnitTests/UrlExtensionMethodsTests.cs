using NUnit.Framework;
using Paydock_dotnet_sdk.Tools;
using System;

namespace UnitTests
{
    [TestFixture]
    public class UrlExtensionMethodsTests
    {
        [TestCase("charge/", "key", null, "charge/")]
        [TestCase("charge/", "key", "aaa", "charge/?key=aaa")]
        [TestCase("charge/?query=1", "key", "aaa", "charge/?query=1&key=aaa")]
        public void AppendParameterString(string url, string parameterName, string value, string expected)
        {
            var actual = url.AppendParameter(parameterName, value);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("charge/", "key", null, "charge/")]
        [TestCase("charge/", "key", 2, "charge/?key=2")]
        [TestCase("charge/?query=1", "key", 2, "charge/?query=1&key=2")]
        public void AppendParameterInt(string url, string parameterName, int? value, string expected)
        {
            var actual = url.AppendParameter(parameterName, value);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("charge/", "key", null, "charge/")]
        [TestCase("charge/", "key", true, "charge/?key=true")]
        [TestCase("charge/?query=1", "key", true, "charge/?query=1&key=true")]
        public void AppendParameterBool(string url, string parameterName, bool? value, string expected)
        {
            var actual = url.AppendParameter(parameterName, value);
            Assert.AreEqual(expected, actual);
        }
    }
}
