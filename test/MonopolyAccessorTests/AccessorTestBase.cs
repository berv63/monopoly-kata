using System.Net.Http;
using Moq;
using RichardSzalay.MockHttp;

namespace MonopolyAccessorTests
{
    public abstract class AccessorTestBase
    {
        protected MockHttpMessageHandler _mockHandler;
        protected Mock<IHttpClientFactory> _mockHttpClientFactory;
    }
}